using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using System;
using System.Diagnostics;
using System.Threading;

namespace Library.WebApiFunctionality
{
    [RestResource]
    public class WebApiServer
    {
        /// <summary>
        /// ...
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="typeOrder"></param>
        public delegate void ReturnOrderNoDelegate(int orderNo, TypeOrder typeOrder);

        /// <summary>
        /// ...
        /// </summary>
        static public event ReturnOrderNoDelegate returnOrderEvent;

        /// <summary>
        /// ...
        /// </summary>
        private static RestServer server = new RestServer();

        #region Routes

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/placeorder")]
        public IHttpContext OrderReady(IHttpContext context)
        {
            var orderNoStr = context.Request.QueryString["orderNo"] ?? "Error";
            int orderNo;
            if (int.TryParse(orderNoStr, out orderNo) == false)
            {
                context.Response.SendResponse("Error");
                return context;
            }
            returnOrderEvent?.Invoke(orderNo, TypeOrder.placeorder);
            context.Response.SendResponse("ok");
            return context;
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/doneorder")]
        public IHttpContext OrderDone(IHttpContext context)
        {
            var orderNoStr = context.Request.QueryString["orderNo"] ?? "Error";
            int orderNo;
            if (int.TryParse(orderNoStr, out orderNo) == false)
            {
                context.Response.SendResponse("Error");
                return context;
            }
            returnOrderEvent?.Invoke(orderNo, TypeOrder.doneorder);
            context.Response.SendResponse("ok");
            return context;
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/removeorder")]
        public IHttpContext OrderRemove(IHttpContext context)
        {
            var orderNoStr = context.Request.QueryString["orderNo"] ?? "Error";
            int orderNo;
            if (int.TryParse(orderNoStr, out orderNo) == false)
            {
                context.Response.SendResponse("Error");
                return context;
            }
            returnOrderEvent?.Invoke(orderNo, TypeOrder.removeorder);
            context.Response.SendResponse("ok");
            return context;
        } 
        #endregion

        /// <summary>
        /// Move the server variable to private property, for StopSever() access
        /// </summary>
        public static void StartServer()
        {
            server.Start();
        }

        /// <summary>
        /// todo; requires some more testing
        /// </summary>
        public static void StopServer()
        {
            if (server.IsListening)
            {
                Trace.Write("Attempting to stop the server .... ");
                server.ThreadSafeStop();
                server.Stop();
                Trace.Write(".. Stopped!");
            }
        }
    }
}
