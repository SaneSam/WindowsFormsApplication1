using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Engine;

namespace Engine
{
    public partial class Game : Form
    {
        private Player _player;
        private LEVEL_UP _reset;
        private const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";
        public Game()
        {

            InitializeComponent();
            _player = PlayerDataMapper.CreateFromDataBase();
            if(_player == null)
        {
            if (File.Exists(PLAYER_DATA_FILE_NAME))
            {
                _player = Player.createplayerformxmlString(File.ReadAllText(PLAYER_DATA_FILE_NAME));
            }
            else
            {
                _player = Player.CreateDefaultPlayer();
            }
        }
                Health.DataBindings.Add("Text", _player, "HP");
                Gold.DataBindings.Add("Text", _player, "Gold");
                Stamina.DataBindings.Add("Text", _player, "Stamina");
                if(HealthBar.Maximum<= _player.MaxHP)
                {
                    HealthBar.DataBindings.Add("Maximum", _player, "MaxHP");
                    HealthBar.DataBindings.Add("Value", _player, "HP");
                }
                StaminaBar.DataBindings.Add("Value",_player, "Stamina");
                dgvInventory.RowHeadersVisible = false;
                dgvInventory.AutoGenerateColumns = false;
                dgvInventory.DataSource = _player.Inventory;

                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Name",
                        Width = 197,
                        DataPropertyName = "Description"
                    });

                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Quantity",
                        DataPropertyName = "Quantity"
                    });

                dgvQuest.RowHeadersVisible = false;
                dgvQuest.AutoGenerateColumns = false;
                dgvQuest.DataSource = _player.Quest;

                    dgvQuest.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Name",
                        Width = 197,
                        DataPropertyName = "Name"
                    });

                    dgvQuest.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Done?",
                        DataPropertyName = "IsCompleted"
                    });

                cobweapons.DataSource = _player.Weapons;
                cobweapons.DisplayMember = "Name";
                cobweapons.ValueMember = "Id";
                    if(_player.CurrentLocation != null)
                    {
                        cobweapons.SelectedItem = _player.CurrentWeapon;
                    }

                cobweapons.SelectedIndexChanged += cobweapons_SelectedIndexChanged;
                cobpotion.DataSource = _player.Potions;
                cobpotion.DisplayMember = "Name";
                cobpotion.ValueMember = "Id";

            
                _player.PropertyChanged += PlayerOnPropertyChanged;
                _player.OnMessage += displaymessage;

                _player.MoveTo(_player.CurrentLocation);

                textBox1.Text = "To Move with wasd." + Environment.NewLine;
            
        }
        //////
        private void displaymessage(object sender, MessageEventArgs messageEventArgs)
        {
            textBox1.Text += messageEventArgs.Message + Environment.NewLine;

            if(messageEventArgs.addExtraNewLine)
            {
                textBox1.Text += Environment.NewLine;
            }
            if(messageEventArgs.Message == "Level Up!")
            {
                LevelUpPerkUP.Text = "Head to camp.";
                if(_player.CurrentLocation == World.LocationByID(World.Location_ID_Camp))
                {
                    LevelUpPerkUP.Enabled = true;
                }
                
            }
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        //////
        private void PlayerOnPropertyChanged(object sender , PropertyChangedEventArgs protertyChangedEventArgs)
        {
            if(protertyChangedEventArgs.PropertyName == "Weapons")
            {
                cobweapons.DataSource = _player.Weapons;
                if(!_player.Weapons.Any())
                {
                    cobweapons.Visible = false;

                }
            }
            if (_player.CurrentLocation == World.LocationByID(World.Location_ID_Camp))
            {
                Restandsleep.Text = "sleep";
            }
            else  
            {
                Restandsleep.Text = "rest";
            }
            if(protertyChangedEventArgs.PropertyName == "Potions")
            {
                cobpotion.DataSource = _player.Potions;
                if(!_player.Potions.Any())
                {
                    Inventor.Enabled = false;
                    cobpotion.Visible = false;
                }
            }
            if(protertyChangedEventArgs.PropertyName == "CurrentLocation")
            {
                North.Enabled = (_player.CurrentLocation.LocationToNorth != null);
                East.Enabled = (_player.CurrentLocation.LocationToEast != null);
                South.Enabled = (_player.CurrentLocation.LocationToSouth != null);
                West.Enabled = (_player.CurrentLocation.LocationToWest != null);
                if((_player.CurrentLocation.LocationToNorth != null))
                {
                    North.Text = _player.CurrentLocation.LocationToNorth.Name;
                }
                else
                {
                    North.Text = " ";
                }
                if((_player.CurrentLocation.LocationToEast != null))
                {
                    East.Text = _player.CurrentLocation.LocationToEast.Name;
                }
                else
                {
                    East.Text = " ";
                }
                if ((_player.CurrentLocation.LocationToSouth != null))
                {
                    South.Text = _player.CurrentLocation.LocationToSouth.Name;
                }
                else
                {
                    South.Text = " ";
                }
                if ((_player.CurrentLocation.LocationToWest != null))
                {
                    West.Text = _player.CurrentLocation.LocationToWest.Name;
                }
                else
                {
                    West.Text = " ";
                }
                textBox1.Text += _player.CurrentLocation.Description + Environment.NewLine;
                if(!_player.CurrentLocation.HasAmonster)
                {
                    Attack.Enabled = false;
                }
                else if (_player.CurrentLocation.HasAmonster) 
                {
                    Attack.Enabled = _player.Weapons.Any();
                }
                btnTrade.Enabled = (_player.CurrentLocation.Vending != null);
                if (btnTrade.Enabled = (_player.CurrentLocation.Vending != null))
                {
                    btnTrade.Text = "Shop";
                }
                else
                {
                    btnTrade.Text = " ";
                }
            }
            
        }
        //////
        private void North_Click(object sender, EventArgs e)
        {//also Ablillites 
            
            if (_player.Stamina <= 0)
            {
                _player.Stamina = 0;

                textBox1.Text = "Take a rest you are tired of walking" + Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                North.Enabled = false;
            }
            if (_player.Stamina > 0)
            {
                _player.Stamina -= 2;
                _player.moveNorth();   
            }
            
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        //////
        private void East_Click(object sender, EventArgs e)
        {//dodge right
            
            if (_player.Stamina <= 0)
            {
                _player.Stamina = 0;
                textBox1.Text = "Take a rest you are tired of walking" + Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                East.Enabled = false;
            }
            if (_player.Stamina > 0 )
            {
                _player.Stamina -= 2;
                _player.moveEast();
            }
            

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        //////
        private void South_Click(object sender, EventArgs e)
        {//dodge back
            
            if (_player.Stamina <= 0)
            {
                _player.Stamina = 0;
                textBox1.Text = "Take a rest you are tired of walking" + Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                South.Enabled = false;
            }
            if (_player.Stamina > 0 )
            {
                _player.Stamina -= 2;
                _player.moveSouth();
            }
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        //////
        private void West_Click(object sender, EventArgs e)
        {//dodge left
            
            if (_player.Stamina <= 0)
            {
                _player.Stamina = 0;
                textBox1.Text = "Take a rest you are tired of walking" + Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                West.Enabled = false;
            }
            if (_player.Stamina > 0)
            {
                _player.Stamina -= 2;
                _player.moveWest();
            }
            

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            
        }
        //////
        private void Attack_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cobweapons.SelectedItem;
            _player.UseWeapon(currentWeapon);
            
        }
        //////
        private void Inventor_Click(object sender, EventArgs e)
        {//use item
            
            Healing potion = (Healing)cobpotion.SelectedItem;
            _player.UsePotion(potion);
            if(!_player.Potions.Any())
            {
                cobpotion.Visible = false;
                
                Inventor.Enabled = false;
            }
        }
        //////
        private void Restandsleep_Click(object sender, EventArgs e)
        {//blank

            _player.Rest();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(PLAYER_DATA_FILE_NAME,_player.Toxmlstring());

            PlayerDataMapper.SaveToDatabase(_player);
        }

        private void cobweapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentWeapon = (Weapon)cobweapons.SelectedItem;
        }
        //////
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.A))//west
            {
                West.PerformClick();
            }
            if (keyData == (Keys.W))//north
            {
                North.PerformClick();
            }
            if (keyData == (Keys.D))//east
            {
                East.PerformClick();
            }
            if (keyData == (Keys.S))//south
            {
                South.PerformClick();
            }
            if (keyData == (Keys.R)) //rest
            {
                Restandsleep.PerformClick();
            }
            if(keyData == (Keys.Q))
            {
                Attack.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            TradingScreen tradingScreen = new TradingScreen(_player);
            tradingScreen.StartPosition = FormStartPosition.CenterParent;
            tradingScreen.ShowDialog(this);
        }

        private void LevelUpPerkUP_Click(object sender, EventArgs e)
        {
            LEVEL_UP level_UP = new LEVEL_UP(_player);
            level_UP.StartPosition = FormStartPosition.CenterParent;
            level_UP.ShowDialog(this);
            LevelUpPerkUP.Visible = false;
        }
    }
}
