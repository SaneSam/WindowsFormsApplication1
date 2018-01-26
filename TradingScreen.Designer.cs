namespace Engine
{
    partial class TradingScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerItems = new System.Windows.Forms.DataGridView();
            this.TraderItems = new System.Windows.Forms.DataGridView();
            this.Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TraderItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(77, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 23);
            this.label10.TabIndex = 27;
            this.label10.Text = "Inventory";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(377, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 23);
            this.label1.TabIndex = 28;
            this.label1.Text = "Trader Invnentory";
            // 
            // PlayerItems
            // 
            this.PlayerItems.BackgroundColor = System.Drawing.Color.SeaShell;
            this.PlayerItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlayerItems.GridColor = System.Drawing.Color.Wheat;
            this.PlayerItems.Location = new System.Drawing.Point(12, 56);
            this.PlayerItems.Name = "PlayerItems";
            this.PlayerItems.Size = new System.Drawing.Size(240, 406);
            this.PlayerItems.TabIndex = 29;
            // 
            // TraderItems
            // 
            this.TraderItems.AllowUserToAddRows = false;
            this.TraderItems.AllowUserToDeleteRows = false;
            this.TraderItems.AllowUserToResizeColumns = false;
            this.TraderItems.AllowUserToResizeRows = false;
            this.TraderItems.BackgroundColor = System.Drawing.Color.SeaShell;
            this.TraderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TraderItems.GridColor = System.Drawing.Color.Wheat;
            this.TraderItems.Location = new System.Drawing.Point(341, 56);
            this.TraderItems.Name = "TraderItems";
            this.TraderItems.Size = new System.Drawing.Size(240, 406);
            this.TraderItems.TabIndex = 30;
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(506, 468);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 31;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // TradingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(593, 501);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.TraderItems);
            this.Controls.Add(this.PlayerItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Name = "TradingScreen";
            this.Text = "TradingScreen";
            ((System.ComponentModel.ISupportInitialize)(this.PlayerItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TraderItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView PlayerItems;
        private System.Windows.Forms.DataGridView TraderItems;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private System.Windows.Forms.Button Close;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}