using System.Linq;
using BlogSystem.Data.Contracts;
using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;
using BlogSystem.Services.Web.Mapping;

namespace BlogSystem.Services.Administration
{
    public class AdministrationService<TEntity, TViewModel> : IAdministrationService<TEntity, TViewModel>
        where TEntity : class, IAuditInfo
        where TViewModel : IAuditInfo
    {
        private readonly IDbRepository<TEntity> dataRepository;
        private readonly IMappingService mappingService;

        public AdministrationService(IDbRepository<TEntity> dataRepository, IMappingService mappingService)
        {
            this.dataRepository = dataRepository;
            this.mappingService = mappingService;
        }

        public virtual IQueryable<TViewModel> GetAll()
        {
            var data = this.dataRepository.All().OrderByDescending(p => p.CreatedOn);
            var entity = this.mappingService.Map<TViewModel>(data);

            return entity;
        }

        public TEntity GetById(object id)
        {
            return this.dataRepository.Find(id);
        }

        public virtual TEntity CreateEntity(TViewModel model)
        {
            TEntity entity = null;

            if (model != null)
            {
                entity = this.mappingService.Map<TEntity>(model);

                this.dataRepository.Add(entity);
                this.dataRepository.SaveChanges();

                model.CreatedOn = entity.CreatedOn;
            }

            return entity;
        }

        public virtual TEntity FindAndUpdateEntity(object id, TViewModel model)
        {
            TEntity entity = null;

            if (model != null)
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

        public virtual void DestroyEntity(object id)
        {
            this.dataRepository.Remove(id);
            this.dataRepository.SaveChanges();
        }
    }
}