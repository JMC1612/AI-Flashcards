using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
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
using Microsoft.VisualBasic;

namespace BfK_S_ApiProjekt
{
    // BeSchumacher -> Hr Schumacher Github

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateLernkarten()
        {
            string outoutTxt = "";

            foreach(Flashcard lk in App.AllFlashcards)
            {
                outoutTxt += $"ID: {lk.id}\n";
                outoutTxt += $"ID: {lk.textFront}\n";
                outoutTxt += $"ID: {lk.textBack}\n";
                outoutTxt += "\n";
            }

            OutputTextbox.Text = outoutTxt;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var test = new Flashcard("vorne", "hinten");
            App.AllFlashcards.Add(test);

            AiCall();


            UpdateLernkarten();
        }

        private async void AiCall()
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={App.ApiKey}";
            Debug.WriteLine(url);

            var requestBody = new
            {
                contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = $@"Generiere 5 Karteikarten über das Thema ""{InputTextbox.Text}"" in diesem Format:
                            ;front_text: TEXT ;back_text: TEXT
                            antworte ab sofort nur in diesem Format ohne jemals davon abzuweichen.",

                        }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await App.internetClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();

                OutputTextbox.Text = responseJson.ToString();
            }
            else
            {
                OutputTextbox.Text = $"Fehler: {response.StatusCode} | " + await response.Content.ReadAsStringAsync();
            }
        }
    }

    public class GemeniRequest
    {
        public GemeniRequest()
        {
        }
    }
}
