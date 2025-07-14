using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.Delete
{
    public class DeleteFlashcardViewModel
    {
        private readonly Flashcard _flashcard;

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand {  get; set; }
        public DeleteFlashcardViewModel(Flashcard flashcard)
        {
            _flashcard = flashcard;

            ConfirmCommand = new RelayCommand(ConfirmDeletion);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private void ConfirmDeletion (object para)
        {
            SQLiteManager.DeleteFlashcard(_flashcard.ID);

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
