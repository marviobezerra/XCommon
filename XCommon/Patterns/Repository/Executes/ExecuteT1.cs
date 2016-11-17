namespace XCommon.Patterns.Repository.Executes
{
    public class Execute<TEntity> : Execute
        where TEntity : new()
    {
        public Execute()
            : this(default(TEntity), null, null)
        {
        }

        public Execute(Execute execute)
            : this(default(TEntity), null, execute)
        {

        }

        public Execute(ExecuteUser user)
            : this(default(TEntity), user, null)
        {

        }

        public Execute(TEntity entity)
            : this(entity, null, null)
        {
            Entity = entity;
        }

        public Execute(TEntity entity, Execute execute)
            : this(entity, null, execute)
        {
        }

        public Execute(TEntity entity, ExecuteUser user)
            : this(entity, user, null)
        {
        }

        public Execute(TEntity entity, ExecuteUser user, Execute execute)
            : base(execute, user)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
