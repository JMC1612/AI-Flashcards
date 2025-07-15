using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels
{
    public class EditFlashcardViewModel : PropertyChangedBase
    {
        readonly Flashcard _flashcard;
        readonly LoadedSqlData _loadedSqlData;

        public ICommand CreateThemeCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ICommand CloseCommand { get; set; }


        private Theme OriginalTheme { get; set; }

        public EditFlashcardViewModel(Flashcard flashcard, LoadedSqlData loadedSqlData)
        {
            _flashcard = flashcard;
            _loadedSqlData = loadedSqlData;

            GetFlashcard();
            TempSelectedTheme = loadedSqlData.LoadSqlData.FirstOrDefault(theme =>
                theme.Flashcards.Contains(flashcard));
            OriginalTheme = TempSelectedTheme;

            TempThemes = _loadedSqlData.LoadSqlData;

            CreateThemeCommand = new RelayCommand(CreateTheme);
            ResetCommand = new RelayCommand(Reset);
            SaveCommand = new RelayCommand(SaveFlashcard);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private ObservableCollection<Theme> tempThemes;
        public ObservableCollection<Theme> TempThemes
        {
            get => tempThemes;
            set
            {
                tempThemes = value;
                OnPropertyChanged(nameof(TempThemes));
            }
        }

        private Theme tempSelectedTheme;
        public Theme TempSelectedTheme
        {
            get => tempSelectedTheme;
            set
            {
                tempSelectedTheme = value;
                OnPropertyChanged(nameof(TempSelectedTheme));
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

        private void GetFlashcard()
        {
            TempFrontText = _flashcard.FrontText;
            TempBackText = _flashcard.BackText;
        }

        private void CreateTheme(object para)
        {
            var createThemeWindow = new CreateThemeView()
            {
                DataContext = new CreateThemeViewModel()
            };
            createThemeWindow.ShowDialog();

            _loadedSqlData.LoadSqlData = SQLiteManager.LoadThemes();
            TempThemes = _loadedSqlData.LoadSqlData;
        }

        private void Reset(object para)
        {
            GetFlashcard();
        }

        private void SaveFlashcard(object para)
        {
            if (TempFrontText != string.Empty && TempBackText != string.Empty && TempSelectedTheme != null)
            {
                _flashcard.FrontText = TempFrontText;
                _flashcard.BackText = TempBackText;
                _flashcard.ThemeId = TempSelectedTheme.Id;
                
                SQLiteManager.UpdateFlashcard(_flashcard);

                if (TempSelectedTheme.Id != OriginalTheme.Id)
                {
                    OriginalTheme.Flashcards = SQLiteManager.LoadFlashcardsForTheme(OriginalTheme.Id);
                    TempSelectedTheme.Flashcards = SQLiteManager.LoadFlashcardsForTheme(TempSelectedTheme.Id);
                }

                if (para is Window window)
                    window.Close();
            }
            
        }

        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
