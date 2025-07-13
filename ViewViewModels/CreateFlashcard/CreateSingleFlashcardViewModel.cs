using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard
{
    public class CreateSingleFlashcardViewModel : PropertyChangedBase
    {
        public ICommand CreateFlashcardCommand { get; set; }
        public ICommand AnswerWithGeminiCommand {  get; set; }

        private readonly LoadedSqlData _loadedSqlData;
        public CreateSingleFlashcardViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;
            CreateFlashcardCommand = new RelayCommand(CreateFlashcard);
            AnswerWithGeminiCommand = new RelayCommand(AnswertWithGemini);
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

        private void CreateFlashcard(object para)
        {
            Flashcard newFlashcard = new Flashcard()
            {
                FrontText = tempFrontText,
                BackText = tempBackText,
                ID = 0 //TODO
            };

            //TODO
        } 

        private void AnswertWithGemini(object para)
        {
            //TODO
        }
    }
}
