using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Projectgogo
{
    public partial class Form6 : Form
    {
        MySqlConnection conn;
        public Form6()
        {
            InitializeComponent();
            conn = databaseConnection();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
        public MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        private void FillDGV(string id)
        {
            string query = "SELECT * FROM user WHERE ID = @id";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        String query;
        private bool CheckDuplicate(string email, string username)
        {
            bool isDuplicate = false;

            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM user WHERE email = @email OR phone = @phone";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phone", username);

                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    isDuplicate = true;
                }
            }

            return isDuplicate;
        }
        public class fn_1
        {
            public static DataSet getData(MySqlCommand command)
            {
                DataSet ds = new DataSet();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection("connection_string_here"))
                    {
                        conn.Open();
                        command.Connection = conn;
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(ds);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                return ds;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string name = textBox1.Text;
            //string email = textBox2.Text;
            //string username = textBox3.Text;

            {
                MySqlConnection conn = databaseConnection();

                try
                {
                    conn.Open();

                    string name = textBox1.Text;
                    string email = textBox2.Text;
                    string username = textBox3.Text;

                    // สร้างคำสั่ง SQL เพื่อตรวจสอบ Username ว่าซ้ำหรือไม่
                    string queryCheckUsername = "SELECT COUNT(*) FROM user WHERE phone = @phone";
                    MySqlCommand cmdCheckUsername = new MySqlCommand(queryCheckUsername, conn);
                    cmdCheckUsername.Parameters.AddWithValue("@phone", username);

                    // ดึงค่าจำนวน Username ที่ซ้ำจากฐานข้อมูล
                    int countUsername = Convert.ToInt32(cmdCheckUsername.ExecuteScalar());

                    // สร้างคำสั่ง SQL เพื่อตรวจสอบ Email ว่าซ้ำหรือไม่
                    string queryCheckEmail = "SELECT COUNT(*) FROM user WHERE email = @email";
                    MySqlCommand cmdCheckEmail = new MySqlCommand(queryCheckEmail, conn);
                    cmdCheckEmail.Parameters.AddWithValue("@email", email);

                    // ดึงค่าจำนวน Email ที่ซ้ำจากฐานข้อมูล
                    int countEmail = Convert.ToInt32(cmdCheckEmail.ExecuteScalar());


                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username))
                    {
                        MessageBox.Show("แจ้งเตือนกรุณากรอกข้อมูลให้ครบทุกช่อง");
                        return;
                    }
                    else if (countUsername > 0)
                    {
                        MessageBox.Show("Username ของคุณซ้ำ");
                    }
                    else if (countEmail > 0)
                    {
                        MessageBox.Show("Email ของคุณซ้ำ");
                    }
                    else
                    {
                        MySqlCommand cmd1 = new MySqlCommand("INSERT INTO user (Name, email, phone) VALUES (@name, @email, @phone)", conn);
                        cmd1.Parameters.AddWithValue("@name", name);
                        cmd1.Parameters.AddWithValue("@email", email);
                        cmd1.Parameters.AddWithValue("@phone", username);

                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("สมัครสมาชิกสำเร็จ");

                        Form1 nextForm = new Form1();
                        nextForm.Show();
                        this.Hide();

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("error :" + ex.Message);
                }
                finally
                {
                    conn.Close();


                }





            }

        }

    }

}

