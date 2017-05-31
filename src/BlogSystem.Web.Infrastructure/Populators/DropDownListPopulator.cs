namespace BlogSystem.Web.Infrastructure.Populators
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repositories;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private readonly IDbRepository<Category> categoriesData;

        public DropDownListPopulator(IDbRepository<Category> categoriesData)
        {
            this.categoriesData = categoriesData;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            return this.categoriesData
                .All()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }
    }
}