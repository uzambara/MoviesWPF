using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Abstractions.Services;
using Movies.BLL.Mappers;
using Movies.BLL.Services;
using Movies.DAL.Data;
using Movies.DAL.Data.Repositories;
using Movies.UI.Components;
using Movies.UI.Events;
using Movies.UI.Factories;
using Movies.UI.Navigation;
using Movies.UI.Pages;
using Movies.UI.VewModels;

namespace Movies.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public static IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetupExceptionHandling();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContextPool<MoviesDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("movies"));
            });

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Content = ServiceProvider.GetRequiredService<SearchPage>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Navigation
            services.AddSingleton<PageNavigator>();

            // Factories
            services.AddSingleton<FavoriteCardFactory>();

            // EventManager
            services.AddSingleton<IEventManager, AppEventManager>();

            // Automapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MovieMapper)));

            // Services
            services.AddTransient<IMovieService, MovieService>();

            // Repositories
            services.AddTransient(typeof(BaseRepository<>));

            // ApiService
            services.AddSingleton<IMovieApiClient, OmdbApiClientService>();

            // ViewModels
            services.AddTransient<SearchPageViewModel>();
            services.AddTransient<FavoriteCardViewModel>();
            services.AddTransient<FavoritePageViewModel>();

            // Pages
            services.AddSingleton<MainWindow>();
            services.AddTransient<SearchPage>();
            services.AddTransient<FavoritePage>();

            // Components
            services.AddTransient<FavoriteCard>();
        }

        private void SetupExceptionHandling()
        {
            var exceptionHandler = new ExceptionHandler();
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                exceptionHandler.HandleException(e.ExceptionObject);
            };

            DispatcherUnhandledException += (s, e) =>
            {
                exceptionHandler.HandleException(e.Exception);
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                exceptionHandler.HandleException(e.Exception);
                e.SetObserved();
            };
        }
    }
}