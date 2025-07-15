using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard
{
    public class CreateMultipleFlashcardsGeminiViewModel : PropertyChangedBase
    {
        public ICommand CreateThemeCommand { get; set; }
        public ICommand CreateFlashcardsCommand { get; set; }
        public ICommand AnswerWithGeminiCommand { get; set; }

        private readonly LoadedSqlData _loadedSqlData;
        public CreateMultipleFlashcardsGeminiViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;

            CreateThemeCommand = new RelayCommand(CreateTheme);
            CreateFlashcardsCommand = new RelayCommand(CreateFlashcards);


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

        private int flashcardsCount = 0;
        public int FlashcardsCount
        {
            get => flashcardsCount;
            set
            {
                string x = value.ToString();
                if (int.TryParse(x, out int result))
                    flashcardsCount = result;
                OnPropertyChanged(nameof(FlashcardsCount));
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

        private void CreateFlashcards(object para)
        {
            if (SelectedTheme != null)
            {
                Task.Run(async () =>
                {
                    var generatedFlashcard = await Gemini.AiGenerateFlashcardsToTheme(FlashcardsCount, SelectedTheme.Name);
                    foreach (var flashcard in generatedFlashcard)
                    {
                        SQLiteManager.InsertFlashcard(flashcard, SelectedTheme.Id);
                    }
                    var flashcards = SQLiteManager.LoadFlashcardsForTheme(SelectedTheme.Id);

                    var targetTheme = Themes.FirstOrDefault(t => t.Id == SelectedTheme.Id);
                    if (targetTheme != null)
                    {
                        targetTheme.Flashcards = new ObservableCollection<Flashcard>(flashcards);
                    }
                });

                if (para is Window window)
                    window.Close();
            }
        }
    }
}
