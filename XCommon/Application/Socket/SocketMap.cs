using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace XCommon.Application.Socket
{
    public abstract class SocketMap
    {
        public abstract Task Receive(SocketMessage msg);

        public abstract Task ClientConnected(string id);

        public abstract Task ClientDisconnected(string id);

        public async Task SendTo<T>(string clientId, T value, [CallerMemberName] string memberName = "")
        {
            SendMessageTo?.Invoke(this, new Tuple<string, string, object>(clientId, memberName, value));
            await Task.Yield();
        }

        public async Task SendAllExcept<T>(string[] clientIdFilter, T value, [CallerMemberName] string memberName = "")
        {
            SendMessageAllExcept?.Invoke(this, new Tuple<string[], string, object>(clientIdFilter, memberName, value));
            await Task.Yield();
        }

        public async Task SendAll<T>(T value, [CallerMemberName] string memberName = "")
        {
            SendMessageAll?.Invoke(this, new Tuple<string, object>(memberName, value));
            await Task.Yield();
        }

        public event EventHandler<Tuple<string, object>> SendMessageAll;
        public event EventHandler<Tuple<string, string, object>> SendMessageTo;
        public event EventHandler<Tuple<string[], string, object>> SendMessageAllExcept;
    }
}
