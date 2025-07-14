using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using ModernWpf.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard
{
    public class CreateSingleFlashcardViewModel : PropertyChangedBase
    {
        public ICommand CreateThemeCommand { get; set; }
        public ICommand CreateFlashcardCommand { get; set; }
        public ICommand AnswerWithGeminiCommand {  get; set; }

        private readonly LoadedSqlData _loadedSqlData;
        public CreateSingleFlashcardViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;

            CreateThemeCommand = new RelayCommand(CreateTheme);
            CreateFlashcardCommand = new RelayCommand(CreateFlashcard);
            AnswerWithGeminiCommand = new RelayCommand(AnswertWithGemini);


            OnPropertyChanged(nameof(Themes));
            SelectedTheme = Themes[0];
        }

        public ObservableCollection<Theme> Themes
        {
            get => _loadedSqlData.LoadSqlData;
            set
            {
                _loadedSqlData.LoadSqlData = value;
                OnPropertyChanged(nameof(Themes));
            }
        }

        private Theme selectedTheme = null;
        public Theme SelectedTheme
        {
            get => selectedTheme;
            set
            {
                selectedTheme = value;
                OnPropertyChanged(nameof(SelectedTheme));
            }
        }

        private string tempFrontText = string.Empty;
        public string TempFrontText
        {
            get => tempFrontText;
            set
            {
                tempFrontText = value;
                OnPropertyChanged(nameof(TempFrontText));
            }
        }

        private string tempBackText = string.Empty;
        public string TempBackText
        {
            get => tempBackText;
            set
            {
                tempBackText = value;
                OnPropertyChanged(nameof(TempBackText));
            }
        }

        private void CreateTheme(object para)
        {
            var createThemeWindow = new CreateThemeView()
            {
                DataContext = new CreateThemeViewModel()
            };
            createThemeWindow.ShowDialog();

            Themes = SQLiteManager.LoadThemes();
        }

        private void CreateFlashcard(object para)
        {
            if (SelectedTheme != null && TempFrontText != string.Empty && TempBackText != string.Empty)
            {
                Flashcard newFlashcard = new Flashcard()
                {
                    FrontText = tempFrontText,
                    BackText = tempBackText
                };

                SQLiteManager.InsertFlashcard(newFlashcard, SelectedTheme.Id);

                var flashcards = SQLiteManager.LoadFlashcardsForTheme(SelectedTheme.Id);

                // Das entsprechende Theme in der Liste finden und aktualisieren
                var targetTheme = Themes.FirstOrDefault(t => t.Id == SelectedTheme.Id);
                if (targetTheme != null)
                {
                    targetTheme.Flashcards = new ObservableCollection<Flashcard>(flashcards);
                }

                if (para is Window window)
                    window.Close();
            }
        } 

        private void AnswertWithGemini(object para)
        {
            if (TempFrontText != string.Empty)
            {
                Task.Run(async () =>
                {
                    Flashcard generatedFlashcard = await Gemini.GenerateFlashcardFromQuestion(TempFrontText);
                    TempBackText = generatedFlashcard.BackText;
                });
            }
        }
    }
}
