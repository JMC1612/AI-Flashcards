using BfK_S_ApiProjekt.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BfK_S_ApiProjekt.ViewViewModels.CreateFlashcard
{
    public class CreateFlashcardViewModel
    {
        public ICommand CloseCommand { get; set; }

        public CreateFlashcardViewModel()
        {
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow(object para)
        {
            if (para is Window window)
                window.Close();
        }
    }
}
