[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BlogSystem.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(BlogSystem.Web.NinjectWebCommon), "Stop")]
namespace BlogSystem.Web
{
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Data;
    using Data.Models;
    using Data.UnitOfWork;
    using Infrastructure.Helpers;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel
                    .Bind<Func<IKernel>>()
                    .ToMethod(ctx => () => new Bootstrapper().Kernel);

                kernel
                    .Bind<IHttpModule>()
                    .To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();

                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IBlogSystemData>()
                .To<BlogSystemData>()
                .InRequestScope()
                .WithConstructorArgument("context", p => new ApplicationDbContext());

            kernel
                .Bind<IUserStore<ApplicationUser>>()
                .To<UserStore<ApplicationUser>>()
                .InRequestScope()
                .WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());

            kernel
                .Bind<IAuthenticationManager>()
                .ToMethod<IAuthenticationManager>(context => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();

            kernel
                .Bind<IUrlGenerator>()
                .To<UrlGenerator>()
                .InRequestScope();
        }
    }
}