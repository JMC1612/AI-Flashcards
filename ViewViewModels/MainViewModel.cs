using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Models;
using BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard;
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

        private LoadedSqlData _loadedSqlData;

        public ICommand CreateNewFlashcardCommand {  get; set; }

        public MainViewModel(FlashcardTableViewModel flashcardTableViewModel, ShowFlashcardViewModel showFlashcardViewModel, LoadedSqlData loadedSqlData)
        {
            _flashcardTableViewModel = flashcardTableViewModel;
            _showFlashcardViewModel = showFlashcardViewModel;
            _loadedSqlData = loadedSqlData;

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
            var createFlashcardWindow = new CreateFlashcardView
            {
                DataContext = new CreateFlashcardViewModel(_loadedSqlData)
            };
            createFlashcardWindow.ShowDialog();
        }
    }
}
