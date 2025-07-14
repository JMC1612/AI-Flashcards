using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BfK_S_ApiProjekt.ViewViewModels;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonRequiredAttribute = Newtonsoft.Json.JsonRequiredAttribute;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BfK_S_ApiProjekt
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //SQLiteManager.SQLiteConnector = SQLiteManager.Initialize();
            //SQLiteManager.SetupTables();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    await Gemini.AiCall(Convert.ToInt16(ammountTextBox.Text), themeTextbox.Text);

        //    //UpdateFlashcardDisplay();
        //}

        //private void UpdateFlashcardDisplay() //solange wir kein UI haben, einf als Text anzeigen
        //{
        //    string testDisplaytext = "";

        //    foreach (var card in App.AllFlashcards)
        //    {
        //        testDisplaytext += $"ID: {card.id}\n";
        //        testDisplaytext += $"Front: {card.textFront}\n";
        //        testDisplaytext += $"Back: {card.textBack}\n";
        //        testDisplaytext += "";
        //    }
        //    OutputTextbox.Text = testDisplaytext;
        //    Debug.Write(testDisplaytext);
        //}

    }
}
