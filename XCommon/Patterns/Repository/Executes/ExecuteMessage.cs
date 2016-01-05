using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Executes
{
    [DataContract]
    public class ExecuteMessage
    {
        public ExecuteMessage()
        {
            MessageInternal = new ExecuteMessageInternal();
        }

        [DataMember]
        public ExecuteMessageType Type { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public ExecuteMessageInternal MessageInternal { get; set; }
    }
}
