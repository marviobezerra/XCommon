using System;
using System.Collections.Generic;

namespace XCommon.Application.Logger
{
    public class LoggerEntity
    {
        public LoggerEntity(Exception exception, Type callerType)
        {
            Date = DateTime.Now;

            ExceptionsMessages = new List<string>();
            StackTracers = new List<string>();

            AddException(exception);
            AddCaller(callerType);
        }
        
        public LogType LogLevel { get; set; }

        public DateTime Date { get; set; }

        public object Resource { get; set; }

        public string Message { get; set; }

        public string SourceAssemblie { get; set; }

        public string SourceNameSpace { get; set; }

        public string SourceType { get; set; }

        public string SourceMethod { get; set; }

        public string SourceFilePath { get; set; }

        public int SourceLineNumber { get; set; }

        public List<string> ExceptionsMessages { get; set; }

        public List<string> StackTracers { get; set; }

        private void AddException(Exception exception)
        {
            if (exception == null)
			{
				return;
			}

			ExceptionsMessages.Add(exception.Message);

            StackTracers.Add(exception.StackTrace);

            if (exception.InnerException != null)
			{
				AddException(exception.InnerException);
			}
		}

        private void AddCaller(Type callerType)
        {
            if (callerType == null)
			{
				return;
			}

			SourceType = callerType.Name;
            SourceAssemblie = callerType.AssemblyQualifiedName;
            SourceNameSpace = callerType.Namespace;
        }
    }
}
