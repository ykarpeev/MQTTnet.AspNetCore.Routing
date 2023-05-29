using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCore.Routing.Routing
{
    public interface IRouteInvocationInterceptor
    {
        /// <summary>
        /// Executed before the route handler is executed
        /// </summary>
        /// <param name="clientId">The identifier of sender of the message</param>
        /// <param name="applicationMessage">The message being handled</param>
        /// <returns>Returns an opague object that may be used to correlate before- and after route execution. May be null</returns>
        Task<object> RouteExecuting(string clientId, MqttApplicationMessage applicationMessage);

        /// <summary>
        /// Executed after the route handler has been executed.
        /// </summary>
        /// <param name="o">Set to the the response of <see cref="RouteExecuting(string, MqttApplicationMessage)"/>. May be null.</param>
        /// <param name="ex">An exception if the route handler failed. Otherwise null.</param>
        /// <returns></returns>
        Task RouteExecuted(object o, Exception ex);
    }
}
