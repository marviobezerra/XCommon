using System.Runtime.Serialization;
using XCommon.Util;

namespace XCommon.Patterns.Repository.Executes
{
	public class ExecuteMessage
    {
        public ExecuteMessage()
        {
            MessageInternal = new ExecuteMessageInternal();
        }

        public ExecuteMessageType Type { get; set; }

        public string Message { get; set; }

        [IgnoreDataMember]
        public ExecuteMessageInternal MessageInternal { get; set; }
    }
}
