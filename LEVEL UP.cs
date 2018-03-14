using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
    
    public partial class LEVEL_UP : Form
    {
        
      
        private Player _currentPlayer;
        public LEVEL_UP(Player player)
        {
            _currentPlayer = player;
            InitializeComponent();
            Strength.DataBindings.Add("Text", _currentPlayer, "Strength");
            Speed.DataBindings.Add("Text", _currentPlayer, "Speed");

        }
        public int Points = 20;

        private void AddStrength_Click(object sender, EventArgs e)
        {
            
            if(Points > 0 && _currentPlayer.Strength != 100)
            {
                _currentPlayer.Strength += 1;
                Strength.Text = Convert.ToString(Convert.ToInt16(Strength.Text) + 1);
                Points -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            else
            {
                Pointshave.Text = "You have no more points";
                LBPOINTS.Visible = false;
            }
            if (Strength.Text != "10" && Points>0)
            {
                SubStrength.Enabled = true;
            }
        }

        private void AddEndurance_Click(object sender, EventArgs e)
        {
            if (Points > 0 && _currentPlayer.Endurance != 100)
            {
                _currentPlayer.Endurance += 1;
                Endurance.Text = Convert.ToString(Convert.ToInt16(Endurance.Text) + 1);
                Points -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            else
            {
                Pointshave.Text = "You have no more points";
                LBPOINTS.Visible = false;
            }
            if (Endurance.Text != "10" && Points > 0)
            {
                SubEndurance.Enabled = true;
            }
        }

        private void AddSpeed_Click(object sender, EventArgs e)
        {
            if (Points > 0 && _currentPlayer.Speed != 100)
            {
                _currentPlayer.Speed += 1;
                Speed.Text = Convert.ToString(Convert.ToInt16(Speed.Text) + 1);
                Points -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            else
            {
                Pointshave.Text = "You have no more points";
                LBPOINTS.Visible = false;
            }
            if (Speed.Text != "10" && Points > 0)
            {
                SubSpeed.Enabled = true;
            }
        }

        private void AddSight_Click(object sender, EventArgs e)
        {
            if (Points > 0 && _currentPlayer.Sight != 100)
            {
                _currentPlayer.Sight += 1;
                Sight.Text = Convert.ToString(Convert.ToInt16(Sight.Text) + 1);
                Points -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            else
            {
                Pointshave.Text = "You have no more points";
                LBPOINTS.Visible = false;
            }
            if (Sight.Text != "10" && Points > 0)
            {
                SubSight.Enabled = true;
            }
        }

        private void AddIntelligence_Click(object sender, EventArgs e)
        {
            if (Points > 0 && _currentPlayer.Intelligence <= 100)
            {
                _currentPlayer.Intelligence += 1;
                Intelligence.Text = Convert.ToString(Convert.ToInt16(Intelligence.Text) + 1);
                Points -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            else
            {
                Pointshave.Text = "You have no more points";
                LBPOINTS.Visible = false;
            }
            if (Intelligence.Text != "10" && Points > 0)
            {
                SubIntelligence.Enabled = true;
            }
        }
        //////////
        private void SubStrength_Click(object sender, EventArgs e)
        {
           
            if( Points != 20 && _currentPlayer.Strength >= 10)
            {
                Points += 1;
                Strength.Text = Convert.ToString(Convert.ToInt16(Strength.Text) - 1);
                _currentPlayer.Strength -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            if(Points != 0)
            {
                LBPOINTS.Visible = true;
                LBPOINTS.Text = Convert.ToString(Points);
                Pointshave.Text = "Points avaible:";
            }
            if (Strength.Text == "10")
            {
                SubStrength.Enabled = false;
            }
        }

        private void SubEndurance_Click(object sender, EventArgs e)
        {
            if (Points != 20 && _currentPlayer.Endurance >= 10)
            {
                Points += 1;
                Endurance.Text = Convert.ToString(Convert.ToInt16(Endurance.Text) - 1);
                _currentPlayer.Endurance -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            if (Points != 0)
            {
                LBPOINTS.Visible = true;
                LBPOINTS.Text = Convert.ToString(Points);
                Pointshave.Text = "Points avaible:";
            }
            if (Endurance.Text == "10")
            {
                SubEndurance.Enabled = false;
            }
        }

        private void SubSpeed_Click(object sender, EventArgs e)
        {
            if (Points != 20 && _currentPlayer.Speed >= 10)
            {
                Points += 1;
                Speed.Text = Convert.ToString(Convert.ToInt16(Speed.Text) - 1);
                _currentPlayer.Speed -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            if (Points != 0)
            {
                LBPOINTS.Visible = true;
                LBPOINTS.Text = Convert.ToString(Points);
                Pointshave.Text = "Points avaible:";
            }
            if (Speed.Text == "10")
            {
                SubSpeed.Enabled = false;
            }
        }

        private void SubSight_Click(object sender, EventArgs e)
        {
            if (Points != 20 && _currentPlayer.Sight >= 10)
            {
                Points += 1;
                Sight.Text = Convert.ToString(Convert.ToInt16(Sight.Text) - 1);
                _currentPlayer.Sight -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            if (Points != 0)
            {
                LBPOINTS.Visible = true;
                LBPOINTS.Text = Convert.ToString(Points);
                Pointshave.Text = "Points avaible:";
            }
            if (Speed.Text == "10")
            {
                SubSight.Enabled = false;
            }
        }

        private void SubIntelligence_Click(object sender, EventArgs e)
        {
            if (Points != 20 && _currentPlayer.Intelligence >= 10)
            {
                Points += 1;
                Intelligence.Text = Convert.ToString(Convert.ToInt16(Intelligence.Text) - 1);
                _currentPlayer.Intelligence -= 1;
                LBPOINTS.Text = Convert.ToString(Points);
            }
            if (Points != 0)
            {
                LBPOINTS.Visible = true;
                LBPOINTS.Text = Convert.ToString(Points);
                Pointshave.Text = "Points avaible:";
            }
            if (Speed.Text == "10")
            {
                SubIntelligence.Enabled = false;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == (Keys.RShiftKey)|keyData ==(Keys.A))
            {
                Points = 20;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
