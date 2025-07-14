using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard
{
    public class CreateFlashcardViewModel : PropertyChangedBase
    {
        public ICommand CloseCommand { get; set; }

        private LoadedSqlData _loadedSqlData;

        public CreateFlashcardViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;
            CloseCommand = new RelayCommand(CloseWindow);

            CreateSingleFlashcardViewModel = new CreateSingleFlashcardViewModel(_loadedSqlData);
        }

        private CreateSingleFlashcardViewModel createSingleFlashcardViewModel;
        public CreateSingleFlashcardViewModel CreateSingleFlashcardViewModel
        {
            get => createSingleFlashcardViewModel;
            set
            {
                createSingleFlashcardViewModel = value;
                OnPropertyChanged(nameof(CreateSingleFlashcardViewModel));   
            }
        }

        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
