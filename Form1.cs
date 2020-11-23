using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            newGame();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newgame_button_Click(object sender, EventArgs e)
        {
            newGame();
        }
        
        private void newGame() {
            
        }
    }
}
