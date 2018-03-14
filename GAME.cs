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
        public int Clock { get; set; }

        private const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";
        public Game()
        {

            InitializeComponent();
            
            _player = PlayerDataMapper.CreateFromDataBase();
            if (_player == null)
            {
                if (File.Exists(PLAYER_DATA_FILE_NAME))
                {
                    _player = Player.createplayerformxmlString(File.ReadAllText(PLAYER_DATA_FILE_NAME));
                }
                else
                {
                    Player.CreateDefaultPlayer();
                    //Start(false, this);
                }
            }
            if(_player != null)
            { 
            Strength.Text = Convert.ToString(_player.Strength);
            Speed.Text = Convert.ToString(_player.Speed);
            Intelligence.Text = Convert.ToString(_player.Intelligence);
            Sight.Text = Convert.ToString(_player.Sight);
            Endurance.Text = Convert.ToString(_player.Endurance);
            StrengthBar.Value = _player.Strength;
            SpeedBar.Value = _player.Speed;
            IntelligenceBar.Value = _player.Intelligence;
            SightBar.Value = _player.Sight;
            EnduranceBar.Value = _player.Endurance;
            Health.DataBindings.Add("Text", _player, "HP");
            Gold.DataBindings.Add("Text", _player, "Gold");
            Stamina.DataBindings.Add("Text", _player, "Stamina");
            StaminaBar.DataBindings.Add("Value", _player, "Stamina");
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
            if (_player.CurrentLocation != null)
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
            }
        }
        public void Start(bool val,Control container)
        {
            foreach(Control c in container.Controls)
            {
                if(c is Panel||c is GroupBox)
                {
                    Start(val, c);
                }
                else
                {
                    c.Enabled = val;
                }
            }
            if(val==false)
            {
                textBox1.Text = ("What occupation for the great army were you?");
                Attack.Text = "Infantry";
                North.Text = "Cavalry";
                Inventor.Text = "Medic";
                Attack.Enabled = true;
                Inventor.Enabled = true;
                North.Enabled = true;
            }
            else if(val == true)
            {
                Attack.Text = "";
                Inventor.Text = "use";
                Attack.Enabled = false;
                Inventor.Enabled = false;
                North.Enabled = false;
               

            }

        }

        //////
        private void displaymessage(object sender, MessageEventArgs messageEventArgs)
        {
            textBox1.Text += messageEventArgs.Message + Environment.NewLine;

            if (messageEventArgs.addExtraNewLine)
            {
                textBox1.Text += Environment.NewLine;
            }
            if (Convert.ToString(messageEventArgs.Message) == "Level Up!")
            {
                LevelUpPerkUP.Visible = true;
                _player.HP = _player.MaxHP;
                _player.Stamina = 100;
            }
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        //////
        private void PlayerOnPropertyChanged(object sender, PropertyChangedEventArgs protertyChangedEventArgs)
        {
            if (protertyChangedEventArgs.PropertyName == "Weapon")
            {
                cobweapons.DataSource = _player.Weapons;
                if (!_player.Weapons.Any())
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
            if (protertyChangedEventArgs.PropertyName == "Potions")
            {
                cobpotion.DataSource = _player.Potions;
                if (!_player.Potions.Any())
                {
                    Inventor.Enabled = false;
                    cobpotion.Visible = false;
                }
            }
            if (protertyChangedEventArgs.PropertyName == "CurrentLocation")
            {
                North.Enabled = (_player.CurrentLocation.LocationToNorth != null);
                East.Enabled = (_player.CurrentLocation.LocationToEast != null);
                South.Enabled = (_player.CurrentLocation.LocationToSouth != null);
                West.Enabled = (_player.CurrentLocation.LocationToWest != null);
                if ((_player.CurrentLocation.LocationToNorth != null))
                {
                    North.Text = _player.CurrentLocation.LocationToNorth.Name;
                }
                else
                {
                    North.Text = " ";
                }
                if ((_player.CurrentLocation.LocationToEast != null))
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

                btnTrade.Enabled = (_player.CurrentLocation.Vending != null);
                if (btnTrade.Enabled = (_player.CurrentLocation.Vending != null))
                {
                    btnTrade.Text = "Shop";
                }
                else
                {
                    btnTrade.Text = " ";
                }
                clock(15);
            }
            if (protertyChangedEventArgs.PropertyName == "CurrentMonster")
            {


                if (_player.CurrentMonster == null)
                {
                    Attack.Enabled = false;
                }
                else if (_player.CurrentMonster != null)
                {
                    Attack.Enabled = _player.Weapons.Any();
                    Attack.Text = "Attack";
                }
            }
            if (protertyChangedEventArgs.PropertyName == "Stats")
            {
                if (_player.Strength != Convert.ToInt32(Strength.Text))
                {
                    Strength.Text = Convert.ToString(_player.Strength);
                    StrengthBar.Value = Convert.ToInt32(Strength.Text);
                }
                if (_player.Speed != Convert.ToInt32(Speed.Text))
                {
                    Speed.Text = Convert.ToString(_player.Speed);
                    SpeedBar.Value = Convert.ToInt32(Speed.Text);
                    StaminaBar.Maximum = 100 + ((int)_player.Speed / 5);
                }
                if (_player.Endurance != Convert.ToInt32(Endurance.Text))
                {
                    Endurance.Text = Convert.ToString(_player.Endurance);
                    EnduranceBar.Value = Convert.ToInt32(Endurance.Text);
                    _player.MaxHP = 100 + _player.Endurance;
                }
                if (_player.Sight != Convert.ToInt32(Sight.Text))
                {
                    Sight.Text = Convert.ToString(_player.Sight);
                    SightBar.Value = Convert.ToInt32(Sight.Text);
                }
                if (_player.Intelligence != Convert.ToInt32(Intelligence.Text))
                {
                    Intelligence.Text = Convert.ToString(_player.Intelligence);
                    IntelligenceBar.Value = Convert.ToInt32(Intelligence.Text);
                }
            }

            if (_player.Stamina > StaminaBar.Maximum)
            {
                _player.Stamina = StaminaBar.Maximum;
            }
            HealthBar.Maximum = _player.MaxHP;
            if (_player.HP > _player.MaxHP)
            {
                _player.HP = _player.MaxHP;
            }
            HealthBar.Value = Convert.ToInt32(_player.HP);
            Health.Text = Convert.ToString(_player.HP);
        }
        //////
        private void North_Click(object sender, EventArgs e)
        {//also Ablillites 

          if(North.Text == "Cavalry")
            {
                //Player.CreateDefaultPlayer("C");
                Start(true, this);
            }
            else
            { 
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
            if (_player.Stamina > 0)
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
            if (_player.Stamina > 0)
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
            if(Attack.Text == "Infantry")
            {
               // Player.CreateDefaultPlayer("I");
                textBox1.Text = "To Move with wasd." + Environment.NewLine;
                Start(true, this);
            }
            else
            {
            Weapon currentWeapon = (Weapon)cobweapons.SelectedItem;
            _player.UseWeapon(currentWeapon);
            }
        }
        //////
        private void Inventor_Click(object sender, EventArgs e)
        {//use item

            if(Inventor.Text == "Medic")
            {
                //Player.CreateDefaultPlayer("M");
                textBox1.Text = "To Move with wasd." + Environment.NewLine;
                Start(true, this);
            }
            else
            {
                Healing potion = (Healing)cobpotion.SelectedItem;
                _player.UsePotion(potion);
                if (!_player.Potions.Any())
                {
                    cobpotion.Visible = false;
                    Inventor.Enabled = false;
                }
            }
        }
        //////
        private void Restandsleep_Click(object sender, EventArgs e)
        {//blank
            clock(60);
            _player.Rest();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            if (_player.Stamina == 0)
            {
                _player.CurrentLocation = _player.CurrentLocation;
            }

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(PLAYER_DATA_FILE_NAME, _player.Toxmlstring());

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
            if (keyData == (Keys.Q))
            {
                Attack.PerformClick();
            }
            if (keyData == (Keys.I))
            {
                Inventor.PerformClick();
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
        public int Hour = 6;
        public int Min = 15;
        public void clock (int time)
        {
            //set time before add time so can reset hours and mins
            if (time == 60)
            {
                Hour = Hour + 2;
                if(Hour == 24)
                {
                    Hour = 0;
                }
                else if(Hour == 25)
                {
                    Hour = 1;
                }
            }
            else
            {
                Min = Min + time;
                if (Min >= 60)
                {
                    Hour++;
                    if (Hour == 24)
                    {
                        Hour = 0;
                    }
                    Min = Min - 60;
                }
            }
            if(Min == 0)
            {
                TIME.Text = Hour + ":00";
            }
            else
            {
                TIME.Text = Hour + ":" + Min;
            }
            
        }
    }
}
