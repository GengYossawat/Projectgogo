using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static QRCoder.PayloadGenerator;

namespace Projectgogo
{
    public partial class Form7 : Form
    {
        MySqlConnection conn;
        public Form7()
        {
            InitializeComponent();
            conn = databaseConnection();
            FillDGV("");
        }
        public MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        private void FillDGV(string valueToSearch)
        {
            string query = "SELECT * FROM user WHERE CONCAT(Name,phone) LIKE '%" + valueToSearch + "%'";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

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

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox5.Text.Trim();
            string name = textBox1.Text.Trim();
            string phone = textBox3.Text.Trim();
            string address = textBox2.Text.Trim();

            // ตรวจสอบความถูกต้องของหมายเลขโทรศัพท์
            if (!IsPhoneNumberValid2(phone))
            {
                MessageBox.Show("โปรดป้อนหมายเลขโทรศัพท์ที่ถูกต้อง (10 หลัก และเริ่มต้นด้วย 0)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบความสมบูรณ์ของข้อมูล
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบทุกช่อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบว่า id, name, และ phone ไม่ซ้ำกับข้อมูลที่มีอยู่แล้ว
            if (IsDuplicate2(id, name, phone))
            {
                MessageBox.Show("ข้อมูลที่ป้อนซ้ำกับที่มีอยู่แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // สร้างคำสั่ง SQL INSERT
            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO user(ID, Name, phone, address) VALUES (@id, @name, @phone, @address)", conn))
            {
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
                cmd.Parameters.Add("@address", MySqlDbType.VarChar).Value = address;

                // ดำเนินการเพิ่มข้อมูล
                ExecMyQuery2(cmd, "Data Inserted");
                FillDGV("");
            }
        }
        private bool IsPhoneNumberValid2(string phone)
        {
            // สมมุติว่าหมายเลขโทรศัพท์ที่ถูกต้องคือหมายเลข 10 หลัก และเริ่มต้นด้วย 0
            return phone.Length == 10 && phone.StartsWith("0") && phone.All(char.IsDigit);
        }

        private bool IsDuplicate2(string id, string name, string phone)
        {
            // ตัวอย่างโค้ดเพื่อการตรวจสอบข้อมูลซ้ำ (ควรทำการเชื่อมต่อฐานข้อมูลเพื่อตรวจสอบจริง)
            string query = "SELECT COUNT(*) FROM user WHERE ID = @id OR Name = @name OR phone = @phone";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return count > 0;
            }
        }

        private void ExecMyQuery2(MySqlCommand cmd, string successMessage)
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

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox5.Text;
            string name = textBox1.Text;
            string phone = textBox3.Text;
            string address = textBox2.Text;

            // ตรวจสอบความถูกต้องของหมายเลขโทรศัพท์
            if (!IsPhoneNumberValid(phone))
            {
                MessageBox.Show("โปรดป้อนหมายเลขโทรศัพท์ที่ถูกต้อง (10 หลัก และเริ่มต้นด้วย 0)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบความสมบูรณ์ของข้อมูล
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบทุกช่อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // ตรวจสอบว่า id, name, และ phone ไม่ซ้ำกับข้อมูลที่มีอยู่แล้ว
            if (IsDuplicate(id, name, phone))
            {
                MessageBox.Show("ข้อมูลที่ป้อนซ้ำกับที่มีอยู่แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ยกเลิกการดำเนินการถัดไป
            }

            // สร้างคำสั่ง SQL UPDATE
            MySqlCommand cmd = new MySqlCommand("UPDATE user SET Name=@name, phone=@phone , address=@address WHERE ID=@id", conn);
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@address", MySqlDbType.VarChar).Value = address;

            // ดำเนินการปรับปรุงข้อมูล
            ExecMyQuery(cmd, "Data Updated");
            FillDGV("");
        }
        private bool IsDuplicate(string id, string name, string phone)
        {
            // ตรวจสอบว่ามีข้อมูลที่ซ้ำกับที่มีอยู่แล้วหรือไม่
            try
            {
                // เช็คการเชื่อมต่อ
                if (conn == null || conn.State != ConnectionState.Open)
                {
                    // กำหนดการเชื่อมต่อ
                    conn = new MySqlConnection("datasource = 127.0.0.1; port = 3306; username = root; password =; database = stock;");
                    conn.Open();
                }

                // ใช้คำสั่ง SQL query เพื่อตรวจสอบว่ามีข้อมูล id, name, และ phone ที่ซ้ำกับที่กำลังแก้ไขหรือไม่
                string query = "SELECT COUNT(*) FROM user WHERE Name = @name AND phone = @phone AND ID != @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@id", id);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                return false;
            }
            finally
            {
                // ตรวจสอบและปิดการเชื่อมต่อ (หากเปิด)
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // ตรวจสอบว่าเป็นตัวเลขหรือไม่
            if (!phoneNumber.All(char.IsDigit))
                return false;

            // ตรวจสอบว่ามี 10 หลักหรือไม่ และเริ่มต้นด้วย 0
            return phoneNumber.Length == 10 && phoneNumber.StartsWith("0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM user WHERE Name = @name", conn);

            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            ExecMyQuery(cmd, "Data Deleted");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            FillDGV(textBox4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 nextForm = new Form2();
            nextForm.Show();
            this.Hide();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
