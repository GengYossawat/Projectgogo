namespace Projectgogo
{
    partial class Form9
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
            this.dateTimeDAY = new System.Windows.Forms.DateTimePicker();
            this.ShowDayHist = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeMonth = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowMonthHist = new System.Windows.Forms.Label();
            this.dateTimeYear = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.ShowYearHist = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Bestselling = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ShowCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimeDAY
            // 
            this.dateTimeDAY.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeDAY.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeDAY.Location = new System.Drawing.Point(121, 169);
            this.dateTimeDAY.Name = "dateTimeDAY";
            this.dateTimeDAY.Size = new System.Drawing.Size(386, 53);
            this.dateTimeDAY.TabIndex = 3;
            this.dateTimeDAY.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // ShowDayHist
            // 
            this.ShowDayHist.AutoSize = true;
            this.ShowDayHist.BackColor = System.Drawing.Color.Transparent;
            this.ShowDayHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowDayHist.Location = new System.Drawing.Point(359, 243);
            this.ShowDayHist.Name = "ShowDayHist";
            this.ShowDayHist.Size = new System.Drawing.Size(136, 46);
            this.ShowDayHist.TabIndex = 4;
            this.ShowDayHist.Text = "0.00 $";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(112, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 46);
            this.label1.TabIndex = 5;
            this.label1.Text = "Price Day :";
            // 
            // dateTimeMonth
            // 
            this.dateTimeMonth.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeMonth.Location = new System.Drawing.Point(121, 319);
            this.dateTimeMonth.Name = "dateTimeMonth";
            this.dateTimeMonth.Size = new System.Drawing.Size(386, 53);
            this.dateTimeMonth.TabIndex = 6;
            this.dateTimeMonth.ValueChanged += new System.EventHandler(this.dateTimeMonth_ValueChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(112, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 46);
            this.label2.TabIndex = 7;
            this.label2.Text = "Price Month :";
            // 
            // ShowMonthHist
            // 
            this.ShowMonthHist.AutoSize = true;
            this.ShowMonthHist.BackColor = System.Drawing.Color.Transparent;
            this.ShowMonthHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowMonthHist.Location = new System.Drawing.Point(359, 391);
            this.ShowMonthHist.Name = "ShowMonthHist";
            this.ShowMonthHist.Size = new System.Drawing.Size(136, 46);
            this.ShowMonthHist.TabIndex = 8;
            this.ShowMonthHist.Text = "0.00 $";
            // 
            // dateTimeYear
            // 
            this.dateTimeYear.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeYear.Location = new System.Drawing.Point(121, 464);
            this.dateTimeYear.Name = "dateTimeYear";
            this.dateTimeYear.Size = new System.Drawing.Size(386, 53);
            this.dateTimeYear.TabIndex = 9;
            this.dateTimeYear.ValueChanged += new System.EventHandler(this.dateTimeYear_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(112, 539);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 46);
            this.label3.TabIndex = 10;
            this.label3.Text = "Price Year :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ShowYearHist
            // 
            this.ShowYearHist.AutoSize = true;
            this.ShowYearHist.BackColor = System.Drawing.Color.Transparent;
            this.ShowYearHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowYearHist.Location = new System.Drawing.Point(359, 539);
            this.ShowYearHist.Name = "ShowYearHist";
            this.ShowYearHist.Size = new System.Drawing.Size(136, 46);
            this.ShowYearHist.TabIndex = 11;
            this.ShowYearHist.Text = "0.00 $";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Projectgogo.Properties.Resources.รวยๆ_เฮงๆ;
            this.pictureBox1.Image = global::Projectgogo.Properties.Resources.รวยๆ_เฮงๆ;
            this.pictureBox1.Location = new System.Drawing.Point(704, 183);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(373, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 616);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 53);
            this.button1.TabIndex = 13;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(574, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(630, 46);
            this.label4.TabIndex = 14;
            this.label4.Text = "Best selling shoes of the month :";
            // 
            // Bestselling
            // 
            this.Bestselling.AutoSize = true;
            this.Bestselling.BackColor = System.Drawing.Color.Transparent;
            this.Bestselling.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bestselling.Location = new System.Drawing.Point(620, 503);
            this.Bestselling.Name = "Bestselling";
            this.Bestselling.Size = new System.Drawing.Size(504, 31);
            this.Bestselling.TabIndex = 15;
            this.Bestselling.Text = "Best Selling in this mouth : Click Here";
            this.Bestselling.Click += new System.EventHandler(this.Bestselling_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(695, 596);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 46);
            this.label6.TabIndex = 16;
            // 
            // ShowCount
            // 
            this.ShowCount.AutoSize = true;
            this.ShowCount.BackColor = System.Drawing.Color.Transparent;
            this.ShowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowCount.ForeColor = System.Drawing.Color.Transparent;
            this.ShowCount.Location = new System.Drawing.Point(840, 596);
            this.ShowCount.Name = "ShowCount";
            this.ShowCount.Size = new System.Drawing.Size(0, 46);
            this.ShowCount.TabIndex = 17;
            // 
            // Form9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Projectgogo.Properties.Resources.adminstock;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.ShowCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Bestselling);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ShowYearHist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimeYear);
            this.Controls.Add(this.ShowMonthHist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeMonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShowDayHist);
            this.Controls.Add(this.dateTimeDAY);
            this.Name = "Form9";
            this.Text = "Form9";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimeDAY;
        private System.Windows.Forms.Label ShowDayHist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ShowMonthHist;
        private System.Windows.Forms.DateTimePicker dateTimeYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ShowYearHist;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Bestselling;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ShowCount;
    }
}