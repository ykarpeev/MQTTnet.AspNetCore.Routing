using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCore.Routing.Routing {
    public interface IRouteInvocationInterceptor
    {
        /// <summary>
        /// Executed before the route handler is executed
        /// </summary>
        /// <param name="publishEventArgs">An instance of <see cref="InterceptingPublishEventArgs"/>, containing information about the message received, such as the <see cref="InterceptingPublishEventArgs.ClientId"/> and <see cref="InterceptingPublishEventArgs.ApplicationMessage"/>.</param>
        /// <returns>Returns an opague object that may be used to correlate before- and after route execution. May be null</returns>
        Task<object> RouteExecuting( InterceptingPublishEventArgs publishEventArgs );

        /// <summary>
        /// Executed after the route handler has been executed.
        /// </summary>
        /// <param name="correlationObject">Set to the the response of <see cref="RouteExecuting(InterceptingPublishEventArgs)"/>. May be null.</param>
        /// <param name="publishEventArgs">An instance of <see cref="InterceptingPublishEventArgs"/>, containing information about the reponse, such as <see cref="InterceptingPublishEventArgs.ProcessPublish"/> and the <see cref="InterceptingPublishEventArgs.Response"/>.</param>
        /// <param name="ex">An exception if the route handler failed, otherwise null.</param>
        /// <returns></returns>
        Task RouteExecuted(object correlationObject, InterceptingPublishEventArgs publishEventArgs, Exception ex );
    }
}
