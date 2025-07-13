using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using BfK_S_ApiProjekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            _loadedSqlData.PropertyChanged += LoadedSqlData_PropertyChanged;
        }

        public ObservableCollection<Theme> Themes
        {
            get => _loadedSqlData.LoadSqlData;
            set
            {
                _loadedSqlData.LoadSqlData = value;
                OnPropertyChanged(nameof(Themes));
            }
        }

        private void LoadedSqlData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Themes));
        }
    }
}
