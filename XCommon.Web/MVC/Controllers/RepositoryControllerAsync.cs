using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using XCommon.Web.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XCommon.Web.MVC.Controllers
{
    [Authorize]
    [CompressFilter]
    public abstract class RepositoryControllerAsync<TBusiness, TEntity, TFilter> : BaseController
        where TBusiness : IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        [Inject]
        protected TBusiness Business { get; set; }

        [HttpPost]
        [WhitespaceFilter]
        public virtual ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        [WhitespaceFilter]
        public virtual ActionResult Edit()
        {
            return PartialView();
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> GetNew()
        {
            return Json(await Business.GetNewAsync(), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> GetByKey(Guid key)
        {
            return Json(await Business.GetByKeyAsync(key), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> GetFilterDefault()
        {
            var task = Task.Factory.StartNew<TFilter>(() => new TFilter());

            return Json(await task, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> GetByFilter(TFilter filter)
        {
            return Json(await Business.GetByFilterAsync(filter), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> Save(TEntity entity)
        {
            var execute = GetExecute(entity);
            execute = await Business.SaveAsync(execute);
            return Json(execute, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> SaveMany(List<TEntity> entitys)
        {
            var execute = GetExecute(entitys);
            execute = await Business.SaveManyAsync(execute);
            return Json(execute, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> Validate(TEntity entity)
        {
            return Json(await Business.ValidateAsync(entity), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> ValidateMany(List<TEntity> entitys)
        {
            return Json(await Business.ValidateManyAsync(entitys), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual async Task<JsonResult> CanDelete(Guid key)
        {
            return Json(await Business.CanDeleteAsync(key), JsonRequestBehavior.DenyGet);
        }

        protected virtual Execute<TEntity> GetExecute(TEntity entity)
        {
            return new Execute<TEntity>
            {
                Entity = entity,
                User = UserSession
            };
        }

        protected virtual Execute<List<TEntity>> GetExecute(List<TEntity> entity)
        {
            return new Execute<List<TEntity>>
            {
                Entity = entity,
                User = UserSession
            };
        }
    }
}
