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
        private int currentLength;
        private Boolean sunk;

        public Ship(string s, int i)
        {
            this.name = s;
            this.length = i;
            this.currentLength = length;
            this.sunk = false;
        }

        public string getName()
        {
            return name;
        }

        public int getLength()
        {
            return length;
        }

        public int getCurrentLength() {
            return currentLength;
        }

        public Boolean getSunk()
        {
            return sunk;
        }

        private void hit()
        {
            currentLength = currentLength - 1;
            if (currentLength == 0)
            {
                sunk = true;
            }
        }
    }
}
