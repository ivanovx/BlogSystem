namespace BlogSystem.Web
{
    using Autofac;

    public class ObjectFactory
    {
        private static IContainer container = AutofacConfig.Container;

        public static T GetInstance<T>() where T : class
        {
            using (var con = container.BeginLifetimeScope())
            {
                return con.Resolve<T>();
            }
        }
    }
}