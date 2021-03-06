using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace XCommon.Application.Executes
{
	public class Execute
	{
		public Execute()
			: this(null, null)
		{
		}

		public Execute(ExecuteUser user)
			: this(null, user)
		{
		}

		public Execute(Execute execute)
			: this(execute, null)
		{
		}

		public Execute(Execute execute, ExecuteUser user)
		{
			Messages = new List<ExecuteMessage>();
			Properties = new Dictionary<string, object>();

			User = user ?? execute?.User;

			if (execute != null)
			{
				AddMessage(execute);
			}
		}

		#region Properties

		[IgnoreDataMember]
		private Dictionary<string, object> Properties { get; set; }

		[IgnoreDataMember]
		public ExecuteUser User { get; set; }

		public bool HasErro { get; private set; }

		public bool HasException { get; private set; }

		public bool HasWarning { get; private set; }

		public List<ExecuteMessage> Messages { get; private set; }
		#endregion

		#region public
		public virtual void AddMessage(ExecuteMessageType type, string message)
		{
			Messages.Add(new ExecuteMessage { Type = type, Message = message });
			CheckMessage();
		}

		public virtual void AddError(string message) => AddMessage(ExecuteMessageType.Error, message);

		public virtual void AddError(string message, params object[] args) => AddMessage(ExecuteMessageType.Error, message, args);

		public virtual void AddWarning(string message) => AddMessage(ExecuteMessageType.Warning, message);

		public virtual void AddWarning(string message, params object[] args) => AddMessage(ExecuteMessageType.Warning, message, args);
		
        public virtual void AddMessage(ExecuteMessageType type, string message, params object[] args)
            => AddMessage(type, string.Format(message ?? string.Empty, args ?? new object[] { }));

        public virtual void AddException(Exception ex, string message)
        {
            var executeMessage = new ExecuteMessage();
            executeMessage.MessageInternal.AddException(ex);
            executeMessage.Message = message;
            executeMessage.Type = ExecuteMessageType.Exception;

            Messages.Add(executeMessage);

            CheckMessage();
        }

        public virtual void AddException(Exception ex, string message, params object[] args) => AddException(ex, string.Format(message, args));

		public virtual void AddMessage(params Execute[] execute)
        {
            foreach (var item in execute)
            {
                AddMessage(item);
            }
        }

        public virtual void AddMessage(Execute execute)
        {
            Messages.AddRange(execute.Messages.Where(c => !Messages.Contains(c)));
            CheckMessage();
        }
        
        public virtual TValue GetProperty<TValue>(string key)
        {

			if (Properties.TryGetValue(key, out object result))
			{
				return (TValue)result;
			}

			return default(TValue);
        }

        public virtual bool SetProperty<TValue>(string key, TValue value)
        {
            try
            {
                Properties[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region private
        private void CheckMessage()
        {
            if (!HasWarning)
			{
				HasWarning = Messages.Any(c => c.Type == ExecuteMessageType.Warning);
			}

			if (!HasErro)
			{
				HasErro = Messages.Any(c => c.Type == ExecuteMessageType.Error || c.Type == ExecuteMessageType.Exception);
			}

			if (!HasException)
			{
				HasException = Messages.Any(c => c.Type == ExecuteMessageType.Exception);
			}
		}
        #endregion
    }
}
