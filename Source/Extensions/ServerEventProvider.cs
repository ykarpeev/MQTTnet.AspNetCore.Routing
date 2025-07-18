// Copyright (c) Atlas Lift Tech Inc. All rights reserved.

using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCore.Routing;

internal sealed class ServerEventProvider : IPublishEventProvider
{
    private readonly MqttServer _server;

    public ServerEventProvider(MqttServer server)
    {
        _server = server;
    }

    public event Func<InterceptingPublishEventArgs, Task> OnPublishAsync
    {
        add => _server.InterceptingPublishAsync += value;
        remove => _server.InterceptingPublishAsync -= value;
    }
}