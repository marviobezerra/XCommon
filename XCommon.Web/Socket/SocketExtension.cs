using Microsoft.AspNetCore.Builder;
using System.Net.WebSockets;
using XCommon.Application.Login;
using XCommon.Application.Socket;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Socket
{
    public static class SocketExtension
    {
        private static ConnectionManager Manager { get; set; }

        public static IApplicationBuilder UseSocket(this IApplicationBuilder app, SocketMap map)
        {
            Manager = new ConnectionManager(map);
            
            app.UseWebSockets();

            app.Use(async (http, next) =>
            {
                ITicketManager ticket = Kernel.Resolve<ITicketManager>();

                if (http.WebSockets.IsWebSocketRequest && ticket.IsAuthenticated)
                {
                    var webSocket = await http.WebSockets.AcceptWebSocketAsync();
                    if (webSocket != null && webSocket.State == WebSocketState.Open)
                    {
                        await Manager.Add(ticket.UserKey.ToString(), webSocket);
                    }
                }
                else
                {
                    await next();
                }
            });

            return app;
        }
    }
}
