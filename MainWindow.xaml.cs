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
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonRequiredAttribute = Newtonsoft.Json.JsonRequiredAttribute;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AiCall();
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
                            text = $@"Generiere {AmmountTextBox.Text} Karteikarten über das Thema ""{InputTextbox.Text}"" in exakt diesem Format

                            ;front_text: TEXT ;back_text: TEXT

                            beachte hierbei, dass jede Karteikarte in einer eigenen Zeile steht und ;front_text:  sowohl als auch ;back_text: beinhaltet.
                            antworte ab sofort nur in diesem Format ohne jemals davon abzuweichen.",

                        }
                    }
                }
            }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await App.internetClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseJsonDocument = JsonDocument.Parse(responseJson);

                string geminiTxt = Convert.ToString(responseJsonDocument.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text"));

                GenerateGeminiFlashcards(geminiTxt);
                
                UpdateFlashcardDisplay();
            }
            else
            {
                OutputTextbox.Text = $"Fehler: {response.StatusCode} | " + await response.Content.ReadAsStringAsync();
            }
        }

        // macht aus dem gemini generierten text Lernkarten :)
        public List<Flashcard> GenerateGeminiFlashcards(string geminiString)
        {
            List<Flashcard> geminiFlashcardList = new List<Flashcard>();

            var lines = geminiString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) { continue; }
                

                    string[] cardTextParts = line.Trim().Split(new[] { ";front_text:", ";back_text:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (cardTextParts.Length == 2)
                    {
                        geminiFlashcardList.Add(new Flashcard(cardTextParts[0].Trim(), cardTextParts[1].Trim()));
                    }
                
            }

            Debug.WriteLine("Generated " + geminiFlashcardList.Count + " gemini flashcards");
            return geminiFlashcardList;
        }

        public void UpdateFlashcardDisplay() //solange wir kein UI haben, einf als Text anzeigen
        {
            string testDisplaytext = "";

            foreach (var card in App.AllFlashcards)
            {
                testDisplaytext += $"ID: {card.id}\n";
                testDisplaytext += $"Front: {card.textFront}\n";
                testDisplaytext += $"Back: {card.textBack}\n";
                testDisplaytext += "";
            }

            OutputTextbox.Text = testDisplaytext;
            Debug.Write(testDisplaytext);
        }

    }
}
