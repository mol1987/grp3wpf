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
        public delegate void ReturnOrderNoDelegate(int orderNo, TypeOrder typeOrder);
        static public event ReturnOrderNoDelegate returnOrderEvent;


        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/PlaceOrder")]
        public IHttpContext OrderReady(IHttpContext context)
        {
            var orderNoStr = context.Request.QueryString["orderNo"] ?? "Error";
            int orderNo;
            if (int.TryParse(orderNoStr, out orderNo) == false)
            {
                context.Response.SendResponse("Error");
                return context;
            }
            if (returnOrderEvent != null)
            {
                returnOrderEvent(orderNo, TypeOrder.PlaceOrder);
            }
            context.Response.SendResponse(orderNoStr);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/DoneOrder")]
        public IHttpContext OrderDone(IHttpContext context)
        {
            var orderNoStr = context.Request.QueryString["orderNo"] ?? "Error";
            int orderNo;
            if (int.TryParse(orderNoStr, out orderNo) == false)
            {
                context.Response.SendResponse("Error");
                return context;
            }
            if (returnOrderEvent != null)
            {
                returnOrderEvent(orderNo, TypeOrder.DoneOrder);
            }
            context.Response.SendResponse(orderNoStr);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/RemoveOrder")]
        public IHttpContext OrderRemove(IHttpContext context)
        {
            var orderNoStr = context.Request.QueryString["orderNo"] ?? "Error";
            int orderNo;
            if (int.TryParse(orderNoStr, out orderNo) == false)
            {
                context.Response.SendResponse("Error");
                return context;
            }
            if (returnOrderEvent != null)
            {
                returnOrderEvent(orderNo, TypeOrder.RemoveOrder);
            }
            context.Response.SendResponse(orderNoStr);
            return context;
        }

        public static void StartServer()
        {
            var server = new RestServer();
            server.Start();
            //using (var server = new RestServer())
            //{
            //    server.Start();
            //    Trace.WriteLine("started server");
            //    server.Stop();
            //}
        }
        //[RestRoute]
        //public IHttpContext HelloWorld(IHttpContext context)
        //{
        //    context.Response.SendResponse("No Order");
        //    return context;
        //}
    }
}
