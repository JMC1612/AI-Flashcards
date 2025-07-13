using BfK_S_ApiProjekt.GeneralClasses;
using BfK_S_ApiProjekt.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.Delete
{
    public class DeleteThemeViewModel
    {
        private readonly Theme _theme;

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommad { get; set; }
        public DeleteThemeViewModel(Theme theme)
        {
            _theme = theme;

            ConfirmCommand = new RelayCommand(ConfirmDeletion);
            CloseCommad = new RelayCommand(CloseWindow);
        }


        private void ConfirmDeletion(object para) 
        {
            //TODO
        }
        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
