using System;
using System.Collections.Generic;

namespace XCommon.Application.Executes
{
	public class ExecuteMessageInternal
    {
        public ExecuteMessageInternal()
        {
            MessageException = new List<string>();
            StackTracers = new List<string>();
        }

        public List<string> MessageException { get; set; }

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
