namespace BlogSystem.Web.Areas.Administration.ViewModels.ApplicationUsers
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using BlogSystem.Common;
    using BlogSystem.Data;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class ApplicationUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleManager<IdentityRole> rolerManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string administratorRoleId = rolerManager.Roles
                .Where(r => r.Name == GlobalConstants.AdminRoleName)
                .Select(r => r.Id)
                .FirstOrDefault();

            configuration.CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(model => model.IsAdmin, config => config.MapFrom(e => e.Roles.Any(user => user.RoleId == administratorRoleId)));
        }
    }
}