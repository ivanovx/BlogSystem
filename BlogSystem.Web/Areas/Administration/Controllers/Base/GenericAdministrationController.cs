using System.Collections.Generic;
using System.Linq;
using BlogSystem.Data.Contracts;
using BlogSystem.Data.Repositories;
using BlogSystem.Web.Areas.Administration.ViewModels.Administration;
using BlogSystem.Web.Infrastructure.Mapping;

namespace BlogSystem.Web.Areas.Administration.Controllers.Base
{
    public abstract class GenericAdministrationController<TEntity, TViewModel> :  AdministrationController
        where TEntity : class, IAuditInfo
        where TViewModel: AdministrationViewModel
    {
        private readonly IDbRepository<TEntity> dataRepository;

        protected GenericAdministrationController(IDbRepository<TEntity> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        protected virtual IQueryable<TViewModel> GetAll()
        {
            IQueryable<TViewModel> entity = this.dataRepository.All().OrderByDescending(p => p.CreatedOn).To<TViewModel>();

            return entity;
        }

        protected virtual TEntity CreateEntity(TViewModel model)
        {
            TEntity entity = null;

            if (model != null && this.ModelState.IsValid)
            {
                entity = this.Mapper.Map<TEntity>(model);

                this.dataRepository.Add(entity);
                this.dataRepository.SaveChanges();

                model.CreatedOn = entity.CreatedOn;
            }

            return entity;
        }

        protected virtual TEntity FindAndUpdateEntity(object id, TViewModel model)
        {
            TEntity entity = null;

            if (model != null && this.ModelState.IsValid)
            {
                entity = this.dataRepository.Find(id);

                if (entity != null)
                {
                    this.Mapper.Map(model, entity);

                    this.dataRepository.Update(entity);

                    model.ModifiedOn = entity.ModifiedOn;

                    this.dataRepository.SaveChanges();
                }
            }

            return entity;
        }

        protected virtual void DestroyEntity(object id)
        {
            if (this.ModelState.IsValid)
            {
                this.dataRepository.Remove(id);
                this.dataRepository.SaveChanges();
            }
        }
    }
}