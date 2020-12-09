using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Battleship_Game
{
    public partial class Points : Form
    {
        public Points()
        {
            InitializeComponent();
        }

        private void Points_Load(object sender, EventArgs e)
        {
            //Change filepath after "AttachDbFilename=" to user's filepath
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\OneDrive\\Documents\\School Papers\\COLLEGE\\Year 4\\Semester 1\\CIS 302\\Battleship Game\\Battleship Game\\Leaderboard.mdf;Integrated Security=True");

            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Leaderboard WHERE Name = 'Player 1'", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                p1Points_label.Text = reader.GetValue(1).ToString();
            }
            connection.Close();

            connection.Open();
            command = new SqlCommand("SELECT * FROM Leaderboard WHERE Name = 'Player 2'", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                p2Points_label.Text = reader.GetValue(1).ToString();
            }

            connection.Close();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            //Change filepath after "AttachDbFilename=" to user's filepath
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\OneDrive\\Documents\\School Papers\\COLLEGE\\Year 4\\Semester 1\\CIS 302\\Battleship Game\\Battleship Game\\Leaderboard.mdf;Integrated Security=True");
            SqlCommand command = connection.CreateCommand();

            try
            {
                string query = "UPDATE Leaderboard SET Points = 0";
                command.CommandText = query;
                connection.Open();

                Int32 returnFlag = (Int32)command.ExecuteNonQuery();
                if (returnFlag > 0)
                {
                    MessageBox.Show("Points reset to 0.");
                    this.Close();
                } 
                else
                    MessageBox.Show("Something went wrong.");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
