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

namespace Projectgogo
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalPriceByDay(dateTimeDAY.Value.Date);
        }

        private void CalculateTotalPriceByDay(DateTime selectedDate)
        {
            try
            {
                using (MySqlConnection conn = databaseConnection())
                {
                    conn.Open();

                    string query = "SELECT SUM(price) FROM bill WHERE DATE(date) = @date";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", selectedDate.ToString("yyyy-MM-dd"));

                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            double totalPrice = Convert.ToDouble(result);
                            ShowDayHist.Text = totalPrice.ToString("#,##0.00");
                        }
                        else
                        {
                            ShowDayHist.Text = "0.00";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
        }
        private void CalculateTotalPriceByMonth()
        {
            // รับวันที่ที่ผู้ใช้เลือกจาก dateTimePicker2
            DateTime selectedDate = dateTimeMonth.Value.Date;

            // กำหนดวันที่เริ่มต้นของเดือน
            DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);

            // กำหนดวันที่สิ้นสุดของเดือน
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);


            // เชื่อมต่อกับฐานข้อมูล
            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();

                // สร้างคำสั่ง SQL เพื่อค้นหาข้อมูลในตาราง orderhistory ในวันที่ที่ผู้ใช้เลือก
                string query = $"SELECT SUM(price) FROM bill WHERE DATE(date) BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}'";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Execute คำสั่ง SQL เพื่อดึงข้อมูล price
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        double totalPrice = 0;

                        // ตรวจสอบว่ามีข้อมูลอยู่ในการอ่านหรือไม่
                        if (reader.HasRows)
                        {
                            // วนลูปอ่านข้อมูล price และคำนวณราคารวม
                            while (reader.Read())
                            {
                                // ตรวจสอบว่าคอลัมน์ไม่ว่าง
                                if (!reader.IsDBNull(0))
                                {
                                    totalPrice += reader.GetDouble(0);
                                }
                            }
                        }
                        else
                        {
                            // ถ้าไม่มีข้อมูลในการอ่าน
                            // ตั้งค่าราคารวมเป็น 0 หรืออื่น ๆ ตามที่คุณต้องการ
                            totalPrice = 0;
                        }

                        // แสดงผลลัพธ์ที่ถูกต้อง
                        ShowMonthHist.Text = Math.Round(totalPrice, 2).ToString("#,##0.00");
                    }
                }
            }
        }
        private void CalculateTotalPriceByMonthv2()
        {
            DateTime selectedDate = dateTimeMonth.Value.Date;
            DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();

                string query = $"SELECT name, SUM(CAST(count AS UNSIGNED)) AS totalCount " +
                               $"FROM history " +
                               $"WHERE DATE(date) BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}' " +
                               $"GROUP BY name " +
                               $"ORDER BY totalCount DESC " +
                               $"LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Bestselling.Text = reader.GetString("name");
                            //ShowCount.Text = reader.GetInt32("totalCount").ToString();
                            ShowProductImage(reader.GetString("name")); // เรียกใช้ ShowProductImage ทันที
                        }
                        else
                        {
                            Bestselling.Text = "ไม่มีรายการสินค้าในเดือนนี้";
                            //ShowCount.Text = "0";
                            //pictureBox1.Image = Properties.Resources.VDOSPORTGIFFFFF; // กำหนดรูปภาพเริ่มต้น
                        }
                    }
                }
            }
        }
        private void CalculateBestSellingByMonthV3()
        {
            // กำหนดวันที่เริ่มต้นและสิ้นสุดของเดือนที่เลือก
            DateTime selectedDate = dateTimeMonth.Value.Date;
            DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();

                // Query เพื่อค้นหาสินค้าที่ขายดีที่สุดในเดือนที่เลือก
                string query = $"SELECT name, SUM(CAST(count AS UNSIGNED)) AS totalCount " +
                               $"FROM history " +
                               $"WHERE DATE(date) BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}' " +
                               $"GROUP BY name " +
                               $"ORDER BY totalCount DESC " +
                               $"LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // ตรวจสอบว่าพบข้อมูลสินค้าที่ขายดีที่สุดหรือไม่
                        if (reader.Read())
                        {
                            // แสดงชื่อสินค้าที่ขายดีที่สุดและจำนวนที่ขายได้
                            Bestselling.Text = $"สินค้าที่ขายดีที่สุด: {reader.GetString("name")} \nจำนวนที่ขายได้: {reader.GetInt32("totalCount")}";
                            //ShowCount.Text = reader.GetInt32("totalCount").ToString();
                        }
                        else
                        {
                            // กรณีที่ไม่มีการขายสินค้าในเดือนนี้
                            Bestselling.Text = "ไม่มีรายการสินค้าในเดือนนี้";
                            //ShowCount.Text = "0";
                        }
                    }
                }
            }
        }

        private void ShowProductImage(string bestsellingProduct)
        {
            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();

                string query = $"SELECT photo FROM stockpk WHERE name = @productName";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // ใช้ Parameter เพื่อป้องกัน SQL Injection
                    cmd.Parameters.AddWithValue("@productName", bestsellingProduct);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) // ตรวจสอบว่าข้อมูลรูปภาพไม่เป็น NULL
                            {
                                byte[] imageData = (byte[])reader["photo"];
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                // ถ้าไม่มีรูปภาพในฐานข้อมูลให้กำหนดรูปภาพเริ่มต้นที่ตั้งไว้
                                //pictureBox1.Image = Properties.Resources.VDOSPORTGIFFFFF;
                            }
                        }
                    }
                }
            }
        }



        private void dateTimeMonth_ValueChanged_1(object sender, EventArgs e)
        {
            CalculateTotalPriceByMonth();
        }

        private void CalculateTotalPriceByYear()
        {
            // รับปีที่ผู้ใช้เลือกจาก dateTimePicker2
            int selectedYear = dateTimeYear.Value.Year;

            // กำหนดวันที่เริ่มต้นของปี
            DateTime startDate = new DateTime(selectedYear, 1, 1);

            // กำหนดวันที่สิ้นสุดของปี
            DateTime endDate = new DateTime(selectedYear, 12, 31);

            // เชื่อมต่อกับฐานข้อมูล
            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();

                // สร้างคำสั่ง SQL เพื่อค้นหาข้อมูลในตาราง orderhistory ในช่วงปีที่ผู้ใช้เลือก
                string query = $"SELECT SUM(price) FROM bill WHERE DATE(date) BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}'";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Execute คำสั่ง SQL เพื่อดึงข้อมูล price
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        double totalPrice = 0;

                        // ตรวจสอบว่ามีข้อมูลอยู่ในการอ่านหรือไม่
                        if (reader.HasRows)
                        {
                            // วนลูปอ่านข้อมูล price และคำนวณราคารวม
                            while (reader.Read())
                            {
                                // ตรวจสอบว่าคอลัมน์ไม่ว่าง
                                if (!reader.IsDBNull(0))
                                {
                                    totalPrice += reader.GetDouble(0);
                                }
                            }
                        }
                        else
                        {
                            // ถ้าไม่มีข้อมูลในการอ่าน
                            // ตั้งค่าราคารวมเป็น 0 หรืออื่น ๆ ตามที่คุณต้องการ
                            totalPrice = 0;
                        }

                        // แสดงผลลัพธ์ที่ถูกต้อง
                        ShowYearHist.Text = Math.Round(totalPrice, 2).ToString("#,##0.00");
                    }
                }
            }
        }

        private void dateTimeYear_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalPriceByYear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 nextForm = new Form2();
            nextForm.Show();
            this.Hide();
        }

        private void Bestselling_Click(object sender, EventArgs e)
        {
            // กำหนดวันที่เริ่มต้นและสิ้นสุดของเดือนที่เลือก
            DateTime selectedDate = dateTimeMonth.Value.Date;
            DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();

                // Query เพื่อค้นหาสินค้าที่ขายดีที่สุดในเดือนที่เลือก
                string query = $"SELECT nameorder, SUM(CAST(amount AS UNSIGNED)) AS totalAmount " +
                               $"FROM bill " +
                               $"WHERE DATE(date) BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}' " +
                               $"GROUP BY nameorder " +
                               $"ORDER BY totalAmount DESC " +
                               $"LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // ตรวจสอบว่าพบข้อมูลสินค้าที่ขายดีที่สุดหรือไม่
                        if (reader.Read())
                        {
                            // แสดงชื่อสินค้าที่ขายดีที่สุดและจำนวนที่ขายได้
                            Bestselling.Text = $"สินค้าที่ขายดีที่สุด: {reader.GetString("nameorder")} \nจำนวนที่ขายได้: {reader.GetInt32("totalAmount")}";
                            ShowCount.Text = reader.GetInt32("totalAmount").ToString();
                        }
                        else
                        {
                            // กรณีที่ไม่มีการขายสินค้าในเดือนนี้
                            Bestselling.Text = "ไม่มีรายการสินค้าในเดือนนี้";
                            ShowCount.Text = "0";
                        }
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
