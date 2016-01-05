using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCommon.Patterns.Repository
{
    public interface IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <returns></returns>
        TEntity GetNew();

        /// <summary>
        /// Cria um novo registro assincono
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetNewAsync();

        /// <summary>
        /// Busca uma registro por sua chave primaria
        /// </summary>
        /// <param name="key">Chave primaria</param>
        /// <returns>Registro</returns>
        TEntity GetByKey(Guid key);

        /// <summary>
        /// Busca assincona de registro por sua chave primaria
        /// </summary>
        /// <param name="key">Chave primaria</param>
        /// <returns>Registro</returns>
        Task<TEntity> GetByKeyAsync(Guid key);

        /// <summary>
        /// Busca registros apartir de um filtro
        /// </summary>
        /// <param name="filter">Filtro para a busca</param>
        /// <returns></returns>
        List<TEntity> GetByFilter(TFilter filter);

        /// <summary>
        /// Busca assincrona registros apartir de um filtro
        /// </summary>
        /// <param name="filter">Filtro para a busca</param>
        /// <returns></returns>
        Task<List<TEntity>> GetByFilterAsync(TFilter filter);

        /// <summary>
        /// Salva um registro
        /// </summary>
        /// <param name="execute">Registro a ser salvo</param>
        /// <returns></returns>
        Execute<TEntity> Save(Execute<TEntity> execute);

        /// <summary>
        /// Salva assincrona um registro
        /// </summary>
        /// <param name="execute">Registro a ser salvo</param>
        /// <returns></returns>
        Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute);

        /// <summary>
        /// Salva uma lista de registro
        /// </summary>
        /// <param name="execute">Lista de registroa a ser salvo</param>
        /// <returns></returns>
        Execute<List<TEntity>> SaveMany(Execute<List<TEntity>> execute);

        /// <summary>
        /// Salva assincrona uma lista de registro
        /// </summary>
        /// <param name="execute">Lista de registroa a ser salvo</param>
        /// <returns></returns>
        Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute);

        /// <summary>
        /// Verifica se um registro pode ser excluido
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool CanDelete(Guid key);

        /// <summary>
        /// Verificação assincrona se um registro pode ser excluido
        /// </summary>
        /// <param name="key">Chave do registro</param>
        /// <returns></returns>
        Task<bool> CanDeleteAsync(Guid key);

        /// <summary>
        /// Executa validações em uma entidade
        /// </summary>
        /// <param name="entity">Entitade a ser validada</param>
        /// <returns>Resultado da validação</returns>
        Execute Validate(TEntity entity);

        /// <summary>
        /// Executa validações (assincronas) em uma entidade
        /// </summary>
        /// <param name="entity">Entitade a ser validada</param>
        /// <returns>Resultado da validação</returns>
        Task<Execute> ValidateAsync(TEntity entity);

        /// <summary>
        /// Executa validações em uma colleção entidade
        /// </summary>
        /// <param name="entity">Coleção de entitades a ser validada</param>
        /// <returns>Resultado da validação</returns>
        Execute ValidateMany(List<TEntity> entity);

        /// <summary>
        /// Executa validações (assincronas) em uma colleção entidade
        /// </summary>
        /// <param name="entity">Coleção de entitades a ser validada</param>
        /// <returns>Resultado da validação</returns>
        Task<Execute> ValidateManyAsync(List<TEntity> entity);
    }
}
