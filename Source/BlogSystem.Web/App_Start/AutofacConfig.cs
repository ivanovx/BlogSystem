using BlogSystem.Services.Data;
using BlogSystem.Web.Infrastructure.Helpers.Url;

namespace BlogSystem.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using AutoMapper;
    using Data;
    using Data.Repositories;
    using Controllers;
    using Infrastructure.Helpers;
    using Infrastructure.Identity;
    using Infrastructure.Mapping;
    using Services.Web.Caching;
    using Services.Web.Mapping;

    public static class AutofacConfig
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
            builder.RegisterType<ApplicationDbContext>().As<DbContext>().InstancePerRequest();

            builder.RegisterGeneric(typeof(DbRepository<>)).As(typeof(IDbRepository<>)).InstancePerRequest();

            builder.RegisterType<CurrentUser>().As<ICurrentUser>().InstancePerRequest();

            builder.RegisterType<UrlGenerator>().As<IUrlGenerator>().InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<BaseController>().PropertiesAutowired();

            builder.Register(c => AutoMapperConfig.MapperConfiguration.CreateMapper()).As<IMapper>().SingleInstance();

            builder.RegisterType<MappingService>().As<IMappingService>().InstancePerRequest();

            builder.RegisterType<CacheService>().As<ICacheService>().InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IDataService))).AsImplementedInterfaces();
        }
    }
}