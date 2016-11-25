namespace XCommon.Application.Socket
{
    public class SocketMessage
    {
        public SocketMessage()
        {
        }

        public SocketMessage(string method, string value)
            : this(null, method, value)
        {
        }

        public SocketMessage(string id, string method, string value)
        {
            Id = id;
            Method = method;
            Value = value;
        }

        public string Id { get; private set; }

        public string Method { get; private set; }

        public string Value { get; private set; }
    }
}
