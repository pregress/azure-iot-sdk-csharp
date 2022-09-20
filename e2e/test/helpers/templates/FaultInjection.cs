﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Exceptions;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;

// If you see intermittent failures on devices that are created by this file, check to see if you have multiple suites
// running at the same time because one test run could be accidentally destroying devices created by a different test run.

namespace Microsoft.Azure.Devices.E2ETests.Helpers.Templates
{
    public static class FaultInjection
    {
        public static readonly TimeSpan DefaultFaultDelay = TimeSpan.FromSeconds(5); // Time in seconds after service initiates the fault.
        public static readonly TimeSpan DefaultFaultDuration = TimeSpan.FromSeconds(5); // Duration in seconds
        public static readonly TimeSpan LatencyTimeBuffer = TimeSpan.FromSeconds(10); // Buffer time waiting fault occurs or connection recover

        public static readonly TimeSpan WaitForDisconnectDuration = TimeSpan.FromTicks(DefaultFaultDelay.Ticks * 3);
        public static readonly TimeSpan WaitForReconnectDuration = TimeSpan.FromTicks(DefaultFaultDuration.Ticks * 2);
        public static readonly TimeSpan ShortRetryDuration = TimeSpan.FromTicks(DefaultFaultDuration.Ticks / 2);

        public static readonly TimeSpan RecoveryTime = TimeSpan.FromMinutes(5);

