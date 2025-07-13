using BfK_S_ApiProjekt.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels
{
    public class MainViewModel
    {
        private FlashcardTableViewModel _flashcardTableViewModel;
        private ShowFlashcardViewModel _showFlashcardViewModel;

        public ICommand CreateNewFlashcardCommand {  get; set; }

        public MainViewModel(FlashcardTableViewModel flashcardTableViewModel, ShowFlashcardViewModel showFlashcardViewModel)
        {
            _flashcardTableViewModel = flashcardTableViewModel;
            _showFlashcardViewModel = showFlashcardViewModel;

            CreateNewFlashcardCommand = new RelayCommand(OpenCreateNewFlashcard);
        }

        public FlashcardTableViewModel FlashcardTableViewModel
        {
            get => _flashcardTableViewModel;
        }
        public ShowFlashcardViewModel ShowFlashcardViewModel
        {
            get => _showFlashcardViewModel;
        }

        private void OpenCreateNewFlashcard(object para)
        {
            //TODO
        }
    }
}
