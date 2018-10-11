using IocExample.Classes;
using Ninject;

namespace IocExample
{
    class Program
    {
        //Ninject
        //static void Main(string[] args)
        //{
        //    IKernel kernel = new StandardKernel();
        //    kernel.Bind<ILogger>().To<ConsoleLogger>();
        //    kernel.Bind<IConnectionFactory>()
        //        .ToConstructor<SqlConnectionFactory>(k => new SqlConnectionFactory("SQL Connection", k.Inject<ILogger>()))
        //        .InSingletonScope();
        //    kernel.Bind<RestClient>().ToConstructor<RestClient>(k => new RestClient("API KEY"));
        //    kernel.Bind<CacheService>().To<CacheService>();
        //    kernel.Bind<CommandExecutor>().To<CommandExecutor>();
        //    kernel.Bind<QueryExecutor>().To<QueryExecutor>();
        //    kernel.Bind<UserService>().To<UserService>();
        //    kernel.Bind<CreateUserHandler>().To<CreateUserHandler>();
        //    var createUserHandler = kernel.Get<CreateUserHandler>();
        //    createUserHandler.Handle();
        //}

        static void Main(string[] args)
        {
            Kernel kernel = new Kernel();

            kernel.BindToType<ILogger>(typeof(ConsoleLogger));
            kernel.BindToType<CreateUserHandler>(typeof(CreateUserHandler));
            kernel.BindToType<CacheService>(typeof(CacheService));
            kernel.BindToType<CommandExecutor>(typeof(CommandExecutor));
            kernel.BindToType<QueryExecutor>(typeof(QueryExecutor));
            kernel.BindToType<UserService>(typeof(UserService));
            kernel.BindToObject<IConnectionFactory>(new SqlConnectionFactory("SQL Connection", kernel.Get<ILogger>()));
            kernel.BindToObject<RestClient>(new RestClient("API KEY"));


            var createUserHandler = kernel.Get<CreateUserHandler>();

            createUserHandler.Handle();

        }

    }
}