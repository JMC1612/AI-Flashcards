﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfK_S_ApiProjekt
{
    public class FlashcardAlt
    {
        public int id = 0;
        public string textFront = "";
        public string textBack = "";

        public FlashcardAlt(string txtFront, string txtBack) {
        
            textFront = txtFront;
            textBack = txtBack;

            if (App.AllFlashcards.Count > 0)
            {
                id = App.AllFlashcards.Last().id + 1;
            } else
            {
                id = 0;
            }
            App.AllFlashcards.Add(this);
        }
    }
}
