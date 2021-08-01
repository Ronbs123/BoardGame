using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            FormMenu FormMemoryGameSettings = new FormMenu();
            FormMemoryGameSettings.ShowDialog();
            Game newGame = FormMemoryGameSettings.GetNewGame();
            FormGame formGame = new FormGame(newGame);
            formGame.ShowDialog();
        }
    }
}