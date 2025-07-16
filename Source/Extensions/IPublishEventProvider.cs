// Copyright (c) Atlas Lift Tech Inc. All rights reserved.

using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCore.Routing;

/// <summary>
/// OnPublish provider allowing to trigger the Routing flow
/// </summary>
public interface IPublishEventProvider
{
    /// <summary>
    /// OnPublishAsync will by the MqttRouter
    /// </summary>
    event Func<InterceptingPublishEventArgs, Task> OnPublishAsync;
}
