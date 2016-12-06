using System;
using System.Collections.Generic;

namespace XCommon.Application.Logger
{
    public class LoggerEntity
    {
        public LoggerEntity()
        {
            ExceptionsMessages = new List<string>();
            StackTracers = new List<string>();
        }

        public LoggerEntity(Exception exception)
            : this()
        {
            AddException(exception);
        }

        public LogType Type { get; set; }

        public object Resource { get; set; }

        public string Message { get; set; }

        public string MemberName { get; set; }

        public string SourceFilePath { get; set; }

        public int SourceLineNumber { get; set; }

        public List<string> ExceptionsMessages { get; set; }

        public List<string> StackTracers { get; set; }

        private void AddException(Exception exception)
        {
            if (exception == null)
                return;

            ExceptionsMessages.Add(exception.Message);

            StackTracers.Add(exception.StackTrace);

            if (exception.InnerException != null)
                AddException(exception.InnerException);
        }
    }
}
