using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfK_S_ApiProjekt.Models
{
    public class LoadedSqlData : PropertyChangedBase
    {
        private ObservableCollection<Theme> loadedSqlData = new();
        public ObservableCollection<Theme> LoadSqlData
        {
            get => loadedSqlData;
            set
            {
                loadedSqlData = value;
                OnPropertyChanged(nameof(LoadSqlData));
            }
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
    }
}
