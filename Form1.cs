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
        readonly String[] SHIP_NAMES = {"Carrier", "Battleship", "Destroyer", "Submarine", "Patrol Boat"};
        readonly int[] SHIP_LENGTHS = {5, 4, 3, 3, 2};

        private int placementCount;
        private Boolean setup;

        readonly Color UNKNOWN_CELL = Color.White;
        readonly Color MISS_CELL = Color.DodgerBlue;
        readonly Color SHIP_CELL = Color.Green;
        readonly Color HIT_CELL = Color.Red;
        readonly Color SUNK_CELL = Color.DarkRed;

        private Color[] cellsColor = new Color[201];

        private int shipStartCell;

        readonly int NUMBER_OF_SHIPS = 5;
        Ship[] playerShips;
        Ship[] AIShips;
        public Form1()
        {
            InitializeComponent();
            newGame();
        }

        private void exit_button_Click(object sender, EventArgs e) {this.Close();}

        private void newgame_button_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void newGame() {
            setup = true;
            visibleDirectionControls(false);
            playerShips = new Ship[NUMBER_OF_SHIPS];
            AIShips = new Ship[NUMBER_OF_SHIPS];
            initializeShips("Player");
            initializeShips("AI");
            initializeCells();
            updateBoard();
            shipStartCell = 0;
            placementInstructions(0);
            //
        }

        private void visibleDirectionControls(Boolean enable) {
            lb_Direction.Visible = enable;
            cb_Direction.Visible = enable;
            bt_Direction.Visible = enable;
        }

        private void initializeShips(String user) {
            for (int i = 0; i < NUMBER_OF_SHIPS; i++) {
                String shipName = user + "_" + SHIP_NAMES[i];
                if (user.Equals("Player")) {
                    playerShips[i] = new Ship(shipName, SHIP_LENGTHS[i]);
                }else if (user.Equals("AI")) {
                    AIShips[i] = new Ship(shipName, SHIP_LENGTHS[i]);
                }
            }
        }

        private void initializeCells() {
            for (int i = 1; i < cellsColor.Length; i++) {
                cellsColor[i] = UNKNOWN_CELL;
            }
        }

        private void updateBoard()
        {
            pA1.BackColor = cellsColor[1];
            pA2.BackColor = cellsColor[2];
            pA3.BackColor = cellsColor[3];
            pA4.BackColor = cellsColor[4];
            //
            pB1.BackColor = cellsColor[11];
            //
            pC1.BackColor = cellsColor[21];
            //
            pD1.BackColor = cellsColor[31];
            //
            pE1.BackColor = cellsColor[41];
            //
            pF1.BackColor = cellsColor[51];
            //
            pG1.BackColor = cellsColor[61];
            //
            pH1.BackColor = cellsColor[71];
            //
            pI1.BackColor = cellsColor[81];
            //
            pJ1.BackColor = cellsColor[91];
            //
            pJ10.BackColor = cellsColor[100];

            aiA1.BackColor = cellsColor[101];
            //
            aiB1.BackColor = cellsColor[111];
            //
            aiC1.BackColor = cellsColor[121];
            //
            aiD1.BackColor = cellsColor[131];
            //
            aiE1.BackColor = cellsColor[141];
            //
            aiF1.BackColor = cellsColor[151];
            //
            aiG1.BackColor = cellsColor[161];
            //
            aiH1.BackColor = cellsColor[171];
            //
            aiI1.BackColor = cellsColor[181];
            //
            aiJ1.BackColor = cellsColor[191];
            //
            aiJ10.BackColor = cellsColor[200];
            //END
        }

        private void placementInstructions(int placement) {
            if (shipStartCell == 0 && placement != NUMBER_OF_SHIPS) {
                output_label.Text = "Select the starting point for your " + SHIP_NAMES[placement] + " (" + SHIP_LENGTHS[placement] + " spaces).";
            }
            else if (placement != NUMBER_OF_SHIPS) {
                output_label.Text = "Select a direction to place the rest of your " + SHIP_NAMES[placement] + " ("+ SHIP_LENGTHS[placement]+" spaces).";
            }
            this.placementCount = placement;
            if (placement == NUMBER_OF_SHIPS) {
                setup = false;
            }
            //Waiting for playerBoardClick() or bt_Direction_Click().
        }

        private void playerBoardClick(int cell) {
            if (shipStartCell == 0 && setup == true){
                shipStartCell = cell;
                //Ensure the ship can be placed.
                cellsColor[cell] = Color.Yellow;
                updateBoard();
                visibleDirectionControls(true);
                //Waiting for bt_Direction_Click().
            }
        }

        private void bt_Direction_Click(object sender, EventArgs e) {
            String shipDirection = cb_Direction.Text;
            if (shipDirection.Equals("North") || shipDirection.Equals("South") || shipDirection.Equals("East") || shipDirection.Equals("West"))
            {
                visibleDirectionControls(false);
                int increment = directionIncriment(shipDirection);
                if (shipPlacementPossible(shipDirection, increment))
                {
                    placeShip(shipStartCell, increment);
                    shipStartCell = 0;
                    placementInstructions(placementCount + 1);
                }
                else
                {
                    MessageBox.Show("That placement is impossible.");
                    shipStartCell = 0;
                    placementInstructions(placementCount);
                }
            }
            else
            {
                MessageBox.Show("No direction selected.");
            }
        }

        private Boolean shipPlacementPossible(String shipDirection, int incriment)
        {
            for (int i = 0; i < SHIP_LENGTHS[placementCount]; i++) {
                int cell = shipStartCell + (incriment * i);
                if (cellsColor[cell].Equals(UNKNOWN_CELL) == false && cellsColor[cell].Equals(Color.Yellow) == false) {
                    return false;
                }
            }
            return true;
        }

        private int directionIncriment(String shipDirection) {
            int increment = 0;
            switch (shipDirection)
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
                default:
                    increment = 0;
                    break;
            }
            return increment;
        }

        private void placeShip(int start, int increment) {
            for (int i = 0; i < SHIP_LENGTHS[placementCount]; i++) {
                cellsColor[shipStartCell + (increment * i)] = SHIP_CELL;
            }
            updateBoard();
        }
        
        private void AIBoardClick(int cell) {
            if (setup == false) {
                attack(cell);
            }
        }

        private void attack(int location) {
            
        }

        private void pA1_Click(object sender, EventArgs e){playerBoardClick(1);}

        private void pA2_Click(object sender, EventArgs e) {playerBoardClick(2);}

        private void pA3_Click(object sender, EventArgs e) {playerBoardClick(3);}

        private void pA4_Click(object sender, EventArgs e)
        {
            playerBoardClick(4);
        }

        private void pA5_Click(object sender, EventArgs e)
        {

        }

        private void pA6_Click(object sender, EventArgs e)
        {

        }

        private void pA7_Click(object sender, EventArgs e)
        {

        }

        private void pA8_Click(object sender, EventArgs e)
        {

        }

        private void pA9_Click(object sender, EventArgs e)
        {

        }

        private void pA10_Click(object sender, EventArgs e)
        {

        }

        private void pB1_Click(object sender, EventArgs e)
        {

        }

        private void pB2_Click(object sender, EventArgs e)
        {

        }

        private void pB3_Click(object sender, EventArgs e)
        {

        }

        private void pB4_Click(object sender, EventArgs e)
        {

        }

        private void pB5_Click(object sender, EventArgs e)
        {

        }

        private void pB6_Click(object sender, EventArgs e)
        {

        }

        private void pB7_Click(object sender, EventArgs e)
        {

        }

        private void pB8_Click(object sender, EventArgs e)
        {

        }

        private void pB9_Click(object sender, EventArgs e)
        {

        }

        private void pB10_Click(object sender, EventArgs e)
        {

        }

        private void pC1_Click(object sender, EventArgs e)
        {

        }

        private void pC2_Click(object sender, EventArgs e)
        {

        }

        private void pC3_Click(object sender, EventArgs e)
        {

        }

        private void pC4_Click(object sender, EventArgs e)
        {

        }

        private void pC5_Click(object sender, EventArgs e)
        {

        }

        private void pC6_Click(object sender, EventArgs e)
        {

        }

        private void pC7_Click(object sender, EventArgs e)
        {

        }

        private void pC8_Click(object sender, EventArgs e)
        {

        }

        private void pC9_Click(object sender, EventArgs e)
        {

        }

        private void pC10_Click(object sender, EventArgs e)
        {

        }

        private void pD1_Click(object sender, EventArgs e)
        {

        }

        private void pD2_Click(object sender, EventArgs e)
        {

        }

        private void pD3_Click(object sender, EventArgs e)
        {

        }

        private void pD4_Click(object sender, EventArgs e)
        {

        }

        private void pD5_Click(object sender, EventArgs e)
        {

        }

        private void pD6_Click(object sender, EventArgs e)
        {

        }

        private void pD7_Click(object sender, EventArgs e)
        {

        }

        private void pD8_Click(object sender, EventArgs e)
        {

        }

        private void pD9_Click(object sender, EventArgs e)
        {

        }

        private void pD10_Click(object sender, EventArgs e)
        {

        }

        private void pE1_Click(object sender, EventArgs e)
        {

        }

        private void pE2_Click(object sender, EventArgs e)
        {

        }

        private void pE3_Click(object sender, EventArgs e)
        {

        }

        private void pE4_Click(object sender, EventArgs e)
        {

        }

        private void pE5_Click(object sender, EventArgs e)
        {

        }

        private void pE6_Click(object sender, EventArgs e)
        {

        }

        private void pE7_Click(object sender, EventArgs e)
        {

        }

        private void pE8_Click(object sender, EventArgs e)
        {

        }

        private void pE9_Click(object sender, EventArgs e)
        {

        }

        private void pE10_Click(object sender, EventArgs e)
        {

        }

        private void pF1_Click(object sender, EventArgs e)
        {

        }

        private void pF2_Click(object sender, EventArgs e)
        {

        }

        private void pF3_Click(object sender, EventArgs e)
        {

        }

        private void pF4_Click(object sender, EventArgs e)
        {

        }

        private void pF5_Click(object sender, EventArgs e)
        {

        }

        private void pF6_Click(object sender, EventArgs e)
        {

        }

        private void pF7_Click(object sender, EventArgs e)
        {

        }

        private void pF8_Click(object sender, EventArgs e)
        {

        }

        private void pF9_Click(object sender, EventArgs e)
        {

        }

        private void pF10_Click(object sender, EventArgs e)
        {

        }

        private void pG1_Click(object sender, EventArgs e)
        {

        }

        private void pG2_Click(object sender, EventArgs e)
        {

        }

        private void pG3_Click(object sender, EventArgs e)
        {

        }

        private void pG4_Click(object sender, EventArgs e)
        {

        }

        private void pG5_Click(object sender, EventArgs e)
        {

        }

        private void pG6_Click(object sender, EventArgs e)
        {

        }

        private void pG7_Click(object sender, EventArgs e)
        {

        }

        private void pG8_Click(object sender, EventArgs e)
        {

        }

        private void pG9_Click(object sender, EventArgs e)
        {

        }

        private void pG10_Click(object sender, EventArgs e)
        {

        }

        private void pH1_Click(object sender, EventArgs e)
        {

        }

        private void pH2_Click(object sender, EventArgs e)
        {

        }

        private void pH3_Click(object sender, EventArgs e)
        {

        }

        private void pH4_Click(object sender, EventArgs e)
        {

        }

        private void pH5_Click(object sender, EventArgs e)
        {

        }

        private void pH6_Click(object sender, EventArgs e)
        {

        }

        private void pH7_Click(object sender, EventArgs e)
        {

        }

        private void pH8_Click(object sender, EventArgs e)
        {

        }

        private void pH9_Click(object sender, EventArgs e)
        {

        }

        private void pH10_Click(object sender, EventArgs e)
        {

        }

        private void pI1_Click(object sender, EventArgs e)
        {

        }

        private void pI2_Click(object sender, EventArgs e)
        {

        }

        private void pI3_Click(object sender, EventArgs e)
        {

        }

        private void pI4_Click(object sender, EventArgs e)
        {

        }

        private void pI5_Click(object sender, EventArgs e)
        {

        }

        private void pI6_Click(object sender, EventArgs e)
        {

        }

        private void pI7_Click(object sender, EventArgs e)
        {

        }

        private void pI8_Click(object sender, EventArgs e)
        {

        }

        private void pI9_Click(object sender, EventArgs e)
        {

        }

        private void pI10_Click(object sender, EventArgs e)
        {

        }

        private void pJ1_Click(object sender, EventArgs e)
        {

        }

        private void pJ2_Click(object sender, EventArgs e)
        {

        }

        private void pJ3_Click(object sender, EventArgs e)
        {

        }

        private void pJ4_Click(object sender, EventArgs e)
        {

        }

        private void pJ5_Click(object sender, EventArgs e)
        {

        }

        private void pJ6_Click(object sender, EventArgs e)
        {

        }

        private void pJ7_Click(object sender, EventArgs e)
        {

        }

        private void pJ8_Click(object sender, EventArgs e)
        {

        }

        private void pJ9_Click(object sender, EventArgs e)
        {

        }

        private void pJ10_Click(object sender, EventArgs e)
        {

        }
    }
}
