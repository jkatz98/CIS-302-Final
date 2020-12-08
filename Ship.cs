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
        private int[] position;
        private Boolean sunk;

        public Ship(string s, int i)
        {
            this.name = s;
            this.length = i;
            this.currentLength = length;
            position = new int[length];
            this.sunk = false;
        }

        public void hit()
        {
            currentLength = currentLength - 1;
            if (currentLength == 0)
            {
                sunk = true;
            }
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

        public void setPosition(int[] position)
        {
            this.position = position;
        }

        public int[] getPosition()
        {
            return position;
        }

        public Boolean isSunk()
        {
            return sunk;
        }
    }
}
