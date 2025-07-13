using BfK_S_ApiProjekt.GeneralClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfK_S_ApiProjekt.Items
{
    public class Flashcard : PropertyChangedBase
    {
        private int id = 0;
        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID));
                
            }
        }

        private string frontText = string.Empty;
        public string FrontText
        {
            get => frontText;
            set
            {
                frontText = value;
                OnPropertyChanged(nameof(FrontText));
            }
        }

        private string backText = string.Empty;
        public string BackText
        {
            get => backText;
            set
            {
                backText = value;
                OnPropertyChanged(nameof(BackText));
            }
        }
    }
}
