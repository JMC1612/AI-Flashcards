using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace BfK_S_ApiProjekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient internetClient = new HttpClient();
        public static string ApiKey = "AIzaSyDcGWHwF78W-ZS6f9SVuZbP__eB3Sxe3Hc";
        public static List<Flashcard> AllFlashcards = new List<Flashcard>();
    }
}
