using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.Edit
{
    public class EditThemeViewModel : PropertyChangedBase
    {
        private readonly Theme _theme;

        public ICommand ResetCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ICommand CloseCommand { get; set; }
        public EditThemeViewModel(Theme theme)
        {
            _theme = theme;

            ResetCommand = new RelayCommand(Reset);
            SaveCommand = new RelayCommand(SaveTheme);
            CloseCommand = new RelayCommand(CloseWindow);
            TempName = theme.Name; 
        }

        private string tempName = string.Empty;
        public string TempName
        {
            get => tempName;
            set
            {
                tempName = value;
                OnPropertyChanged(nameof(TempName));
            }
        }

        private void Reset(object para)
        {
            TempName = _theme.Name;
        }

        private void SaveTheme(object para)
        {
            if (TempName != string.Empty)
            {
                _theme.Name = TempName;
                SQLiteManager.UpdateTheme(_theme);

                if (para is Window window)
                    window.Close();
            }
        }

        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
