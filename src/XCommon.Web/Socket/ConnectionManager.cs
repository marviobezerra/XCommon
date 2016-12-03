using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCommon.Application.Socket;

namespace XCommon.Web.Socket
{
    public class ConnectionManager
    {
        public ConnectionManager(SocketMap map)
        {
            Connections = new List<ConnectionItem>();
            Map = map;

            Map.SendMessageAll += async (s, e) => await SendAll(new SocketMessage(string.Empty, e.Item1, JsonConvert.SerializeObject(e.Item2)));
            Map.SendMessageTo += async (s, e) => await SendTo(e.Item1, new SocketMessage(e.Item2, JsonConvert.SerializeObject(e.Item3)));
            Map.SendMessageAllExcept += async (s, e) => await SendAllExcept(e.Item1, new SocketMessage(e.Item2, JsonConvert.SerializeObject(e.Item3)));
        }

        private SocketMap Map { get; set; }

        private List<ConnectionItem> Connections { get; set; }

        internal async Task Add(string id, WebSocket connection)
        {
            Connections.Add(new ConnectionItem(id, connection));

            await Map.ClientConnected(id);
            await SendTo(id, new SocketMessage("Id", id));
            await KeepAlive(connection);
            await Map.ClientDisconnected(id);

            Connections.RemoveAll(c => c.Id == id);
        }

        public async Task SendTo(string id, SocketMessage message)
        {
            foreach (var item in Connections.Where(c => c.Id == id))
            {
                await Send(item.Socket, message);
            }
        }

        public async Task SendAllExcept(string[] id, SocketMessage message)
        {
            foreach (var connection in Connections.Where(c => !id.Contains(c.Id)))
            {
                await Send(connection.Socket, message);
            }
        }

        public async Task SendAll(SocketMessage message)
        {
            foreach (var connection in Connections.Where(c => c.Socket.State == WebSocketState.Open))
            {
                await Send(connection.Socket, message);
            }
        }

        private async Task Send(WebSocket webSocket, SocketMessage message)
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                var value = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
                await webSocket.SendAsync(value, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task KeepAlive(WebSocket connection)
        {
            while (connection.State == WebSocketState.Open)
            {
                var token = CancellationToken.None;
                var buffer = new ArraySegment<byte>(new byte[4096]);
                var received = await connection.ReceiveAsync(buffer, token);

                switch (received.MessageType)
                {
                    case WebSocketMessageType.Text:
                        var request = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
                        SocketMessage receive = JsonConvert.DeserializeObject<SocketMessage>(request);
                        await Map.Receive(receive);
                        break;
                }
            }
        }
    }
}