        public static Client.Message ComposeErrorInjectionProperties(
            string faultType,
            string reason,
            TimeSpan delayInSecs,
            TimeSpan durationInSecs)
        {
            return new Client.Message(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
            {
                Properties =
                {
                    ["AzIoTHub_FaultOperationType"] = faultType,
                    ["AzIoTHub_FaultOperationCloseReason"] = reason,
                    ["AzIoTHub_FaultOperationDelayInSecs"] = delayInSecs.TotalSeconds.ToString(CultureInfo.InvariantCulture),
                    ["AzIoTHub_FaultOperationDurationInSecs"] = durationInSecs.TotalSeconds.ToString(CultureInfo.InvariantCulture)
                }
            };
        }

        public static bool FaultShouldDisconnect(string faultType)
        {
            return faultType != FaultInjectionConstants.FaultType_Auth
                && faultType != FaultInjectionConstants.FaultType_Throttle
                && faultType != FaultInjectionConstants.FaultType_QuotaExceeded;
        }

        // Fault timings:
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //  --- device in normal operation --- | FaultRequested | --- <delayInSec> --- | --- Device in fault mode for <durationInSec> --- | --- device in normal operation ---
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static async Task ActivateFaultInjectionAsync(
            Client.TransportType transport,
            string faultType,
            string reason,
            TimeSpan delayInSec,
            TimeSpan durationInSec,
            DeviceClient deviceClient,
            MsTestLogger logger)
        {
            logger.Trace($"{nameof(ActivateFaultInjectionAsync)}: Requesting fault injection type={faultType} reason={reason}, delay={delayInSec}s, duration={DefaultFaultDuration}s");

            uint oldTimeout = deviceClient.OperationTimeoutInMilliseconds;

            try
            {
                // For MQTT FaultInjection will terminate the connection prior to a PUBACK
                // which leads to an infinite loop trying to resend the FaultInjection message.
                if (transport == Client.TransportType.Mqtt
                    || transport == Client.TransportType.Mqtt_Tcp_Only
                    || transport == Client.TransportType.Mqtt_WebSocket_Only)
                {
                    deviceClient.OperationTimeoutInMilliseconds = (uint)delayInSec.TotalMilliseconds;
                }

                using Client.Message faultInjectionMessage = ComposeErrorInjectionProperties(
                    faultType,
                    reason,
                    delayInSec,
                    durationInSec);

                await deviceClient.SendEventAsync(faultInjectionMessage).ConfigureAwait(false);
            }
            catch (IotHubCommunicationException ex)
            {
                logger.Trace($"{nameof(ActivateFaultInjectionAsync)}: {ex}");

                // For quota injection, the fault is only seen for the original HTTP request.
                if (transport == Client.TransportType.Http1)
                {
                    throw;
                }
            }
            catch (TimeoutException ex)
            {
                logger.Trace($"{nameof(ActivateFaultInjectionAsync)}: {ex}");

                // For quota injection, the fault is only seen for the original HTTP request.
                if (transport == Client.TransportType.Http1)
                {
                    throw;
                }
            }
            finally
            {
                deviceClient.OperationTimeoutInMilliseconds = oldTimeout;
                logger.Trace($"{nameof(ActivateFaultInjectionAsync)}: Fault injection requested.");
            }
        }

        // Error injection template method.
        public static async Task TestErrorInjectionAsync(
            string devicePrefix,
            TestDeviceType type,
            Client.TransportType transport,
            string proxyAddress,
            string faultType,
            string reason,
            TimeSpan delayInSec,
            TimeSpan durationInSec,
            Func<DeviceClient, TestDevice, Task> initOperation,
            Func<DeviceClient, TestDevice, Task> testOperation,
            Func<Task> cleanupOperation,
            MsTestLogger logger)
        {
            using TestDevice testDevice = await TestDevice.GetTestDeviceAsync(logger, devicePrefix, type).ConfigureAwait(false);

            ITransportSettings transportSettings = CreateTransportSettingsFromName(transport, proxyAddress);
            DeviceClient deviceClient = testDevice.CreateDeviceClient(new ITransportSettings[] { transportSettings });

            ConnectionStatus? lastConnectionStatus = null;
            ConnectionStatusChangeReason? lastConnectionStatusChangeReason = null;
            int connectionStatusChangeCount = 0;

            deviceClient.SetConnectionStatusChangesHandler((status, statusChangeReason) =>
            {
                connectionStatusChangeCount++;
                lastConnectionStatus = status;
                lastConnectionStatusChangeReason = statusChangeReason;
                logger.Trace($"{nameof(FaultInjection)}.{nameof(ConnectionStatusChangesHandler)}: status={status} statusChangeReason={statusChangeReason} count={connectionStatusChangeCount}");
            });

            var faultInjectionDuration = new Stopwatch();

            try
            {
                await deviceClient.OpenAsync().ConfigureAwait(false);
                if (transport != Client.TransportType.Http1)
                {
                    // Normally one connection but in some cases, due to network issues we might have already retried several times to connect.
                    connectionStatusChangeCount.Should().BeGreaterOrEqualTo(
                        1,
                        $"The expected connection status change should be equal or greater than 1 but was {connectionStatusChangeCount}");

                    lastConnectionStatus.Should().Be(ConnectionStatus.Connected, $"Reason: {lastConnectionStatusChangeReason}");
                    lastConnectionStatusChangeReason.Should().Be(ConnectionStatusChangeReason.Connection_Ok);
                }

                await initOperation(deviceClient, testDevice).ConfigureAwait(false);

                logger.Trace($"{nameof(FaultInjection)} Testing baseline");
                await testOperation(deviceClient, testDevice).ConfigureAwait(false);

                int countBeforeFaultInjection = connectionStatusChangeCount;
                logger.Trace($"{nameof(FaultInjection)} Testing fault handling");
                faultInjectionDuration.Start();
                await ActivateFaultInjectionAsync(transport, faultType, reason, delayInSec, durationInSec, deviceClient, logger).ConfigureAwait(false);
                logger.Trace($"{nameof(FaultInjection)}: Waiting for fault injection to be active: {delayInSec} seconds.");
                await Task.Delay(delayInSec).ConfigureAwait(false);

                // For disconnect type faults, the device should disconnect and recover.
                if (FaultShouldDisconnect(faultType))
                {
                    logger.Trace($"{nameof(FaultInjection)}: Confirming fault injection has been activated.");
                    // Check that service issued the fault to the faulting device
                    bool isFaulted = false;

                    var connectionChangeWaitDuration = Stopwatch.StartNew();
                    while (connectionChangeWaitDuration.Elapsed < LatencyTimeBuffer)
                    {
                        if (connectionStatusChangeCount > countBeforeFaultInjection)
                        {
                            isFaulted = true;
                            break;
                        }

                        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                    }
                    connectionChangeWaitDuration.Reset();

                    isFaulted.Should().BeTrue($"The device {testDevice.Id} did not get faulted with fault type: {faultType}");
                    logger.Trace($"{nameof(FaultInjection)}: Confirmed fault injection has been activated.");

                    // Check the device is back online
                    logger.Trace($"{nameof(FaultInjection)}: Confirming device back online.");

                    connectionChangeWaitDuration.Start();
                    while (lastConnectionStatus != ConnectionStatus.Connected
                        && connectionChangeWaitDuration.Elapsed < durationInSec.Add(LatencyTimeBuffer))
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                    }
                    connectionChangeWaitDuration.Reset();

                    lastConnectionStatus.Should().Be(ConnectionStatus.Connected, $"{testDevice.Id} did not reconnect");

                    logger.Trace($"{nameof(FaultInjection)}: Confirmed device back online.");

                    // Perform the test operation.
                    logger.Trace($"{nameof(FaultInjection)}: Performing test operation for device {testDevice.Id}.");
                    await testOperation(deviceClient, testDevice).ConfigureAwait(false);
                }
                else
                {
                    logger.Trace($"{nameof(FaultInjection)}: Performing test operation while fault injection is being activated.");
                    // Perform the test operation for the faulted device multi times.
                    int counter = 0;
                    var sw = Stopwatch.StartNew();
                    while (sw.Elapsed < LatencyTimeBuffer)
                    {
                        logger.Trace($"{nameof(FaultInjection)}: Performing test operation for device - Run {counter++}.");
                        await testOperation(deviceClient, testDevice).ConfigureAwait(false);
                        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                    }
                    sw.Reset();
                }

                await deviceClient.CloseAsync().ConfigureAwait(false);

                if (transport != Client.TransportType.Http1)
                {
                    if (FaultShouldDisconnect(faultType))
                    {
                        // 4 is the minimum notification count: connect, fault, reconnect, disable.
                        // There are cases where the retry must be timed out (i.e. very likely for MQTT where otherwise
                        // we would attempt to send the fault injection forever.)
                        connectionStatusChangeCount.Should().BeGreaterOrEqualTo(4, $"For {testDevice.Id}");
                    }
                    else
                    {
                        // 2 is the minimum notification count: connect, disable.
                        // We will monitor the test environment real network stability and switch to >=2 if necessary to
                        // account for real network issues.
                        connectionStatusChangeCount.Should().Be(2, $"For {testDevice.Id}");
                    }
                    lastConnectionStatus.Should().Be(ConnectionStatus.Disabled, $"For {testDevice.Id} with reason {lastConnectionStatusChangeReason}");
                    lastConnectionStatusChangeReason.Should().Be(ConnectionStatusChangeReason.Client_Close);
                }
            }
            finally
            {
                await cleanupOperation().ConfigureAwait(false);

                deviceClient.Dispose();
                await testDevice.RemoveDeviceAsync().ConfigureAwait(false);

                faultInjectionDuration.Stop();

                TimeSpan timeToFinishFaultInjection = durationInSec.Subtract(faultInjectionDuration.Elapsed);
                if (timeToFinishFaultInjection > TimeSpan.Zero)
                {
                    logger.Trace($"{nameof(FaultInjection)}: Waiting {timeToFinishFaultInjection} to ensure that FaultInjection duration passed.");
                    await Task.Delay(timeToFinishFaultInjection).ConfigureAwait(false);
                }
            }
        }

        private static ITransportSettings CreateTransportSettingsFromName(Client.TransportType transportType, string proxyAddress)
        {
            return transportType switch
            {
                Client.TransportType.Http1 => new Http1TransportSettings
                {
                    Proxy = proxyAddress == null ? null : new WebProxy(proxyAddress),
                },
                Client.TransportType.Amqp or Client.TransportType.Amqp_Tcp_Only => new AmqpTransportSettings(transportType),
                Client.TransportType.Amqp_WebSocket_Only => new AmqpTransportSettings(transportType)
                {
                    Proxy = proxyAddress == null ? null : new WebProxy(proxyAddress),
                },
                Client.TransportType.Mqtt or Client.TransportType.Mqtt_Tcp_Only => new MqttTransportSettings(transportType),
                Client.TransportType.Mqtt_WebSocket_Only => new MqttTransportSettings(transportType)
                {
                    Proxy = proxyAddress == null ? null : new WebProxy(proxyAddress),
                },
                _ => throw new NotSupportedException($"Unknown transport: '{transportType}'."),
            };
        }
    }
}
