using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Game
{
    class Ship
    {
        private string name;
        private int length;
        private Boolean sunk;

        public Ship(string s, int i)
        {
            this.name = s;
            this.length = i;
        }

        private string getName()
        {
            return name;
        }

        private int getLength()
        {
            return length;
        }

        private Boolean getSunk()
        {
            return sunk;
        }

        private void hit()
        {
            length = length - 1;
            if (length == 0)
            {
                sunk = true;
            }
        }
    }
}
