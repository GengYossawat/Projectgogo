using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projectgogo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            label1.Click += new EventHandler(label1_Click);
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void dataequipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textuser_TextChanged(object sender, EventArgs e)
        {

        }
        private void textpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textuser.Text != "admin" && textpassword.Text != "123")
            {
                MessageBox.Show("กรอกชื่อ หรือ รหัสผิด,\nกรุณากรอกใหม่");
            }
            else
            {
                MessageBox.Show("ยินดีต้อนรับ");
                this.Hide();

                Form2 nextForm = new Form2();
                nextForm.Show();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            { 
                textpassword.UseSystemPasswordChar = false;
            }
            else 
            {
                textpassword.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ยังไม่เปิดให้สมัครแอดมิน");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            info nextForm = new info();
            nextForm.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // URL ของ Google Maps
            string url = "https://maps.app.goo.gl/zCJnbqTmFhNKVV4XA";

            // เปิดเบราว์เซอร์เริ่มต้นและนำทางไปยัง URL
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        public static implicit operator Form1(Form2 v)
        {
            throw new NotImplementedException();
        }
    }
}

