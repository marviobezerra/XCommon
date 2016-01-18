using System.Data.Entity;
using System.Threading.Tasks;

namespace XCommon.Application.ContextEF
{
    public interface IContextFactory<TContext>
        where TContext : DbContext
    {
        Task<TContext> CreateAsync();
        Task<DbContext> GetContext();

    }
}
