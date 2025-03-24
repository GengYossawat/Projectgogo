using MySql.Data.MySqlClient;
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
using System.IO;

namespace Projectgogo
{
    public partial class Form5 : Form
    {
        MySqlConnection conn;
        public Form5()
        {
            InitializeComponent();
            conn = databaseConnection();
            FillDGV(""); // เรียกเมธอด FillDGV() เพียงครั้งเดียวที่ฟอร์มถูกโหลด
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
        public MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        private void FillDGV(string valueToSearchTextBox4)
        {
            string query = "SELECT * FROM stockpk WHERE CONCAT(ID, Name, Price,Size, Amount, Type) LIKE '%" + valueToSearchTextBox4 + "%'";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AllowUserToAddRows = false;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[6]; // แก้ตำแหน่งคอลัมน์ให้เป็นคอลัมน์ที่เก็บรูปภาพ
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            FillDGV(textBox4.Text);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(opf.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถโหลดรูปภาพ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void ExecMyQuery(MySqlCommand cmd, string myMsg)
        {
            conn.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show(myMsg);
            }
            else
            {
                MessageBox.Show("Query Not Executed");
            }
            conn.Close();

            FillDGV("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่า Name และ Size มีอยู่แล้วในฐานข้อมูลหรือไม่
            if (IsNameAndSizeDuplicate(textBox2.Text, textBox6.Text))
            {
                MessageBox.Show("ชื่อและขนาดนี้มีอยู่แล้ว กรุณาใช้ชื่อหรือขนาดอื่น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบว่ามีรูปภาพใน pictureBox1 หรือไม่
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("กรุณาเพิ่มรูปภาพ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบค่าของ Amount (Amo) ว่าเป็นตัวเลขที่ไม่ติดลบ
            if (!int.TryParse(textBox5.Text, out int amount) || amount < 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนที่ถูกต้อง (0 ขึ้นไป)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบว่าค่า Price (textBox3) เป็นตัวเลขทศนิยมหรือไม่
            if (!decimal.TryParse(textBox3.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("กรุณากรอกราคาที่ถูกต้อง (ตัวเลขเท่านั้น)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // แปลงรูปภาพเป็น byte array
            byte[] img;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                img = ms.ToArray();
            }

            // สร้างคำสั่ง SQL INSERT
            MySqlCommand cmd = new MySqlCommand("INSERT INTO stockpk(ID, Name, Price, Size, Amount, Image, Type) VALUES (@id, @name, @price, @size, @Amo, @img, @type)", conn);

            // กำหนดค่าพารามิเตอร์
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = textBox1.Text;
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
            cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = price; // เปลี่ยนเป็นพารามิเตอร์สำหรับราคา
            cmd.Parameters.Add("@size", MySqlDbType.VarChar).Value = textBox6.Text;
            cmd.Parameters.Add("@Amo", MySqlDbType.Int32).Value = amount; // เปลี่ยนค่าพารามิเตอร์เป็นจำนวนเต็ม
            cmd.Parameters.Add("@type", MySqlDbType.VarChar).Value = textBox7.Text;
            cmd.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            // ดำเนินการเพิ่มข้อมูล
            ExecMyQuery1(cmd, "Data Inserted");
            FillDGV("");
        }



        // ฟังก์ชันตรวจสอบค่า Name และ Size ซ้ำ
        private bool IsNameAndSizeDuplicate(string name, string size)
        {
            string query = "SELECT COUNT(*) FROM stockpk WHERE Name = @name AND Size = @size";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@size", MySqlDbType.VarChar).Value = size;

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return count > 0;
            }
        }

        // ฟังก์ชันดำเนินการคำสั่ง SQL และแสดงผลลัพธ์
        private void ExecMyQuery1(MySqlCommand cmd, string successMessage)
        {
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show(successMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่า Name และ Size ซ้ำกันหรือไม่
            if (IsNameAndSizeDuplicateForUpdate(textBox1.Text, textBox2.Text, textBox6.Text))
            {
                MessageBox.Show("ชื่อและขนาดนี้มีอยู่แล้ว กรุณาใช้ชื่อหรือขนาดอื่น", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบว่ามีรูปภาพใน pictureBox1 หรือไม่
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("กรุณาเพิ่มรูปภาพ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบค่าของ Amount (Amo) ว่าเป็นตัวเลขที่ไม่ติดลบ
            if (!IsAmountValid(textBox5.Text))
            {
                MessageBox.Show("กรุณากรอกจำนวนที่ถูกต้อง (0 ขึ้นไป)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบว่า Price เป็นตัวเลขและไม่ติดลบ
            if (!decimal.TryParse(textBox3.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("กรุณากรอกราคาที่ถูกต้อง (0 ขึ้นไป)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // แปลงรูปภาพเป็น byte array
            byte[] img;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                img = ms.ToArray();
            }

            // สร้างคำสั่ง SQL UPDATE
            MySqlCommand cmd = new MySqlCommand("UPDATE stockpk SET Name=@name, Price=@price, Image=@img, Amount=@Amo, Size=@size, Type=@type WHERE ID=@id", conn);

            // กำหนดค่าพารามิเตอร์
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = textBox1.Text;
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
            cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = price; // กำหนดเป็น Decimal สำหรับ Price
            cmd.Parameters.Add("@size", MySqlDbType.VarChar).Value = textBox6.Text;
            cmd.Parameters.Add("@Amo", MySqlDbType.Int32).Value = int.Parse(textBox5.Text); // แปลงเป็น int
            cmd.Parameters.Add("@type", MySqlDbType.VarChar).Value = textBox7.Text;
            cmd.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            // ดำเนินการอัปเดตข้อมูล
            ExecMyQuery3(cmd, "Data Updated");
            FillDGV("");
        }


        // ฟังก์ชันเพื่อเช็คความถูกต้องของ Amount (Amo)
        private bool IsAmountValid(string amountText)
        {
            // เช็คให้แน่ใจว่ามันเป็นตัวเลขที่มากกว่าหรือเท่ากับ 0
            return int.TryParse(amountText, out int amount) && amount >= 0;
        }



        // ฟังก์ชันตรวจสอบค่า Name และ Size ซ้ำ
        private bool IsNameAndSizeDuplicateForUpdate(string id, string name, string size)
        {
            string query = "SELECT COUNT(*) FROM stockpk WHERE Name = @name AND Size = @size AND ID != @id";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@size", MySqlDbType.VarChar).Value = size;

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return count > 0;
            }
        }

        // ฟังก์ชันดำเนินการคำสั่ง SQL และแสดงผลลัพธ์
        private void ExecMyQuery3(MySqlCommand cmd, string successMessage)
        {
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show(successMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM stockpk WHERE ID = @id", conn);

            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = textBox1.Text;
            ExecMyQuery(cmd, "Data Deleted");
            FillDGV("");

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 nextForm = new Form2();
            nextForm.Show();
            this.Hide();
        }
    }
}
