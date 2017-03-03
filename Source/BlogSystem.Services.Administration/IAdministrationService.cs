using System.Linq;
using BlogSystem.Data.Contracts;

namespace BlogSystem.Services.Administration
{
    public interface IAdministrationService<TEntity, TViewModel>
    {
        IQueryable<TViewModel> GetAll();

        TEntity GetById(object id);

        TEntity CreateEntity(TViewModel model);

        TEntity FindAndUpdateEntity(object id, TViewModel model);

        void DestroyEntity(object id);
    }
}