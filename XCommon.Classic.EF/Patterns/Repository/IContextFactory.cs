using System.Data.Entity;

namespace XCommon.Application.ContextEF
{
    public interface IContextFactory<TContext>
        where TContext : DbContext
    {
        TContext Create();
        DbContext GetContext();

    }
}
