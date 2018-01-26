using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace Engine
{
    public partial class TradingScreen : Form
    {
        private Player _currentPlayer;
        
        public TradingScreen(Player player)
        {
            _currentPlayer = player;

            InitializeComponent();

            DataGridViewCellStyle rightalignedcellstyle = new DataGridViewCellStyle();
            rightalignedcellstyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            PlayerItems.RowHeadersVisible = false;
            PlayerItems.AutoGenerateColumns = false;

            PlayerItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemID",
                Visible = false
            });

            PlayerItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 132,
                DataPropertyName = "Description"
            });
            PlayerItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qty",
                Width = 30,
                DefaultCellStyle = rightalignedcellstyle,
                DataPropertyName = "Quantity"
            });
            PlayerItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                Width = 35,
                DefaultCellStyle = rightalignedcellstyle,
                DataPropertyName = "Price"
            });

            PlayerItems.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Sell 1",
                UseColumnTextForButtonValue = true,
                Width = 40,
                DataPropertyName = "ItemID"
            });

            PlayerItems.DataSource = _currentPlayer.Inventory;

            PlayerItems.CellClick += PlayerItems_CellClick;

            TraderItems.RowHeadersVisible = false;
            TraderItems.AutoGenerateColumns = false;

            TraderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemID",
                Visible = false
            });

            TraderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 137,
                DataPropertyName = "Description"
            });
            TraderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                Width = 35,
                DefaultCellStyle = rightalignedcellstyle,
                DataPropertyName = "Price"
            });

            TraderItems.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Buy 1",
                UseColumnTextForButtonValue = true,
                Width = 65,
                DataPropertyName = "ItemID"
            });

            TraderItems.DataSource = _currentPlayer.CurrentLocation.Vending.Inventory;

            TraderItems.CellClick += TraderItems_CellClick;
        }
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void TraderItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                var itemID = TraderItems.Rows[e.RowIndex].Cells[0].Value;

                _items itembeingbought = World.ItemByID(Convert.ToInt32(itemID));

                if(_currentPlayer.Gold >= itembeingbought.Price)
                {
                    _currentPlayer.additemtoinventor(itembeingbought);
                    _currentPlayer.Gold -= itembeingbought.Price;
                }
                else
                {
                    MessageBox.Show("You do not have enough gold to buy the " + itembeingbought.Name);
                }
            }
        }

        private void PlayerItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                var itemID = PlayerItems.Rows[e.RowIndex].Cells[0].Value;

                _items itembeingsold = World.ItemByID(Convert.ToInt32(itemID));

                if(itembeingsold.Price == World.UNSELLABLE_ITEM_PRICE)
                {
                    MessageBox.Show("You cannot sell the " + itembeingsold.Name);
                }
                else
                {
                    _currentPlayer.RemoveItemFromInventory(itembeingsold);
                    _currentPlayer.Gold += itembeingsold.Price;
                }
            }
            
        }
    }
}
