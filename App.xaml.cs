using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using BfK_S_ApiProjekt.ViewViewModels;
using BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard;
using ModernWpf;
using BfK_S_ApiProjekt.Models;
using BfK_S_ApiProjekt.ViewViewModels.Edit;


namespace BfK_S_ApiProjekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient internetClient = new HttpClient();
        public static string ApiKey = "AIzaSyDcGWHwF78W-ZS6f9SVuZbP__eB3Sxe3Hc";
        public static List<FlashcardAlt> AllFlashcards = new List<FlashcardAlt>();

        public IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;

            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();

            services.AddSingleton<CreateFlashcardViewModel>();
            services.AddSingleton<CreateSingleFlashcardViewModel>();
            services.AddSingleton<CreateMultipleFlashcardsGeminiViewModel>();

            services.AddSingleton<EditFlashcardViewModel>();
            services.AddSingleton<EditThemeViewModel>();

            services.AddSingleton<FlashcardTableViewModel>();
            services.AddSingleton<ShowFlashcardViewModel>();


            services.AddSingleton<LoadedSqlData>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            };
            mainWindow.Show();
        }
    }
}
