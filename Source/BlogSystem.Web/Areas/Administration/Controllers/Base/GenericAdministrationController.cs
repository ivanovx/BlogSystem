namespace BlogSystem.Web.Areas.Administration.Controllers.Base
{
    using System.Linq;
    using Data.Contracts;
    using Data.Repositories;
    using ViewModels.Administration;
    using BlogSystem.Services.Web.Mapping;

    public abstract class GenericAdministrationController<TEntity, TViewModel> :  AdministrationController
        where TEntity : class, IAuditInfo
        where TViewModel: AdministrationViewModel
    {
        protected readonly IDbRepository<TEntity> dataRepository;
        protected readonly IMappingService mappingService;

        protected GenericAdministrationController(IDbRepository<TEntity> dataRepository, IMappingService mappingService)
        {
            this.dataRepository = dataRepository;
            this.mappingService = mappingService;
        }

        protected virtual IQueryable<TViewModel> GetAll()
        {
            var data = this.dataRepository.All().OrderByDescending(p => p.CreatedOn);
            var entity = this.mappingService.Map<TViewModel>(data);

            return entity;
        }

        protected virtual TEntity CreateEntity(TViewModel model)
        {
            TEntity entity = null;

            if (model != null && this.ModelState.IsValid)
            {
                entity = this.mappingService.Map<TEntity>(model);

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
                    this.mappingService.Map(model, entity);

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