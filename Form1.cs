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
        readonly String[] SHIP_NAMES = { "Carrier", "Battleship", "Destroyer", "Submarine", "Patrol Boat" };
        readonly int[] SHIP_LENGTHS = { 5, 4, 3, 3, 2 };

        private int placementCount;
        private Boolean setup;

        readonly Color UNKNOWN_CELL = Color.White;
        readonly Color MISS_CELL = Color.DodgerBlue;
        readonly Color SHIP_CELL = Color.Black;
        readonly Color HIT_CELL = Color.Red;
        readonly Color SUNK_CELL = Color.DarkRed;

        private Label[] cellsLocation = new Label[201];
        private Color[] cellsColor = new Color[201];

        private int shipStartCell;

        readonly int NUMBER_OF_SHIPS = 5;
        Ship[] player1Ships;
        Ship[] player2Ships;

        private String turn;
        Boolean winner;

        public Form1()
        {
            InitializeComponent();
            newGame();
        }

        private void exit_button_Click(object sender, EventArgs e) { this.Close(); }

        private void newgame_button_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void newGame()
        {
            winner = false;
            setup = true;
            turn = "Player 1";
            visibleDirectionControls(false);
            initializeShips();
            initializeCells();
            updateBoard();
            restoreLabels();
            shipStartCell = 0;
            placementCount = 0;
            placementInstructions(turn, 0);
        }

        private void visibleDirectionControls(Boolean enable)
        {
            lb_Direction.Visible = enable;
            cb_Direction.Visible = enable;
            bt_Direction.Visible = enable;
        }

        private void initializeShips()
        {
            player1Ships = new Ship[NUMBER_OF_SHIPS];
            player2Ships = new Ship[NUMBER_OF_SHIPS];
            for (int i = 0; i < NUMBER_OF_SHIPS; i++)
            {
                player1Ships[i] = new Ship(SHIP_NAMES[i], SHIP_LENGTHS[i]);
                player2Ships[i] = new Ship(SHIP_NAMES[i], SHIP_LENGTHS[i]);
            }
        }

        private void updateBoard() {
            for (int i = 1; i < cellsLocation.Length; i++)
            {
                if (turn.Equals("Player 1"))
                {
                    if (i <= 100)
                    {
                        cellsLocation[i].BackColor = cellsColor[i];
                    }
                    else
                    {
                        if (cellsColor[i].Equals(SHIP_CELL))
                        {
                            cellsLocation[i].BackColor = UNKNOWN_CELL;
                        }
                        else
                        {
                            cellsLocation[i].BackColor = cellsColor[i];
                        }
                    }
                }
                else if (turn.Equals("Player 2"))
                {
                    if (i > 100)
                    {
                        cellsLocation[i].BackColor = cellsColor[i];
                    }
                    else
                    {
                        if (cellsColor[i].Equals(SHIP_CELL))
                        {
                            cellsLocation[i].BackColor = UNKNOWN_CELL;
                        }
                        else
                        {
                            cellsLocation[i].BackColor = cellsColor[i];
                        }
                    }
                }
            }
        }

        private void clearBoard() {
            for (int i = 1; i < cellsLocation.Length; i++)
            {
                if (cellsColor[i].Equals(SHIP_CELL))
                {
                    cellsLocation[i].BackColor = UNKNOWN_CELL;

                }
                else {
                    cellsLocation[i].BackColor = cellsColor[i];
                }
            }
        }

        private void switchTurns()
        {
            if (winner == false) {
                if (turn.Equals("Player 1"))
                {
                    turn = "Player 2";
                }
                else if (turn.Equals("Player 2"))
                {
                    turn = "Player 1";
                }
                clearBoard();
                MessageBox.Show("It's " + turn + "'s Turn, press ok to continue.");
                updateBoard();
                if (setup == false)
                {
                    attackInstructions();
                }
            }
        }

        private void placementInstructions(String player, int placement) {
            if (player.Equals("Player 1") && placement >= NUMBER_OF_SHIPS)
            {
                switchTurns();
                placement = 0;
                this.placementCount = 0;
                placementInstructions(turn, 0);
            }
            else if (player.Equals("Player 2") && placement >= NUMBER_OF_SHIPS) {
                setup = false;
                attackInstructions();
            }
            if (setup) {
                if (shipStartCell == 0)
                {
                    output_label.Text = turn +" select the starting point for your " + SHIP_NAMES[placement] + " (" + SHIP_LENGTHS[placement] + " spaces).";
                }
                else {
                    output_label.Text = turn +" select a direction to place the rest of your " + SHIP_NAMES[placement] + " (" + SHIP_LENGTHS[placement] + " spaces).";
                }
            }
            this.placementCount = placement;
        }

        private void attackInstructions() {
            if (turn.Equals("Player 1")) {
                output_label.Text = "Select a cell on Player 2's board to attack!";
            } else if (turn.Equals("Player 2")){
                output_label.Text = "Select a cell on Player 1's board to attack!";
            }
        }

        private void playerBoardClick(int cell)
        {
            if (winner == false) {
                if (turn.Equals("Player 1") && setup && shipStartCell == 0)
                {
                    //Player 1 placing their Ships.
                    if (cellsColor[cell].Equals(UNKNOWN_CELL))
                    {
                        shipStartCell = cell;
                        cellsColor[cell] = Color.Yellow;
                        updateBoard();
                        visibleDirectionControls(true);
                    }
                    else
                    {
                        MessageBox.Show("Please select another starting point.");
                        placementInstructions(turn, placementCount);
                    }
                }
                else if (turn.Equals("Player 2") && setup == false)
                {
                    //Player 2 attacking Player 1's board.
                    attack(cell);
                    switchTurns();
                }
            }
        }

        private void AIBoardClick(int cell) {
            if (winner == false)
            {
                if (turn.Equals("Player 2") && setup && shipStartCell == 0)
                {
                    //Player 2 placing their Ships.
                    if (cellsColor[cell].Equals(UNKNOWN_CELL))
                    {
                        shipStartCell = cell;
                        cellsColor[cell] = Color.Yellow;
                        updateBoard();
                        visibleDirectionControls(true);
                    }
                    else
                    {
                        MessageBox.Show("Please select another starting point.");
                        placementInstructions(turn, placementCount);
                    }
                }
                else if (turn.Equals("Player 1") && setup == false)
                {
                    //Player 1 attacking Player 2's board.
                    attack(cell);
                    switchTurns();
                }
            }
        }

        private void bt_Direction_Click(object sender, EventArgs e)
        {
            String direction = cb_Direction.Text;
            visibleDirectionControls(false);
            if (direction.Equals("North") || direction.Equals("South") || direction.Equals("East") || direction.Equals("West"))
            {
                if (shipPlacementPossible(shipStartCell, direction))
                {
                    placeShip(shipStartCell, direction);
                    shipStartCell = 0;
                    placementInstructions(turn, placementCount + 1);
                }
                else {
                    MessageBox.Show("That placement is impossible.");
                    cellsColor[shipStartCell] = UNKNOWN_CELL;
                    updateBoard();
                    shipStartCell = 0;
                    placementInstructions(turn, placementCount);
                }
            }
            else {
                MessageBox.Show("That placement is impossible.");
                cellsColor[shipStartCell] = UNKNOWN_CELL;
                updateBoard();
                shipStartCell = 0;
                placementInstructions(turn, placementCount);
            }
        }

        private Boolean shipPlacementPossible(int shipStartingCell, String direction) {
            int increment = directionIncrement(direction);
            int shipEndingCell = shipStartingCell + (increment * (SHIP_LENGTHS[placementCount] - 1));
            //Ensure the ships stay in range / in the same row.
            int row = (shipStartingCell / 10) * 10;
            switch (direction)
            {
                case "North":
                    if (shipStartingCell <= 100)
                    {
                        //Player 1's Board.
                        if (shipEndingCell < 1)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //Player 2's Board.
                        if (shipEndingCell < 101)
                        {
                            return false;
                        }
                    }
                    break;
                case "South":
                    if (shipStartingCell <= 100)
                    {
                        //Player 1's Board.
                        if (shipEndingCell > 100)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //Player 2's Board.
                        if (shipEndingCell > 200)
                        {
                            return false;
                        }
                    }
                    break;
                case "East":
                    if (shipEndingCell - 1 >= row + 10)
                    {
                        return false;
                    }
                    break;
                case "West":
                    if (shipEndingCell - 1 < row)
                    {
                        return false;
                    }
                    break;
            }
            //Ensure ships don't overlap.
            for (int i = 0; i < SHIP_LENGTHS[placementCount]; i++)
            {
                int cell = shipStartingCell + (increment * i);
                if (cellsColor[cell].Equals(SHIP_CELL))
                {
                    return false;
                }
            }
            return true;
        }

        private void placeShip(int startingCell, String direction) {
            if (shipPlacementPossible(startingCell, direction)) {
                int increment = directionIncrement(direction);
                int[] shipPositions = new int[SHIP_LENGTHS[placementCount]];
                for (int i = 0; i < SHIP_LENGTHS[placementCount]; i++)
                {
                    int cell = shipStartCell + (increment * i);
                    cellsColor[cell] = SHIP_CELL;
                    shipPositions[i] = cell;
                }
                if (turn.Equals("Player 1"))
                {
                    player1Ships[placementCount].setPosition(shipPositions);
                }
                else if (turn.Equals("Player 2"))
                {
                    player2Ships[placementCount].setPosition(shipPositions);
                }
                updateBoard();
            }
        }

        private int directionIncrement(String direction)
        {
            int increment = 0;
            switch (direction)
            {
                case "North":
                    increment = -10;
                    break;
                case "South":
                    increment = 10;
                    break;
                case "East":
                    increment = 1;
                    break;
                case "West":
                    increment = -1;
                    break;
            }
            return increment;
        }

        private void attack(int cell) {
            if (cellsColor[cell].Equals(UNKNOWN_CELL) || cellsColor[cell].Equals(SHIP_CELL))
            {
                if (cellsColor[cell].Equals(SHIP_CELL))
                {
                    cellsColor[cell] = HIT_CELL;
                    output_label.Text = turn + " hit a ship!";
                    shipHit(cell);
                }
                else
                {
                    cellsColor[cell] = MISS_CELL;
                    output_label.Text = turn+" missed!";
                }
                updateBoard();
                checkWinner();
                if (winner) {
                    
                }
            }
        }

        private void shipHit(int cell) {
            for (int i = 0; i < NUMBER_OF_SHIPS; i++)
            {
                for (int j = 0; j < player1Ships[i].getLength(); j++)
                {
                    if ((player1Ships[i].getPosition())[j] == cell)
                    {
                        player1Ships[i].hit();
                        if (player1Ships[i].isSunk())
                        {
                            int[] sunk = new int[player1Ships[i].getLength()];
                            sunk = player1Ships[i].getPosition();
                            for (int k = 0; k < sunk.Length; k++)
                            {
                                cellsColor[sunk[k]] = SUNK_CELL;
                            }
                            updatePlayerLabels(player1Ships[i]);
                            output_label.Text = turn + " sunk a ship!";
                        }
                            break;
                    }
                }
            }
            for (int i = 0; i < NUMBER_OF_SHIPS; i++)
            {
                for (int j = 0; j < player2Ships[i].getLength(); j++)
                {
                    if ((player2Ships[i].getPosition())[j] == cell)
                    {
                        player2Ships[i].hit();
                        if (player2Ships[i].isSunk())
                        {
                            int[] sunk = new int[player2Ships[i].getLength()];
                            sunk = player2Ships[i].getPosition();
                            for (int k = 0; k < sunk.Length; k++)
                            {
                                cellsColor[sunk[k]] = SUNK_CELL;
                            }
                            updatePlayer2Labels(player2Ships[i]);
                            output_label.Text = turn + " sunk a ship!";
                        }
                        break;
                    }
                }
            }
        }

        private void checkWinner() {
            int sunk = 0;
            for (int i = 0; i < NUMBER_OF_SHIPS; i++) {
                if (player1Ships[i].isSunk())
                {
                    sunk++;
                }
                else {
                    break;
                }
            }
            if (sunk == NUMBER_OF_SHIPS)
            {
                winner = true;
                output_label.Text = "Player 2 wins!";
            }
            else {
                sunk = 0;
                for (int i = 0; i < NUMBER_OF_SHIPS; i++)
                {
                    if (player2Ships[i].isSunk())
                    {
                        sunk++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (sunk == NUMBER_OF_SHIPS)
                {
                    winner = true;
                    output_label.Text = "Player 1 wins!";
                }
            }
        }

        private void updatePlayerLabels(Ship sunkShip)
        {
            if (sunkShip.getName() == "Carrier")
            {
                pCarrier_label.ForeColor = Color.Red;
            }
            else if (sunkShip.getName() == "Battleship")
            {
                pBattleship_label.ForeColor = Color.Red;
            }
            else if (sunkShip.getName() == "Destroyer")
            {
                pDestroyer_label.ForeColor = Color.Red;

            }
            else if (sunkShip.getName() == "Submarine")
            {
                pSubmarine_label.ForeColor = Color.Red;

            }
            else if (sunkShip.getName() == "Patrol Boat")
            {
                pPatrol_label.ForeColor = Color.Red;

            }
        }

        private void updatePlayer2Labels(Ship sunkShip)
        {
            if (sunkShip.getName() == "Carrier")
            {
                aiCarrier_label.ForeColor = Color.Red;
            }
            else if (sunkShip.getName() == "Battleship")
            {
                aiBattleship_label.ForeColor = Color.Red;
            }
            else if (sunkShip.getName() == "Destroyer")
            {
                aiDestroyer_label.ForeColor = Color.Red;

            }
            else if (sunkShip.getName() == "Submarine")
            {
                aiSubmarine_label.ForeColor = Color.Red;

            }
            else if (sunkShip.getName() == "Patrol Boat")
            {
                aiPatrol_label.ForeColor = Color.Red;

            }
        }

        private void restoreLabels() {
            pCarrier_label.ForeColor = Color.Black;
            pBattleship_label.ForeColor = Color.Black;
            pDestroyer_label.ForeColor = Color.Black;
            pSubmarine_label.ForeColor = Color.Black;
            pPatrol_label.ForeColor = Color.Black;
            aiCarrier_label.ForeColor = Color.Black;
            aiBattleship_label.ForeColor = Color.Black;
            aiDestroyer_label.ForeColor = Color.Black;
            aiSubmarine_label.ForeColor = Color.Black;
            aiPatrol_label.ForeColor = Color.Black;
        }

        private void initializeCells()
        {
            for (int i = 1; i < cellsColor.Length; i++)
            {
                cellsColor[i] = UNKNOWN_CELL;
            }
            cellsLocation[1] = this.pA1;
            cellsLocation[2] = this.pA2;
            cellsLocation[3] = this.pA3;
            cellsLocation[4] = this.pA4;
            cellsLocation[5] = this.pA5;
            cellsLocation[6] = this.pA6;
            cellsLocation[7] = this.pA7;
            cellsLocation[8] = this.pA8;
            cellsLocation[9] = this.pA9;
            cellsLocation[10] = this.pA10;
            cellsLocation[11] = this.pB1;
            cellsLocation[12] = this.pB2;
            cellsLocation[13] = this.pB3;
            cellsLocation[14] = this.pB4;
            cellsLocation[15] = this.pB5;
            cellsLocation[16] = this.pB6;
            cellsLocation[17] = this.pB7;
            cellsLocation[18] = this.pB8;
            cellsLocation[19] = this.pB9;
            cellsLocation[20] = this.pB10;
            cellsLocation[21] = this.pC1;
            cellsLocation[22] = this.pC2;
            cellsLocation[23] = this.pC3;
            cellsLocation[24] = this.pC4;
            cellsLocation[25] = this.pC5;
            cellsLocation[26] = this.pC6;
            cellsLocation[27] = this.pC7;
            cellsLocation[28] = this.pC8;
            cellsLocation[29] = this.pC9;
            cellsLocation[30] = this.pC10;
            cellsLocation[31] = this.pD1;
            cellsLocation[32] = this.pD2;
            cellsLocation[33] = this.pD3;
            cellsLocation[34] = this.pD4;
            cellsLocation[35] = this.pD5;
            cellsLocation[36] = this.pD6;
            cellsLocation[37] = this.pD7;
            cellsLocation[38] = this.pD8;
            cellsLocation[39] = this.pD9;
            cellsLocation[40] = this.pD10;
            cellsLocation[41] = this.pE1;
            cellsLocation[42] = this.pE2;
            cellsLocation[43] = this.pE3;
            cellsLocation[44] = this.pE4;
            cellsLocation[45] = this.pE5;
            cellsLocation[46] = this.pE6;
            cellsLocation[47] = this.pE7;
            cellsLocation[48] = this.pE8;
            cellsLocation[49] = this.pE9;
            cellsLocation[50] = this.pE10;
            cellsLocation[51] = this.pF1;
            cellsLocation[52] = this.pF2;
            cellsLocation[53] = this.pF3;
            cellsLocation[54] = this.pF4;
            cellsLocation[55] = this.pF5;
            cellsLocation[56] = this.pF6;
            cellsLocation[57] = this.pF7;
            cellsLocation[58] = this.pF8;
            cellsLocation[59] = this.pF9;
            cellsLocation[60] = this.pF10;
            cellsLocation[61] = this.pG1;
            cellsLocation[62] = this.pG2;
            cellsLocation[63] = this.pG3;
            cellsLocation[64] = this.pG4;
            cellsLocation[65] = this.pG5;
            cellsLocation[66] = this.pG6;
            cellsLocation[67] = this.pG7;
            cellsLocation[68] = this.pG8;
            cellsLocation[69] = this.pG9;
            cellsLocation[70] = this.pG10;
            cellsLocation[71] = this.pH1;
            cellsLocation[72] = this.pH2;
            cellsLocation[73] = this.pH3;
            cellsLocation[74] = this.pH4;
            cellsLocation[75] = this.pH5;
            cellsLocation[76] = this.pH6;
            cellsLocation[77] = this.pH7;
            cellsLocation[78] = this.pH8;
            cellsLocation[79] = this.pH9;
            cellsLocation[80] = this.pH10;
            cellsLocation[81] = this.pI1;
            cellsLocation[82] = this.pI2;
            cellsLocation[83] = this.pI3;
            cellsLocation[84] = this.pI4;
            cellsLocation[85] = this.pI5;
            cellsLocation[86] = this.pI6;
            cellsLocation[87] = this.pI7;
            cellsLocation[88] = this.pI8;
            cellsLocation[89] = this.pI9;
            cellsLocation[90] = this.pI10;
            cellsLocation[91] = this.pJ1;
            cellsLocation[92] = this.pJ2;
            cellsLocation[93] = this.pJ3;
            cellsLocation[94] = this.pJ4;
            cellsLocation[95] = this.pJ5;
            cellsLocation[96] = this.pJ6;
            cellsLocation[97] = this.pJ7;
            cellsLocation[98] = this.pJ8;
            cellsLocation[99] = this.pJ9;
            cellsLocation[100] = this.pJ10;
            cellsLocation[101] = this.aiA1;
            cellsLocation[102] = this.aiA2;
            cellsLocation[103] = this.aiA3;
            cellsLocation[104] = this.aiA4;
            cellsLocation[105] = this.aiA5;
            cellsLocation[106] = this.aiA6;
            cellsLocation[107] = this.aiA7;
            cellsLocation[108] = this.aiA8;
            cellsLocation[109] = this.aiA9;
            cellsLocation[110] = this.aiA10;
            cellsLocation[111] = this.aiB1;
            cellsLocation[112] = this.aiB2;
            cellsLocation[113] = this.aiB3;
            cellsLocation[114] = this.aiB4;
            cellsLocation[115] = this.aiB5;
            cellsLocation[116] = this.aiB6;
            cellsLocation[117] = this.aiB7;
            cellsLocation[118] = this.aiB8;
            cellsLocation[119] = this.aiB9;
            cellsLocation[120] = this.aiB10;
            cellsLocation[121] = this.aiC1;
            cellsLocation[122] = this.aiC2;
            cellsLocation[123] = this.aiC3;
            cellsLocation[124] = this.aiC4;
            cellsLocation[125] = this.aiC5;
            cellsLocation[126] = this.aiC6;
            cellsLocation[127] = this.aiC7;
            cellsLocation[128] = this.aiC8;
            cellsLocation[129] = this.aiC9;
            cellsLocation[130] = this.aiC10;
            cellsLocation[131] = this.aiD1;
            cellsLocation[132] = this.aiD2;
            cellsLocation[133] = this.aiD3;
            cellsLocation[134] = this.aiD4;
            cellsLocation[135] = this.aiD5;
            cellsLocation[136] = this.aiD6;
            cellsLocation[137] = this.aiD7;
            cellsLocation[138] = this.aiD8;
            cellsLocation[139] = this.aiD9;
            cellsLocation[140] = this.aiD10;
            cellsLocation[141] = this.aiE1;
            cellsLocation[142] = this.aiE2;
            cellsLocation[143] = this.aiE3;
            cellsLocation[144] = this.aiE4;
            cellsLocation[145] = this.aiE5;
            cellsLocation[146] = this.aiE6;
            cellsLocation[147] = this.aiE7;
            cellsLocation[148] = this.aiE8;
            cellsLocation[149] = this.aiE9;
            cellsLocation[150] = this.aiE10;
            cellsLocation[151] = this.aiF1;
            cellsLocation[152] = this.aiF2;
            cellsLocation[153] = this.aiF3;
            cellsLocation[154] = this.aiF4;
            cellsLocation[155] = this.aiF5;
            cellsLocation[156] = this.aiF6;
            cellsLocation[157] = this.aiF7;
            cellsLocation[158] = this.aiF8;
            cellsLocation[159] = this.aiF9;
            cellsLocation[160] = this.aiF10;
            cellsLocation[161] = this.aiG1;
            cellsLocation[162] = this.aiG2;
            cellsLocation[163] = this.aiG3;
            cellsLocation[164] = this.aiG4;
            cellsLocation[165] = this.aiG5;
            cellsLocation[166] = this.aiG6;
            cellsLocation[167] = this.aiG7;
            cellsLocation[168] = this.aiG8;
            cellsLocation[169] = this.aiG9;
            cellsLocation[170] = this.aiG10;
            cellsLocation[171] = this.aiH1;
            cellsLocation[172] = this.aiH2;
            cellsLocation[173] = this.aiH3;
            cellsLocation[174] = this.aiH4;
            cellsLocation[175] = this.aiH5;
            cellsLocation[176] = this.aiH6;
            cellsLocation[177] = this.aiH7;
            cellsLocation[178] = this.aiH8;
            cellsLocation[179] = this.aiH9;
            cellsLocation[180] = this.aiH10;
            cellsLocation[181] = this.aiI1;
            cellsLocation[182] = this.aiI2;
            cellsLocation[183] = this.aiI3;
            cellsLocation[184] = this.aiI4;
            cellsLocation[185] = this.aiI5;
            cellsLocation[186] = this.aiI6;
            cellsLocation[187] = this.aiI7;
            cellsLocation[188] = this.aiI8;
            cellsLocation[189] = this.aiI9;
            cellsLocation[190] = this.aiI10;
            cellsLocation[191] = this.aiJ1;
            cellsLocation[192] = this.aiJ2;
            cellsLocation[193] = this.aiJ3;
            cellsLocation[194] = this.aiJ4;
            cellsLocation[195] = this.aiJ5;
            cellsLocation[196] = this.aiJ6;
            cellsLocation[197] = this.aiJ7;
            cellsLocation[198] = this.aiJ8;
            cellsLocation[199] = this.aiJ9;
            cellsLocation[200] = this.aiJ10;
        }

        private void pA1_Click(object sender, EventArgs e) { playerBoardClick(1); }
        private void pA2_Click(object sender, EventArgs e) { playerBoardClick(2); }
        private void pA3_Click(object sender, EventArgs e) { playerBoardClick(3); }
        private void pA4_Click(object sender, EventArgs e) { playerBoardClick(4); }
        private void pA5_Click(object sender, EventArgs e) { playerBoardClick(5); }
        private void pA6_Click(object sender, EventArgs e) { playerBoardClick(6); }
        private void pA7_Click(object sender, EventArgs e) { playerBoardClick(7); }
        private void pA8_Click(object sender, EventArgs e) { playerBoardClick(8); }
        private void pA9_Click(object sender, EventArgs e) { playerBoardClick(9); }
        private void pA10_Click(object sender, EventArgs e) { playerBoardClick(10); }
        private void pB1_Click(object sender, EventArgs e) { playerBoardClick(11); }
        private void pB2_Click(object sender, EventArgs e) { playerBoardClick(12); }
        private void pB3_Click(object sender, EventArgs e) { playerBoardClick(13); }
        private void pB4_Click(object sender, EventArgs e) { playerBoardClick(14); }
        private void pB5_Click(object sender, EventArgs e) { playerBoardClick(15); }
        private void pB6_Click(object sender, EventArgs e) { playerBoardClick(16); }
        private void pB7_Click(object sender, EventArgs e) { playerBoardClick(17); }
        private void pB8_Click(object sender, EventArgs e) { playerBoardClick(18); }
        private void pB9_Click(object sender, EventArgs e) { playerBoardClick(19); }
        private void pB10_Click(object sender, EventArgs e) { playerBoardClick(20); }
        private void pC1_Click(object sender, EventArgs e) { playerBoardClick(21); }
        private void pC2_Click(object sender, EventArgs e) { playerBoardClick(22); }
        private void pC3_Click(object sender, EventArgs e) { playerBoardClick(23); }
        private void pC4_Click(object sender, EventArgs e) { playerBoardClick(24); }
        private void pC5_Click(object sender, EventArgs e) { playerBoardClick(25); }
        private void pC6_Click(object sender, EventArgs e) { playerBoardClick(26); }
        private void pC7_Click(object sender, EventArgs e) { playerBoardClick(27); }
        private void pC8_Click(object sender, EventArgs e) { playerBoardClick(28); }
        private void pC9_Click(object sender, EventArgs e) { playerBoardClick(29); }
        private void pC10_Click(object sender, EventArgs e) { playerBoardClick(30); }
        private void pD1_Click(object sender, EventArgs e) { playerBoardClick(31); }
        private void pD2_Click(object sender, EventArgs e) { playerBoardClick(32); }
        private void pD3_Click(object sender, EventArgs e) { playerBoardClick(33); }
        private void pD4_Click(object sender, EventArgs e) { playerBoardClick(34); }
        private void pD5_Click(object sender, EventArgs e) { playerBoardClick(35); }
        private void pD6_Click(object sender, EventArgs e) { playerBoardClick(36); }
        private void pD7_Click(object sender, EventArgs e) { playerBoardClick(37); }
        private void pD8_Click(object sender, EventArgs e) { playerBoardClick(38); }
        private void pD9_Click(object sender, EventArgs e) { playerBoardClick(39); }
        private void pD10_Click(object sender, EventArgs e) { playerBoardClick(40); }
        private void pE1_Click(object sender, EventArgs e) { playerBoardClick(41); }
        private void pE2_Click(object sender, EventArgs e) { playerBoardClick(42); }
        private void pE3_Click(object sender, EventArgs e) { playerBoardClick(43); }
        private void pE4_Click(object sender, EventArgs e) { playerBoardClick(44); }
        private void pE5_Click(object sender, EventArgs e) { playerBoardClick(45); }
        private void pE6_Click(object sender, EventArgs e) { playerBoardClick(46); }
        private void pE7_Click(object sender, EventArgs e) { playerBoardClick(47); }
        private void pE8_Click(object sender, EventArgs e) { playerBoardClick(48); }
        private void pE9_Click(object sender, EventArgs e) { playerBoardClick(49); }
        private void pE10_Click(object sender, EventArgs e) { playerBoardClick(50); }
        private void pF1_Click(object sender, EventArgs e) { playerBoardClick(51); }
        private void pF2_Click(object sender, EventArgs e) { playerBoardClick(52); }
        private void pF3_Click(object sender, EventArgs e) { playerBoardClick(53); }
        private void pF4_Click(object sender, EventArgs e) { playerBoardClick(54); }
        private void pF5_Click(object sender, EventArgs e) { playerBoardClick(55); }
        private void pF6_Click(object sender, EventArgs e) { playerBoardClick(56); }
        private void pF7_Click(object sender, EventArgs e) { playerBoardClick(57); }
        private void pF8_Click(object sender, EventArgs e) { playerBoardClick(58); }
        private void pF9_Click(object sender, EventArgs e) { playerBoardClick(59); }
        private void pF10_Click(object sender, EventArgs e) { playerBoardClick(60); }
        private void pG1_Click(object sender, EventArgs e) { playerBoardClick(61); }
        private void pG2_Click(object sender, EventArgs e) { playerBoardClick(62); }
        private void pG3_Click(object sender, EventArgs e) { playerBoardClick(63); }
        private void pG4_Click(object sender, EventArgs e) { playerBoardClick(64); }
        private void pG5_Click(object sender, EventArgs e) { playerBoardClick(65); }
        private void pG6_Click(object sender, EventArgs e) { playerBoardClick(66); }
        private void pG7_Click(object sender, EventArgs e) { playerBoardClick(67); }
        private void pG8_Click(object sender, EventArgs e) { playerBoardClick(68); }
        private void pG9_Click(object sender, EventArgs e) { playerBoardClick(69); }
        private void pG10_Click(object sender, EventArgs e) { playerBoardClick(70); }
        private void pH1_Click(object sender, EventArgs e) { playerBoardClick(71); }
        private void pH2_Click(object sender, EventArgs e) { playerBoardClick(72); }
        private void pH3_Click(object sender, EventArgs e) { playerBoardClick(73); }
        private void pH4_Click(object sender, EventArgs e) { playerBoardClick(74); }
        private void pH5_Click(object sender, EventArgs e) { playerBoardClick(75); }
        private void pH6_Click(object sender, EventArgs e) { playerBoardClick(76); }
        private void pH7_Click(object sender, EventArgs e) { playerBoardClick(77); }
        private void pH8_Click(object sender, EventArgs e) { playerBoardClick(78); }
        private void pH9_Click(object sender, EventArgs e) { playerBoardClick(79); }
        private void pH10_Click(object sender, EventArgs e) { playerBoardClick(80); }
        private void pI1_Click(object sender, EventArgs e) { playerBoardClick(81); }
        private void pI2_Click(object sender, EventArgs e) { playerBoardClick(82); }
        private void pI3_Click(object sender, EventArgs e) { playerBoardClick(83); }
        private void pI4_Click(object sender, EventArgs e) { playerBoardClick(84); }
        private void pI5_Click(object sender, EventArgs e) { playerBoardClick(85); }
        private void pI6_Click(object sender, EventArgs e) { playerBoardClick(86); }
        private void pI7_Click(object sender, EventArgs e) { playerBoardClick(87); }
        private void pI8_Click(object sender, EventArgs e) { playerBoardClick(88); }
        private void pI9_Click(object sender, EventArgs e) { playerBoardClick(89); }
        private void pI10_Click(object sender, EventArgs e) { playerBoardClick(90); }
        private void pJ1_Click(object sender, EventArgs e) { playerBoardClick(91); }
        private void pJ2_Click(object sender, EventArgs e) { playerBoardClick(92); }
        private void pJ3_Click(object sender, EventArgs e) { playerBoardClick(93); }
        private void pJ4_Click(object sender, EventArgs e) { playerBoardClick(94); }
        private void pJ5_Click(object sender, EventArgs e) { playerBoardClick(95); }
        private void pJ6_Click(object sender, EventArgs e) { playerBoardClick(96); }
        private void pJ7_Click(object sender, EventArgs e) { playerBoardClick(97); }
        private void pJ8_Click(object sender, EventArgs e) { playerBoardClick(98); }
        private void pJ9_Click(object sender, EventArgs e) { playerBoardClick(99); }
        private void pJ10_Click(object sender, EventArgs e) { playerBoardClick(100); }

        private void aiA1_Click(object sender, EventArgs e) { AIBoardClick(101); }
        private void aiA2_Click(object sender, EventArgs e) { AIBoardClick(102); }
        private void aiA3_Click(object sender, EventArgs e) { AIBoardClick(103); }
        private void aiA4_Click(object sender, EventArgs e) { AIBoardClick(104); }
        private void aiA5_Click(object sender, EventArgs e) { AIBoardClick(105); }
        private void aiA6_Click(object sender, EventArgs e) { AIBoardClick(106); }
        private void aiA7_Click(object sender, EventArgs e) { AIBoardClick(107); }
        private void aiA8_Click(object sender, EventArgs e) { AIBoardClick(108); }
        private void aiA9_Click(object sender, EventArgs e) { AIBoardClick(109); }
        private void aiA10_Click(object sender, EventArgs e) { AIBoardClick(110); }
        private void aiB1_Click(object sender, EventArgs e) { AIBoardClick(111); }
        private void aiB2_Click(object sender, EventArgs e) { AIBoardClick(112); }
        private void aiB3_Click(object sender, EventArgs e) { AIBoardClick(113); }
        private void aiB4_Click(object sender, EventArgs e) { AIBoardClick(114); }
        private void aiB5_Click(object sender, EventArgs e) { AIBoardClick(115); }
        private void aiB6_Click(object sender, EventArgs e) { AIBoardClick(116); }
        private void aiB7_Click(object sender, EventArgs e) { AIBoardClick(117); }
        private void aiB8_Click(object sender, EventArgs e) { AIBoardClick(118); }
        private void aiB9_Click(object sender, EventArgs e) { AIBoardClick(119); }
        private void aiB10_Click(object sender, EventArgs e) { AIBoardClick(120); }
        private void aiC1_Click(object sender, EventArgs e) { AIBoardClick(121); }
        private void aiC2_Click(object sender, EventArgs e) { AIBoardClick(122); }
        private void aiC3_Click(object sender, EventArgs e) { AIBoardClick(123); }
        private void aiC4_Click(object sender, EventArgs e) { AIBoardClick(124); }
        private void aiC5_Click(object sender, EventArgs e) { AIBoardClick(125); }
        private void aiC6_Click(object sender, EventArgs e) { AIBoardClick(126); }
        private void aiC7_Click(object sender, EventArgs e) { AIBoardClick(127); }
        private void aiC8_Click(object sender, EventArgs e) { AIBoardClick(128); }
        private void aiC9_Click(object sender, EventArgs e) { AIBoardClick(129); }
        private void aiC10_Click(object sender, EventArgs e) { AIBoardClick(130); }
        private void aiD1_Click(object sender, EventArgs e) { AIBoardClick(131); }
        private void aiD2_Click(object sender, EventArgs e) { AIBoardClick(132); }
        private void aiD3_Click(object sender, EventArgs e) { AIBoardClick(133); }
        private void aiD4_Click(object sender, EventArgs e) { AIBoardClick(134); }
        private void aiD5_Click(object sender, EventArgs e) { AIBoardClick(135); }
        private void aiD6_Click(object sender, EventArgs e) { AIBoardClick(136); }
        private void aiD7_Click(object sender, EventArgs e) { AIBoardClick(137); }
        private void aiD8_Click(object sender, EventArgs e) { AIBoardClick(138); }
        private void aiD9_Click(object sender, EventArgs e) { AIBoardClick(139); }
        private void aiD10_Click(object sender, EventArgs e) { AIBoardClick(140); }
        private void aiE1_Click(object sender, EventArgs e) { AIBoardClick(141); }
        private void aiE2_Click(object sender, EventArgs e) { AIBoardClick(142); }
        private void aiE3_Click(object sender, EventArgs e) { AIBoardClick(143); }
        private void aiE4_Click(object sender, EventArgs e) { AIBoardClick(144); }
        private void aiE5_Click(object sender, EventArgs e) { AIBoardClick(145); }
        private void aiE6_Click(object sender, EventArgs e) { AIBoardClick(146); }
        private void aiE7_Click(object sender, EventArgs e) { AIBoardClick(147); }
        private void aiE8_Click(object sender, EventArgs e) { AIBoardClick(148); }
        private void aiE9_Click(object sender, EventArgs e) { AIBoardClick(149); }
        private void aiE10_Click(object sender, EventArgs e) { AIBoardClick(150); }
        private void aiF1_Click(object sender, EventArgs e) { AIBoardClick(151); }
        private void aiF2_Click(object sender, EventArgs e) { AIBoardClick(152); }
        private void aiF3_Click(object sender, EventArgs e) { AIBoardClick(153); }
        private void aiF4_Click(object sender, EventArgs e) { AIBoardClick(154); }
        private void aiF5_Click(object sender, EventArgs e) { AIBoardClick(155); }
        private void aiF6_Click(object sender, EventArgs e) { AIBoardClick(156); }
        private void aiF7_Click(object sender, EventArgs e) { AIBoardClick(157); }
        private void aiF8_Click(object sender, EventArgs e) { AIBoardClick(158); }
        private void aiF9_Click(object sender, EventArgs e) { AIBoardClick(159); }
        private void aiF10_Click(object sender, EventArgs e) { AIBoardClick(160); }
        private void aiG1_Click(object sender, EventArgs e) { AIBoardClick(161); }
        private void aiG2_Click(object sender, EventArgs e) { AIBoardClick(162); }
        private void aiG3_Click(object sender, EventArgs e) { AIBoardClick(163); }
        private void aiG4_Click(object sender, EventArgs e) { AIBoardClick(164); }
        private void aiG5_Click(object sender, EventArgs e) { AIBoardClick(165); }
        private void aiG6_Click(object sender, EventArgs e) { AIBoardClick(166); }
        private void aiG7_Click(object sender, EventArgs e) { AIBoardClick(167); }
        private void aiG8_Click(object sender, EventArgs e) { AIBoardClick(168); }
        private void aiG9_Click(object sender, EventArgs e) { AIBoardClick(169); }
        private void aiG10_Click(object sender, EventArgs e) { AIBoardClick(170); }
        private void aiH1_Click(object sender, EventArgs e) { AIBoardClick(171); }
        private void aiH2_Click(object sender, EventArgs e) { AIBoardClick(172); }
        private void aiH3_Click(object sender, EventArgs e) { AIBoardClick(173); }
        private void aiH4_Click(object sender, EventArgs e) { AIBoardClick(174); }
        private void aiH5_Click(object sender, EventArgs e) { AIBoardClick(175); }
        private void aiH6_Click(object sender, EventArgs e) { AIBoardClick(176); }
        private void aiH7_Click(object sender, EventArgs e) { AIBoardClick(177); }
        private void aiH8_Click(object sender, EventArgs e) { AIBoardClick(178); }
        private void aiH9_Click(object sender, EventArgs e) { AIBoardClick(179); }
        private void aiH10_Click(object sender, EventArgs e) { AIBoardClick(180); }
        private void aiI1_Click(object sender, EventArgs e) { AIBoardClick(181); }
        private void aiI2_Click(object sender, EventArgs e) { AIBoardClick(182); }
        private void aiI3_Click(object sender, EventArgs e) { AIBoardClick(183); }
        private void aiI4_Click(object sender, EventArgs e) { AIBoardClick(184); }
        private void aiI5_Click(object sender, EventArgs e) { AIBoardClick(185); }
        private void aiI6_Click(object sender, EventArgs e) { AIBoardClick(186); }
        private void aiI7_Click(object sender, EventArgs e) { AIBoardClick(187); }
        private void aiI8_Click(object sender, EventArgs e) { AIBoardClick(188); }
        private void aiI9_Click(object sender, EventArgs e) { AIBoardClick(189); }
        private void aiI10_Click(object sender, EventArgs e) { AIBoardClick(190); }
        private void aiJ1_Click(object sender, EventArgs e) { AIBoardClick(191); }
        private void aiJ2_Click(object sender, EventArgs e) { AIBoardClick(192); }
        private void aiJ3_Click(object sender, EventArgs e) { AIBoardClick(193); }
        private void aiJ4_Click(object sender, EventArgs e) { AIBoardClick(194); }
        private void aiJ5_Click(object sender, EventArgs e) { AIBoardClick(195); }
        private void aiJ6_Click(object sender, EventArgs e) { AIBoardClick(196); }
        private void aiJ7_Click(object sender, EventArgs e) { AIBoardClick(197); }
        private void aiJ8_Click(object sender, EventArgs e) { AIBoardClick(198); }
        private void aiJ9_Click(object sender, EventArgs e) { AIBoardClick(199); }
        private void aiJ10_Click(object sender, EventArgs e) { AIBoardClick(200); }
    }
}
