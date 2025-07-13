using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfK_S_ApiProjekt.ViewViewModels
{
    public class FlashcardTableViewModel : PropertyChangedBase
    {
        private readonly LoadedSqlData _loadedSqlData;
        public FlashcardTableViewModel(LoadedSqlData loadedSqlData)
        {
            _loadedSqlData = loadedSqlData;
        }

        private ObservableCollection<Theme> themes = new()
        {
            new Theme()
            {
                Name = "Kategorie A",
                Id = 0,
                Flashcards = new ObservableCollection<Flashcard>
                {
                    new() { FrontText = "Eintrag A1", BackText = string.Empty, ID = 0 },
                    new() { FrontText = "Eintrag A2", BackText = string.Empty, ID = 1 },
                }
            },
            new Theme
            {
                Name = "Kategorie B",
                Id = 1,
                Flashcards = new ObservableCollection<Flashcard>
                {
                    new() { FrontText = "Eintrag B1", BackText = string.Empty, ID = 2 },
                    new() { FrontText = "Eintrag B2", BackText = string.Empty, ID = 3 },
                    new() { FrontText = "Eintrag B3", BackText = string.Empty, ID = 4 }
                }
            },
            new Theme
            {
                Name = "Kategorie C",
                Id = 2,
                Flashcards = new ObservableCollection<Flashcard>
                {
                    new() { FrontText = "Eintrag C1", BackText = string.Empty, ID = 5 }
                }
            }
        };
        public ObservableCollection<Theme> Themes
        {
            get => /*_loadedSqlData.LoadSqlData*/themes;
            set
            {
                /*_loadedSqlData.LoadSqlData*/
                themes = value;
                OnPropertyChanged(nameof(Themes));
            }
        }
    }
}
