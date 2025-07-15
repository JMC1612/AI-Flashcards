using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BfK_S_ApiProjekt
{
    public class Gemini
    {
        public static async Task<Flashcard?> GenerateFlashcardFromQuestion(string frontText, string themeName)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={App.ApiKey}";

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
                        text = $@"Beantworte die folgende Frage möglichst präzise und informativ.

                        Gib ausschließlich die Antwort zurück – kein Format, keine Wiederholung der Frage, keine Einleitung. Keine explizite referenz das Thema.
                        
                        Thema: ""{themeName}""
                        Frage: ""{frontText}"""
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

                string backText = responseJsonDocument
                    .RootElement.GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text").ToString()
                    .Trim();

                return new Flashcard
                {
                    FrontText = frontText,
                    BackText = backText
                };
            }
            else
            {
                Debug.WriteLine($"Fehler: {response.StatusCode} | " + await response.Content.ReadAsStringAsync());
                return null;
            }
        }




        public static async Task<ObservableCollection<Flashcard>> AiGenerateFlashcardsToTheme(int ammount, string thema)
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
                            text = $@"Generiere {ammount.ToString()} Karteikarten über das Thema ""{thema}"" in exakt diesem Format

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

                return GenerateGeminiFlashcards(geminiTxt);
            }
            else
            {
                Debug.WriteLine($"Fehler: {response.StatusCode} | " + await response.Content.ReadAsStringAsync());
                return null;
            }
        }

        // macht aus dem gemini generierten text Lernkarten :)
        public static ObservableCollection<Flashcard> GenerateGeminiFlashcards(string geminiString)
        {
            var geminiFlashcardCollection = new ObservableCollection<Flashcard>();

            var lines = geminiString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] cardTextParts = line.Trim().Split(new[] { ";front_text:", ";back_text:" }, StringSplitOptions.RemoveEmptyEntries);
                if (cardTextParts.Length == 2)
                {
                    geminiFlashcardCollection.Add(new Flashcard
                    {
                        FrontText = cardTextParts[0].Trim(),
                        BackText = cardTextParts[1].Trim()
                        // ID bleibt leer (Default = 0)
                    });
                }
            }

            Debug.WriteLine($"Generated {geminiFlashcardCollection.Count} Gemini flashcards.");
            return geminiFlashcardCollection;
        }


    }
}
