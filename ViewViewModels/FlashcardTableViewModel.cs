using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using BfK_S_ApiProjekt.ViewViewModels.Delete;
using BfK_S_ApiProjekt.ViewViewModels.Edit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels
{
    public class FlashcardTableViewModel : PropertyChangedBase
    {
        private readonly LoadedSqlData _loadedSqlData;

        public ICommand EditThemeCommand { get; set; }
        public ICommand DeleteThemeCommand { get; set; }
        public ICommand EditFlashcardCommand { get; set; }
        public ICommand DeleteFlashcardCommand { get; set; }

        public FlashcardTableViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;
            _loadedSqlData.PropertyChanged += LoadedSqlData_PropertyChanged;

            EditThemeCommand = new RelayCommand(EditTheme);
            DeleteThemeCommand = new RelayCommand(DeleteTheme);
            EditFlashcardCommand = new RelayCommand(EditFlashcard);
            DeleteFlashcardCommand = new RelayCommand(DeleteFlashcard);

            SQLiteManager.Initialize();
            Themes = SQLiteManager.LoadThemes();
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

        public object SelectedItem
        {
            get => _loadedSqlData.SelectedItem;
            set
            {
                if (value != null)
                {
                    _loadedSqlData.SelectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    if (value is Theme theme)
                    {
                        var flashcards = SQLiteManager.LoadFlashcardsForTheme(theme.Id);

                        var targetTheme = Themes.FirstOrDefault(t => t.Id == theme.Id);
                        if (targetTheme != null)
                        {
                            targetTheme.Flashcards = new ObservableCollection<Flashcard>(flashcards);
                        }
                    }
                }
            }
        }

        private void LoadedSqlData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Themes));
        }

        private void EditTheme(object para)
        {
            if (para is Theme theme)
            {
                var editThemeWindow = new EditThemeView()
                {
                    DataContext = new EditThemeViewModel(theme)
                };
                editThemeWindow.ShowDialog();
            }
        }

        private void DeleteTheme(object para)
        {
            if (para is Theme theme)
            {
                var deleteThemeWindow = new DeleteThemeView()
                {
                    DataContext = new DeleteThemeViewModel(theme, _loadedSqlData)
                };
                deleteThemeWindow.ShowDialog();

                Themes.Remove(theme);
            }
        }

        private void EditFlashcard(object para)
        {
            if (para is Flashcard flashcard)
            {
                var editFlashcardWindow = new EditFlashcardView()
                {
                    DataContext = new EditFlashcardViewModel(flashcard, _loadedSqlData)
                };
                editFlashcardWindow.ShowDialog();
            }
        }

        private void DeleteFlashcard(object para)
        {
            if (para is Flashcard flashcard)
            {
                var deleteFlashcardWindow = new DeleteFlashcardView()
                {
                    DataContext = new DeleteFlashcardViewModel(flashcard)
                };
                deleteFlashcardWindow.ShowDialog();

                var parentTheme = Themes.FirstOrDefault(theme =>
                    theme.Flashcards.Contains(flashcard));
                parentTheme.Flashcards = SQLiteManager.LoadFlashcardsForTheme(parentTheme.Id);
            }
        }
    }
}
