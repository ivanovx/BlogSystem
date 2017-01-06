using System.Collections.Generic;

namespace BlogSystem.Web.Areas.Administration.ViewModels.User
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Common;
    using Data;
    using Data.Models;
    using Infrastructure.Mapping;
    using Administration;

    public class ApplicationUserViewModel : AdministrationViewModel, IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            var context = new ApplicationDbContext();
            var rolerManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var administratorRoleId = rolerManager.Roles
                .Where(r => r.Name == GlobalConstants.AdminRoleName)
                .Select(r => r.Id)
                .FirstOrDefault();

            configuration.CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(model => model.IsAdmin, config => config.MapFrom(e => e.Roles.Any(user => user.RoleId == administratorRoleId)))
                .ForMember(m => m.CommentsCount, c => c.MapFrom(user => user.Comments.Count));
        }
    }
}