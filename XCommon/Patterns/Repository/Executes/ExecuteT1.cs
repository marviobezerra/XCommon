using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Executes
{
    public class Execute<T> : Execute
        where T : new()
    {
        public Execute()
        {
            Entity = new T();
        }

        public Execute(Execute execute)
            : base(execute)
        {

        }

        public Execute(Execute execute, T entity)
            : base(execute)
        {
            Entity = entity;
        }

        public Execute(T entity)
        {
            Entity = entity;
        }

        public Execute(T entity, ExecuteUser user)
        {
            User = user;
            Entity = entity;
        }
		
        public T Entity { get; set; }
    }
}
