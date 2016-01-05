using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Executes
{
    public class ExecuteMessageInternal
    {
        public ExecuteMessageInternal()
        {
            MessageException = new List<string>();
            StackTracers = new List<string>();
        }

        [DataMember]
        public List<string> MessageException { get; set; }

        [DataMember]
        public List<string> StackTracers { get; set; }

        public void AddException(Exception e)
        {
            if (e == null)
                return;

            MessageException.Add(e.Message);

            StackTracers.Add(e.StackTrace);

            if (e.InnerException != null)
                AddException(e.InnerException);
        }
    }
}
