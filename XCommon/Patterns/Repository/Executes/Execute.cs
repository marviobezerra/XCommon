using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Executes
{
    [DataContract]
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
            Messages = execute.Messages ?? new List<ExecuteMessage>();
        }

        #region Propertys

        [DataMember]
        public ExecuteUser User { get; set; }

        [DataMember]
        public bool HasErro { get; private set; }

        [DataMember]
        public bool HasWarning { get; private set; }
        
        [DataMember]
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
            executeMessage.Type = ExecuteMessageType.Excessao;

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
                HasWarning = Messages.Any(c => c.Type == ExecuteMessageType.Aviso);

            if (!HasErro)
                HasErro = Messages.Any(c => c.Type == ExecuteMessageType.Erro || c.Type == ExecuteMessageType.Excessao);
        }
        #endregion
    }
}
