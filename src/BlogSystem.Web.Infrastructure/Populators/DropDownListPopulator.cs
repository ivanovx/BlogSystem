namespace BlogSystem.Web.Infrastructure.Populators
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private readonly IDbRepository<Category> categoriesData;

        public DropDownListPopulator(IDbRepository<Category> categoriesData)
        {
            this.categoriesData = categoriesData;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
                var categories = this.categoriesData
                    .All()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList();

            return DefaultDropDown().Concat(categories);
        }

        public IEnumerable<SelectListItem> DefaultDropDown()
        {
            return Enumerable
                .Repeat(new SelectListItem
                {
                    Value = "",
                    Text = "Select Category"
                }, count: 1);
        }
    }
}