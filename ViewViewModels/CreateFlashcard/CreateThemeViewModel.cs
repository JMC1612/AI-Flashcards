using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard
{
    public class CreateThemeViewModel : PropertyChangedBase
    {
        public ICommand CreateThemeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public CreateThemeViewModel()
        {
            CloseCommand = new RelayCommand(CloseWindow);
            CreateThemeCommand = new RelayCommand(CreateTheme);
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

        private void CreateTheme(object para)
        {
            if (TempName != string.Empty)
            {
                Theme newTheme = new()
                {
                    Name = TempName
                };
                SQLiteManager.InsertTheme(newTheme);

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
