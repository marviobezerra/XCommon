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
        public virtual JsonResult GetNew()
        {
            return Json(Business.GetNew(), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult GetByKey(Guid key)
        {
            return Json(Business.GetByKey(key), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult GetByFilter(TFilter filter)
        {
            return Json(Business.GetByFilter(filter), JsonRequestBehavior.DenyGet);
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
        public virtual JsonResult Save(TEntity entity)
        {
            var execute = GetExecute(entity);
            execute = Business.Save(execute);
            return Json(execute, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult SaveMany(List<TEntity> entitys)
        {
            var execute = GetExecute(entitys);
            execute = Business.SaveMany(execute);
            return Json(execute, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult Validate(TEntity entity)
        {
            return Json(Business.Validate(entity), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult ValidateMany(List<TEntity> entitys)
        {
            return Json(Business.ValidateMany(entitys), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult CanDelete(Guid key)
        {
            return Json(Business.CanDelete(key), JsonRequestBehavior.DenyGet);
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
