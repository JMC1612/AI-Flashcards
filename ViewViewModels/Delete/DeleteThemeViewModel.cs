using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.Delete
{
    public class DeleteThemeViewModel : PropertyChangedBase
    {
        private Theme _theme;
        private readonly LoadedSqlData _loadedSqlData;

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public DeleteThemeViewModel(Theme theme, LoadedSqlData loadedSqlData)
        {
            _theme = theme;
            _loadedSqlData = loadedSqlData;

            ConfirmCommand = new RelayCommand(ConfirmDeletion);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private bool deleteAllFlashcardsInTheme = true;
        public bool DeleteAllFlashcardsInTheme
        {
            get => deleteAllFlashcardsInTheme;
            set
            {
                deleteAllFlashcardsInTheme = value;
                OnPropertyChanged(nameof(DeleteAllFlashcardsInTheme));
            }
        }

        private void ConfirmDeletion(object para) 
        {
            foreach (var flashcard in _theme.Flashcards)
            {
                if (DeleteAllFlashcardsInTheme)
                {
                    SQLiteManager.DeleteFlashcard(flashcard.ID);
                }
                else
                {
                    flashcard.ThemeId = 1;
                    SQLiteManager.UpdateFlashcard(flashcard);

                    var UnsortedTheme = _loadedSqlData.LoadSqlData.FirstOrDefault(t => t.Id == 1);
                    if (UnsortedTheme != null)
                        UnsortedTheme.Flashcards = SQLiteManager.LoadFlashcardsForTheme(1);
                }
            }

            SQLiteManager.DeleteTheme(_theme.Id);

            if (para is Window window)
                window.Close();
        }
        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
