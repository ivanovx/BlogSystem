namespace BlogSystem.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreatedOn { get; set; }
    }
}