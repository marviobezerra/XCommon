namespace XCommon.Patterns.Repository.Executes
{
    public class Execute<TEntity> : Execute
        where TEntity : new()
    {
        public Execute()
        {
            Entity = new TEntity();
        }

        public Execute(Execute execute)
            : base(execute)
        {

        }

        public Execute(Execute execute, TEntity entity)
            : base(execute)
        {
            Entity = entity;
        }

        public Execute(TEntity entity)
        {
            Entity = entity;
        }

        public Execute(TEntity entity, ExecuteUser user)
        {
            User = user;
            Entity = entity;
        }
		
        public TEntity Entity { get; set; }
    }
}
