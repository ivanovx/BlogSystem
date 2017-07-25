namespace BlogSystem.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Reflection;

    using Autofac;
    using Autofac.Integration.Mvc;

    using AutoMapper;

    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security.DataProtection;

    using BlogSystem.Data;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using BlogSystem.Web.Identity;
    using BlogSystem.Web.Controllers;
    using BlogSystem.Web.Services.Caching;
    using BlogSystem.Web.Services.Mapping;

    using BlogSystem.Web.Infrastructure.Helpers.Url;
    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.Infrastructure.XSS;

    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            builder.Register(c => c.Resolve<ApplicationDbContext>()).As<DbContext>().InstancePerRequest();

            builder.RegisterGeneric(typeof(DbRepository<>)).As(typeof(IDbRepository<>)).InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();

            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<DbContext>())).AsImplementedInterfaces().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new DpapiDataProtectionProvider("Application​")
            });

            builder.RegisterType<CurrentUser>().As<ICurrentUser>().InstancePerRequest();
            
            builder.RegisterType<UrlGenerator>().As<IUrlGenerator>().InstancePerRequest();

            builder.RegisterType<HtmlSanitizerAdapter>().As<ISanitizer>().InstancePerRequest();

            builder.RegisterType<CacheService>().As<ICacheService>().InstancePerRequest();

            builder.RegisterType<MappingService>().As<IMappingService>().InstancePerRequest();

            builder.Register(c => AutoMapperConfig.MapperConfiguration.CreateMapper()).As<IMapper>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}