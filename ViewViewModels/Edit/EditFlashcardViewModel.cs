using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
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

        public ICommand ResetCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public EditFlashcardViewModel(Flashcard flashcard)
        {
            _flashcard = flashcard;
            GetFlashcard();
            ResetCommand = new RelayCommand(Reset);
            SaveCommand = new RelayCommand(SaveFlashcard);
            CloseCommand = new RelayCommand(CloseWindow);
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

        private void Reset(object para)
        {
            GetFlashcard();
        }

        private void SaveFlashcard(object para)
        {
            if (TempFrontText != string.Empty && TempBackText != string.Empty)
            {
                _flashcard.FrontText = TempFrontText;
                _flashcard.BackText = TempBackText;
                SQLiteManager.UpdateFlashcard(_flashcard);

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
