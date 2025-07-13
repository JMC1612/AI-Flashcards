using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels
{
    public class ShowFlashcardViewModel : PropertyChangedBase
    {
        public ICommand ChangeFlashcardSideCommand { get; set; }

        private LoadedSqlData _loadedSqlData;
        public ShowFlashcardViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;
            _loadedSqlData.PropertyChanged += LoadedSqlData_PropertyChanged;

            ChangeFlashcardSideCommand = new RelayCommand(ChangeFlashcardSide);
        }

        private void LoadedSqlData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedFlashcard));
            OnPropertyChanged(nameof(FlashcardFront));
            OnPropertyChanged(nameof(FlashcardBack));
            ShowedFlashcardSide = SelectedFlashcard.FrontText;
        }

        public Flashcard SelectedFlashcard
        {
            get
            {
                if (_loadedSqlData.SelectedItem is Flashcard selectedFlashcard)
                {
                    ShowFlashcard = true;
                    return selectedFlashcard;
                }
                ShowFlashcard = false;
                return null;
            }
        }

        
        private void ChangeFlashcardSide(object para)
        {
            if (ShowedFlashcardSide == SelectedFlashcard.FrontText)
            {
                ShowedFlashcardSide = SelectedFlashcard.BackText;
            }
            else
            {
                ShowedFlashcardSide = SelectedFlashcard.FrontText;
            }
        }

        public string FlashcardFront
        {
            get => SelectedFlashcard.FrontText;
        }

        public string FlashcardBack
        {
            get => SelectedFlashcard.BackText;
        }

        private bool showFlashcard = false;
        public bool ShowFlashcard
        {
            get => showFlashcard;
            set
            {
                showFlashcard = value;
                OnPropertyChanged(nameof(ShowFlashcard));
            }
        }

        private string showedFlashcardSide = string.Empty;
        public string ShowedFlashcardSide
        {
            get => showedFlashcardSide;
            set
            {
                showedFlashcardSide = value;
                OnPropertyChanged(nameof(ShowedFlashcardSide));
            }
        }
    }
}
