using BfK_S_ApiProjekt.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfK_S_ApiProjekt.Items
{
    public class Theme : PropertyChangedBase
    {
        private int id = 0;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private ObservableCollection<Flashcard> flashcards = new();
        public ObservableCollection<Flashcard> Flashcards
        {
            get => flashcards;
            set
            {
                flashcards = value;
                OnPropertyChanged(nameof(Flashcards));
            }
        }

        public bool IsButtonsVisible
        {
            get
            {
                if (Id == 1 && Name == "unsorted")
                    return false;
                return true;
            }
        }
    }
}
