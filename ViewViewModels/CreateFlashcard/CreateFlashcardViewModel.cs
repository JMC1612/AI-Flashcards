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
        public ICommand ShowCreateSingleFlashcardCommand { get; set; }
        public ICommand ShowCreateMultipleFlashcardsCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private LoadedSqlData _loadedSqlData;

        public CreateFlashcardViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;

            ShowCreateSingleFlashcardCommand = new RelayCommand(ShowCreateSingleFlashcard);
            ShowCreateMultipleFlashcardsCommand = new RelayCommand(ShowCreateMultipleFlashcards);

            CloseCommand = new RelayCommand(CloseWindow);

            CreateSingleFlashcardViewModel = new CreateSingleFlashcardViewModel(_loadedSqlData);
            CreateMultipleFlashcardsGeminiViewModel = new CreateMultipleFlashcardsGeminiViewModel(_loadedSqlData);

            CreateSingleFlashcardIsVisible = true;
            CreateMultipleFlashcardsIsVisible = false;
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

        private CreateMultipleFlashcardsGeminiViewModel createMultipleFlashcardsGeminiViewModel;
        public CreateMultipleFlashcardsGeminiViewModel CreateMultipleFlashcardsGeminiViewModel
        {
            get => createMultipleFlashcardsGeminiViewModel;
            set
            {
                createMultipleFlashcardsGeminiViewModel = value;
                OnPropertyChanged(nameof(CreateMultipleFlashcardsGeminiViewModel));   
            }
        }

        private bool createSingleFlashcardIsVisible = true;
        public bool CreateSingleFlashcardIsVisible
        {
            get => createSingleFlashcardIsVisible;
            set
            {
                createSingleFlashcardIsVisible = value;
                OnPropertyChanged(nameof(CreateSingleFlashcardIsVisible));
            }
        }

        private bool createMultipleFlashcardsIsVisible = false;
        public bool CreateMultipleFlashcardsIsVisible
        {
            get => createMultipleFlashcardsIsVisible;
            set
            {
                createMultipleFlashcardsIsVisible = value;
                OnPropertyChanged(nameof(CreateMultipleFlashcardsIsVisible));
            }
        }

        private void ShowCreateSingleFlashcard(object para)
        {
            CreateMultipleFlashcardsIsVisible = false;
            CreateSingleFlashcardIsVisible = true;
        }

        private void ShowCreateMultipleFlashcards(object para)
        {
            CreateSingleFlashcardIsVisible = false;
            CreateMultipleFlashcardsIsVisible = true;
        }

        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
