using Autofac;
using Microsoft.Extensions.Logging;
using ToDoApp.Data;
using ToDoApp.MVVM;
using ToDoApp.Service;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp
{
    public static class MauiProgram
    {
        private static IViewFactory viewFactory;

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddTransient<ToDoListPage>();
            builder.Services.AddTransient<ToDoItemPage>();

            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddTransient<ToDoListPageViewModel>();
            builder.Services.AddTransient<ToDoItemPageViewModel>();

            builder.Services.AddSingleton<IAppDatabase, AppDatabase>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            var ioc = new ContainerBuilder();
            ioc.RegisterType<AppDatabase>().As<IAppDatabase>().SingleInstance();
            ioc.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            ioc.RegisterType(typeof(HomePage));
            ioc.RegisterType(typeof(HomePageViewModel));

            ioc.RegisterType(typeof(ToDoListPage));
            ioc.RegisterType(typeof(ToDoListPageViewModel));

            ioc.RegisterType(typeof(ToDoItemPage));
            ioc.RegisterType(typeof(ToDoItemPageViewModel));

            ioc.Register<IViewFactory>(context => viewFactory);

            var container = ioc.Build();
            Resolver.Init(container);

            viewFactory = new ViewFactory(container);
            viewFactory.Register<HomePageViewModel, HomePage>();
            viewFactory.Register<ToDoListPageViewModel, ToDoListPage>();
            viewFactory.Register<ToDoItemPageViewModel, ToDoItemPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
