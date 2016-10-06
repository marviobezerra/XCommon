using System.Net.WebSockets;

namespace XCommon.Web.Socket
{
    public class ConnectionItem
    {
        public ConnectionItem(string id, WebSocket socket)
        {
            Id = id;
            Socket = socket;
        }

        public string Id { get; set; }

        public WebSocket Socket { get; set; }

    }
}
