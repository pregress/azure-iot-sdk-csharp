﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.E2ETests.Helpers;
using Microsoft.Azure.Devices.E2ETests.Helpers.Templates;
using Microsoft.Azure.Devices.E2ETests.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Azure.Devices.E2ETests
{
    public partial class FaultInjectionPoolAmqpTests
    {
        private const string MethodDevicePrefix = "E2E_MethodFaultInjectionPoolAmqpTests";
        private const string MethodName = "MethodE2EFaultInjectionPoolAmqpTests";

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodTcpConnRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                        TestDeviceType.Sasl,
                        Client.TransportType.Amqp_Tcp_Only,
                        PoolingOverAmqp.SingleConnection_PoolSize,
                        PoolingOverAmqp.SingleConnection_DevicesCount,
                        MethodE2ETests.SetDeviceReceiveMethodAsync,
                        FaultInjection.FaultType_Tcp,
                        FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodTcpConnRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_IotHubSak_DeviceMethodTcpConnRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodTcpConnRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodAmqpConnLostRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodAmqpConnLostRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodAmqpConnLostRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodAmqpConnLostRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodSessionLostRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodSessionLostRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodSessionLostRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHub_DeviceMethodSessionLostRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodReqLinkDropRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodReqLinkDropRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodReqLinkDropRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodReqLinkDropRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodRespLinkDropRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodRespLinkDropRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodRespLinkDropRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [Ignore]
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodRespLinkDropRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodGracefulShutdownRecovery_SingleConnection_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_GracefulShutdownAmqp,
                    FaultInjection.FaultCloseReason_Bye)
                .ConfigureAwait(false);
        }

        // TODO: #943 - Honor different pool sizes for different connection pool settings.
        [Ignore]
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodGracefulShutdownRecovery_SingleConnection_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.SingleConnection_PoolSize,
                    PoolingOverAmqp.SingleConnection_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_GracefulShutdownAmqp,
                    FaultInjection.FaultCloseReason_Bye)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodTcpConnRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodTcpConnRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_IotHubSak_DeviceMethodTcpConnRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodTcpConnRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_Tcp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodAmqpConnLostRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodAmqpConnLostRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodAmqpConnLostRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodAmqpConnLostRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpConn,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodSessionLostRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodSessionLostRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodSessionLostRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_IoTHub_DeviceMethodSessionLostRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpSess,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodReqLinkDropRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodReqLinkDropRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodReqLinkDropRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodReqLinkDropRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodReq,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodRespLinkDropRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodRespLinkDropRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodRespLinkDropRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        // TODO: #950 - Link/session faults for message send/ method/ twin operations closes the connection.
        [TestMethod]
        public async Task Method_IoTHubSak_DeviceMethodRespLinkDropRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_AmqpMethodResp,
                    FaultInjection.FaultCloseReason_Boom,
                    authScope: ConnectionStringAuthScope.IoTHub)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodGracefulShutdownRecovery_MultipleConnections_Amqp()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_Tcp_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_GracefulShutdownAmqp,
                    FaultInjection.FaultCloseReason_Bye)
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task Method_DeviceSak_DeviceMethodGracefulShutdownRecovery_MultipleConnections_AmqpWs()
        {
            await SendMethodAndRespondRecoveryPoolOverAmqpAsync(
                    TestDeviceType.Sasl,
                    Client.TransportType.Amqp_WebSocket_Only,
                    PoolingOverAmqp.MultipleConnections_PoolSize,
                    PoolingOverAmqp.MultipleConnections_DevicesCount,
                    MethodE2ETests.SetDeviceReceiveMethodAsync,
                    FaultInjection.FaultType_GracefulShutdownAmqp,
                    FaultInjection.FaultCloseReason_Bye)
                .ConfigureAwait(false);
        }

        private async Task SendMethodAndRespondRecoveryPoolOverAmqpAsync(
            TestDeviceType type,
            Client.TransportType transport,
            int poolSize,
            int devicesCount,
            Func<DeviceClient, string, Task<Task>> setDeviceReceiveMethod,
            string faultType,
            string reason,
            int delayInSec = FaultInjection.DefaultDelayInSec,
            int durationInSec = FaultInjection.DefaultDurationInSec,
            ConnectionStringAuthScope authScope = ConnectionStringAuthScope.Device)
        {
            var testDevicesWithCallbackHandler = new Dictionary<string, TestDeviceCallbackHandler>();

            Func<DeviceClient, TestDevice, Task> initOperation = async (deviceClient, testDevice) =>
            {
                var testDeviceCallbackHandler = new TestDeviceCallbackHandler(deviceClient);
                testDevicesWithCallbackHandler.Add(testDevice.Id, testDeviceCallbackHandler);

                _log.Trace($"{nameof(MethodE2EPoolAmqpTests)}: Setting method callback handler for device {testDevice.Id}");
                await testDeviceCallbackHandler
                    .SetDeviceReceiveMethodAsync(MethodName, MethodE2ETests.DeviceResponseJson, MethodE2ETests.ServiceRequestJson)
                    .ConfigureAwait(false);
            };

            Func<DeviceClient, TestDevice, Task> testOperation = async (deviceClient, testDevice) =>
            {
                TestDeviceCallbackHandler testDeviceCallbackHandler = testDevicesWithCallbackHandler[testDevice.Id];
                using var cts = new CancellationTokenSource(FaultInjection.RecoveryTimeMilliseconds);

                _log.Trace($"{nameof(MethodE2EPoolAmqpTests)}: Preparing to receive method for device {testDevice.Id}");
                Task serviceSendTask = MethodE2ETests.ServiceSendMethodAndVerifyResponseAsync(
                    testDevice.Id,
                    MethodName,
                    MethodE2ETests.DeviceResponseJson,
                    MethodE2ETests.ServiceRequestJson);
                Task methodReceivedTask = testDeviceCallbackHandler.WaitForMethodCallbackAsync(cts.Token);

                await Task.WhenAll(serviceSendTask, methodReceivedTask).ConfigureAwait(false);
            };

            Func<IList<DeviceClient>, Task> cleanupOperation = async (deviceClients) =>
            {
                foreach (DeviceClient deviceClient in deviceClients)
                {
                    deviceClient.Dispose();
                }

                testDevicesWithCallbackHandler.Clear();
                await Task.FromResult<bool>(false).ConfigureAwait(false);
            };

            await FaultInjectionPoolingOverAmqp
                .TestFaultInjectionPoolAmqpAsync(
                    MethodDevicePrefix,
                    transport,
                    poolSize,
                    devicesCount,
                    faultType,
                    reason,
                    delayInSec,
                    durationInSec,
                    initOperation,
                    testOperation,
                    cleanupOperation,
                    authScope)
                .ConfigureAwait(false);
        }
    }
}