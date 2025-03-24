namespace Projectgogo
{
    partial class FormMoney
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.datagridshoworder = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.buttoncash = new System.Windows.Forms.Button();
            this.buttonqr = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.VATLABEL = new System.Windows.Forms.Label();
            this.TOTALlabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.totalprice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.datagridshoworder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(178, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "ORDER";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(503, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 607);
            this.panel1.TabIndex = 1;
            // 
            // datagridshoworder
            // 
            this.datagridshoworder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridshoworder.Location = new System.Drawing.Point(51, 146);
            this.datagridshoworder.Name = "datagridshoworder";
            this.datagridshoworder.Size = new System.Drawing.Size(374, 469);
            this.datagridshoworder.TabIndex = 2;
            this.datagridshoworder.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridshoworder_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(519, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "DISCOUNT  :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(519, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(359, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "MEMBER SHIP 5 % :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(797, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(244, 39);
            this.label4.TabIndex = 7;
            this.label4.Text = "---- PRICE ----";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox1.Location = new System.Drawing.Point(882, 281);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(246, 52);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.ForestGreen;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1134, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 52);
            this.button1.TabIndex = 9;
            this.button1.Text = "SUBMIT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(526, 329);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(325, 328);
            this.guna2PictureBox1.TabIndex = 10;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // buttoncash
            // 
            this.buttoncash.BackColor = System.Drawing.Color.OrangeRed;
            this.buttoncash.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncash.ForeColor = System.Drawing.Color.White;
            this.buttoncash.Location = new System.Drawing.Point(882, 355);
            this.buttoncash.Name = "buttoncash";
            this.buttoncash.Size = new System.Drawing.Size(95, 52);
            this.buttoncash.TabIndex = 11;
            this.buttoncash.Text = "CASH";
            this.buttoncash.UseVisualStyleBackColor = false;
            this.buttoncash.Click += new System.EventHandler(this.buttoncash_Click);
            // 
            // buttonqr
            // 
            this.buttonqr.BackColor = System.Drawing.Color.OliveDrab;
            this.buttonqr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonqr.ForeColor = System.Drawing.Color.White;
            this.buttonqr.Location = new System.Drawing.Point(1012, 355);
            this.buttonqr.Name = "buttonqr";
            this.buttonqr.Size = new System.Drawing.Size(95, 52);
            this.buttonqr.TabIndex = 12;
            this.buttonqr.Text = "QR-CODE";
            this.buttonqr.UseVisualStyleBackColor = false;
            this.buttonqr.Click += new System.EventHandler(this.buttonqr_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SlateGray;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1141, 355);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 52);
            this.button3.TabIndex = 14;
            this.button3.Text = "CANCEL";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(519, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 39);
            this.label5.TabIndex = 15;
            this.label5.Text = "VAT 7 % :";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(519, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 39);
            this.label6.TabIndex = 16;
            this.label6.Text = "TOTAL :";
            // 
            // VATLABEL
            // 
            this.VATLABEL.AutoSize = true;
            this.VATLABEL.BackColor = System.Drawing.Color.Transparent;
            this.VATLABEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VATLABEL.Location = new System.Drawing.Point(703, 186);
            this.VATLABEL.Name = "VATLABEL";
            this.VATLABEL.Size = new System.Drawing.Size(193, 39);
            this.VATLABEL.TabIndex = 17;
            this.VATLABEL.Text = "----VAT ----";
            this.VATLABEL.Click += new System.EventHandler(this.VATLABEL_Click);
            // 
            // TOTALlabel
            // 
            this.TOTALlabel.AutoSize = true;
            this.TOTALlabel.BackColor = System.Drawing.Color.Transparent;
            this.TOTALlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TOTALlabel.Location = new System.Drawing.Point(694, 94);
            this.TOTALlabel.Name = "TOTALlabel";
            this.TOTALlabel.Size = new System.Drawing.Size(238, 39);
            this.TOTALlabel.TabIndex = 18;
            this.TOTALlabel.Text = "----TOTAL ----";
            this.TOTALlabel.Click += new System.EventHandler(this.TOTALlabel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(519, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(273, 39);
            this.label7.TabIndex = 19;
            this.label7.Text = "TOTAL PRICE :";
            // 
            // totalprice
            // 
            this.totalprice.AutoSize = true;
            this.totalprice.BackColor = System.Drawing.Color.Transparent;
            this.totalprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalprice.Location = new System.Drawing.Point(769, 136);
            this.totalprice.Name = "totalprice";
            this.totalprice.Size = new System.Drawing.Size(185, 39);
            this.totalprice.TabIndex = 20;
            this.totalprice.Text = "---- DC ----";
            // 
            // FormMoney
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Projectgogo.Properties.Resources.adminstock;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.totalprice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TOTALlabel);
            this.Controls.Add(this.VATLABEL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonqr);
            this.Controls.Add(this.buttoncash);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datagridshoworder);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "FormMoney";
            this.Text = "FormMoney";
            ((System.ComponentModel.ISupportInitialize)(this.datagridshoworder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView datagridshoworder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Button buttoncash;
        private System.Windows.Forms.Button buttonqr;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label VATLABEL;
        private System.Windows.Forms.Label TOTALlabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label totalprice;
    }
}