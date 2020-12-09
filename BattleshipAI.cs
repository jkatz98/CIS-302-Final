using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Game
{
    class BattleshipAI
    {

        public BattleshipAI()
        {

        }

        public int getAttack()
        {
            return 0;
        }

        public String intToDirection(int intDirection)
        {
            String direction = "";
            switch (intDirection)
            {
                case 1:
                    direction = "North";
                    break;
                case 2:
                    direction = "South";
                    break;
                case 3:
                    direction = "East";
                    break;
                case 4:
                    direction = "West";
                    break;
            }
            return direction;
        }
    }
}
