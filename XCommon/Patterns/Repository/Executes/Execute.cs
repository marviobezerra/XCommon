﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCommon.Util;

namespace XCommon.Patterns.Repository.Executes
{
	public class Execute
    {
        public Execute()
        {
			
            Messages = new List<ExecuteMessage>();
        }

        public Execute(Execute execute)
        {
            User = execute.User;
            HasWarning = execute.HasWarning;
            HasErro = execute.HasErro;
			HasException = execute.HasException;
            Messages = execute.Messages ?? new List<ExecuteMessage>();
        }

        #region Propertys
		
		[IgnoreDataMember, Ignore]
        public ExecuteUser User { get; set; }

        public bool HasErro { get; private set; }

		public bool HasException { get; private set; }

        public bool HasWarning { get; private set; }
        
        public List<ExecuteMessage> Messages { get; private set; }
        #endregion

        #region public
        public virtual void AddMessage(ExecuteMessageType type, string message, params object[] args)
        {
            AddMessage(type, string.Format(message, args));
        }

        public virtual void AddMessage(ExecuteMessageType type, string message)
        {
            Messages.Add(new ExecuteMessage { Type = type, Message = message });
            CheckMessage();
        }

        public virtual void AddMessage(Exception ex, string message)
        {
            ExecuteMessage executeMessage = new ExecuteMessage();
            executeMessage.MessageInternal.AddException(ex);
            executeMessage.Message = message;
            executeMessage.Type = ExecuteMessageType.Exception;

            Messages.Add(executeMessage);

            CheckMessage();
        }

        public virtual void AddMessage(Exception ex, string message, params object[] args)
        {
            AddMessage(ex, string.Format(message, args));
        }

        public virtual void AddMessage(params Execute[] execute)
        {
            foreach (Execute item in execute)
            {
                AddMessage(item);
            }
        }

        public virtual void AddMessage(Execute execute)
        {
            Messages.AddRange(execute.Messages.Where(c => !Messages.Contains(c)));
            CheckMessage();
        }

        public virtual string CompileMessages()
        {
            string result = string.Empty;
            Messages.ForEach(c => result += string.Format("{0} - {1}{2}", c.Message, c.Type, Environment.NewLine));
            return result;
        }
        #endregion

        #region private
        private void CheckMessage()
        {
            if (!HasWarning)
                HasWarning = Messages.Any(c => c.Type == ExecuteMessageType.Warning);

            if (!HasErro)
                HasErro = Messages.Any(c => c.Type == ExecuteMessageType.Error || c.Type == ExecuteMessageType.Exception);

			if (!HasException)
				HasException = Messages.Any(c => c.Type == ExecuteMessageType.Exception);
		}
        #endregion
    }
}
