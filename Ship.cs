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
        private int[] position;
        private Boolean sunk;

        public Ship(string s, int i)
        {
            this.name = s;
            this.length = i;
            position = new int[length];
            this.sunk = false;
        }

        public void hit()
        {
            length = length - 1;
            if (length == 0)
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

        public void setPosition(int[] position)
        {
            this.position = position;
        }

        public int[] getPosition()
        {
            return position;
        }

        public Boolean getSunk()
        {
            return sunk;
        }
    }
}
