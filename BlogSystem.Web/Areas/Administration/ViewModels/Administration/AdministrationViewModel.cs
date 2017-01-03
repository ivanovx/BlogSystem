namespace BlogSystem.Web.Areas.Administration.ViewModels.Administration
{
    using System;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public abstract class AdministrationViewModel
    {
        [HiddenInput(DisplayValue = true)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = true)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime? ModifiedOn { get; set; }
    }
}