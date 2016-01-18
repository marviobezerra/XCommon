using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using XCommon.Web.MVC.Filters;

namespace XCommon.Web.MVC.Controllers
{
    [Authorize]
    [CompressFilter]
    public abstract class RepositoryController<TBusiness, TEntity, TFilter> : BaseController
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
        public virtual async Task<JsonResult> GetByFilter(TFilter filter)
        {
            return Json(await Business.GetByFilterAsync(filter), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult GetFilterDefault()
        {
            var filter = new TFilter();
            return Json(filter, JsonRequestBehavior.DenyGet);
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
