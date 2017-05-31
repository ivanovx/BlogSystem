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
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Services.Data;
    using Services.Web;
    using Identity;
    using Controllers;
    using Infrastructure.Helpers.Url;
    using Infrastructure.Identity;
    using Infrastructure.Mapping;
    using Infrastructure.Settings;
    using Infrastructure.XSS;

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
            builder
                .RegisterType<ApplicationDbContext>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .Register(c => c.Resolve<ApplicationDbContext>())
                .As<DbContext>()
                .InstancePerRequest();

            builder
                .RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            builder
                .RegisterType<ApplicationUserManager>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<ApplicationSignInManager>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .Register(c => new UserStore<ApplicationUser>(c.Resolve<DbContext>()))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>();

            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });

            builder
                .RegisterType<CurrentUser>()
                .As<ICurrentUser>()
                .InstancePerRequest();

            builder
                .RegisterType<SettingsManager>()
                .As<ISettingsManager>()
                .InstancePerRequest();

            builder
                .RegisterType<UrlGenerator>()
                .As<IUrlGenerator>()
                .InstancePerRequest();

            builder
                .RegisterType<HtmlSanitizerAdapter>()
                .As<ISanitizer>()
                .InstancePerRequest();

            builder
                .Register(c => AutoMapperConfig.MapperConfiguration.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>()
                .PropertiesAutowired();

            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IDataService)))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IWebService)))
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}