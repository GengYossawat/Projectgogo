using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projectgogo
{
    public partial class Form8 : Form
    {
        MySqlConnection conn;
        private object idValue;
        private object nameValue;
        private object priceValue;
        private object amontValue;
        private object sizeValue;
        private object amountValue;
        private DataTable LoadSizes;

        public Form8()
        {
            InitializeComponent();
            comboBoxsize.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxsize.DrawItem += new DrawItemEventHandler(SizeText_DrawItem);
            this.Load += new EventHandler(Form8_Load);
            conn = databaseConnection();
            FillDGV3("");
            //LoadComboBoxData();
            LoadNames();
            comboBoxname.SelectedIndexChanged += comboBoxname_SelectedIndexChanged;
            comboBoxname.SelectedIndexChanged += new System.EventHandler(this.comboBoxname_SelectedIndexChanged);

        }
        private void Form8_Load(object sender, EventArgs e)
        {
            LoadNames();
            LoadAvailableSizes();
        }

        public MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void FillDGV2(string valueToSearch)
        {
           
        }
        private void FillDGV3(string searchValue)
        {
            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM cart";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                datagridshow.DataSource = table;
            }
        }
        private Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 nextForm = new Form2();
            nextForm.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// ปุ่มยืนยันการส่ง order
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        /// 


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // กำหนด connection string - เปลี่ยนตามค่าจริงของคุณ
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // ตรวจสอบว่ามีข้อมูลในตาราง cart หรือไม่
                    string checkQuery = "SELECT COUNT(*) FROM cart";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count == 0)
                        {
                            MessageBox.Show("ไม่มีข้อมูลในตะกร้าสินค้า");
                            return; // ออกจาก method ถ้าไม่มีข้อมูล
                        }
                    }

                    // สร้างป๊อปอัปเพื่อยืนยันการลบข้อมูล
                    DialogResult result = MessageBox.Show("คุณต้องการลบข้อมูลทั้งหมดใน cart ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // สร้างคำสั่ง SQL สำหรับลบข้อมูลทั้งหมดในตาราง cart
                        string deleteQuery = "DELETE FROM cart";
                        using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("ลบข้อมูลทั้งหมดใน cart เรียบร้อยแล้ว");

                        // เรียกใช้ FillDGV3 เพื่ออัปเดต DataGridView
                        FillDGV3("");

                        // อัปเดตยอดรวมใน Label
                        UpdateTotalPrice();
                    }
                    else
                    {
                        MessageBox.Show("การลบข้อมูลถูกยกเลิก");
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
        }
        // สร้างปุ่มแก้ไขสินค้า
        public static class InputBox
        {
            public static DialogResult Show(string title, string promptText, ref string value)
            {
                Form form = new Form();
                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = title;
                label.Text = promptText;
                textBox.Text = value;

                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                DialogResult dialogResult = form.ShowDialog();
                value = textBox.Text;
                return dialogResult;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (datagridshow.SelectedRows.Count > 0)
            {
                DataGridViewRow row = datagridshow.SelectedRows[0];
                int idValue = Convert.ToInt32(row.Cells["ID"].Value);
                string nameValue = row.Cells["Name"].Value.ToString();
                decimal priceValue = Convert.ToDecimal(row.Cells["Price"].Value);
                string sizeValue = row.Cells["Size"].Value.ToString();
                int amountValue = Convert.ToInt32(row.Cells["Amount"].Value);

                string inputAmountStr = "1";
                DialogResult result = InputBox.Show("ระบุจำนวนที่จะลบ", "กรุณากรอกจำนวนที่ต้องการลบ:", ref inputAmountStr);

                if (result == DialogResult.OK && int.TryParse(inputAmountStr, out int inputAmount) && inputAmount > 0)
                {
                    if (inputAmount > amountValue)
                    {
                        MessageBox.Show("จำนวนที่ต้องการลบมากกว่าจำนวนที่มีอยู่");
                        return;
                    }

                    DialogResult confirmResult = MessageBox.Show("คุณต้องการลบข้อมูลที่เลือกใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
                        try
                        {
                            using (MySqlConnection conn = new MySqlConnection(connectionString))
                            {
                                conn.Open();
                                if (inputAmount == amountValue)
                                {
                                    // ลบแถวทั้งหมดถ้าจำนวนที่จะลบเท่ากับจำนวนที่มีอยู่
                                    string deleteQuery = "DELETE FROM cart WHERE ID = @id AND Name = @name AND Price = @price AND Size = @size AND Amount = @amount LIMIT 1";
                                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                                    {
                                        deleteCmd.Parameters.AddWithValue("@id", idValue);
                                        deleteCmd.Parameters.AddWithValue("@name", nameValue);
                                        deleteCmd.Parameters.AddWithValue("@price", priceValue);
                                        deleteCmd.Parameters.AddWithValue("@size", sizeValue);
                                        deleteCmd.Parameters.AddWithValue("@amount", amountValue);
                                        deleteCmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // อัปเดตจำนวนถ้าจำนวนที่จะลบน้อยกว่าจำนวนที่มีอยู่
                                    string updateQuery = "UPDATE cart SET Amount = Amount - @inputAmount WHERE ID = @id AND Name = @name AND Price = @price AND Size = @size AND Amount = @amount";
                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@inputAmount", inputAmount);
                                        updateCmd.Parameters.AddWithValue("@id", idValue);
                                        updateCmd.Parameters.AddWithValue("@name", nameValue);
                                        updateCmd.Parameters.AddWithValue("@price", priceValue);
                                        updateCmd.Parameters.AddWithValue("@size", sizeValue);
                                        updateCmd.Parameters.AddWithValue("@amount", amountValue);
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            MessageBox.Show("ลบข้อมูลที่เลือกใน cart เรียบร้อยแล้ว");

                            // อัปเดต DataGridView
                            FillDGV3("");

                            // อัปเดตยอดรวมใน Label
                            UpdateTotalPrice();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("การลบข้อมูลถูกยกเลิก");
                    }
                }
                else
                {
                    MessageBox.Show("กรุณากรอกจำนวนที่ถูกต้อง (ตัวเลขที่มากกว่า 0)");
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการลบ");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }
        private void UpdateTotalPrice()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT SUM(Amount * Price) AS TotalPrice FROM cart";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            decimal totalPrice = Convert.ToDecimal(result);
                            label5.Text = totalPrice.ToString("C2");
                        }
                        else
                        {
                            label5.Text = "0.00";
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการคำนวณยอดรวม: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormMoney nextForm = new FormMoney();
            nextForm.Show();
            this.Hide();
        }
        private void LoadNames()
        {
            comboBoxname.Items.Clear(); // เคลียร์รายการเก่าก่อน
            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;"; // เชื่อมต่อกับฐานข้อมูล MySQL

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // ดึงชื่อที่ไม่ซ้ำกันจากตาราง stockpk
                    string query = "SELECT DISTINCT name FROM stockpk";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxname.Items.Add(reader.GetString("name")); // เพิ่มชื่อที่ไม่ซ้ำกันลงใน comboBoxname
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
            LoadAvailableSizes();
        }
        private void comboBoxname_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableSizes();
        }
        

        private void numberam_ValueChanged(object sender, EventArgs e)
        {

        }



        private void buttonselect_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีการเลือกข้อมูลใน comboBoxsize หรือไม่
            if (comboBoxsize.SelectedItem == null)
            {
                MessageBox.Show("กรุณาเลือกขนาดจากรายการ");
                return;
            }

            // ตรวจสอบว่ามีการเลือกข้อมูลใน comboBoxname หรือไม่
            if (comboBoxname.SelectedItem == null)
            {
                MessageBox.Show("กรุณาเลือกชื่อจากรายการ");
                return;
            }

            string selectedSize = comboBoxsize.SelectedItem.ToString();
            string selectedName = comboBoxname.SelectedItem.ToString();
            decimal selectedAmount = numberam.Value;

            if (selectedAmount < 1)
            {
                MessageBox.Show("จำนวนต้องไม่น้อยกว่า 1");
                return;
            }

            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;"; // เชื่อมต่อกับฐานข้อมูล MySQL

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // สร้างคำสั่ง SQL เพื่อดึงข้อมูลที่ตรงกับชื่อและขนาด
                    string query = "SELECT id, amount, price FROM stockpk WHERE name = @name AND size = @size";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", selectedName);
                    cmd.Parameters.AddWithValue("@size", selectedSize);

                    int id = 0;
                    decimal amountInDb = 0;
                    decimal price = 0;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader.GetInt32("id");
                            amountInDb = reader.GetDecimal("amount");
                            price = reader.GetDecimal("price");
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลในฐานข้อมูล");
                            return;
                        }
                    }

                    // ตรวจสอบว่ามีข้อมูลในตาราง cart ที่ซ้ำกันหรือไม่
                    string checkCartQuery = "SELECT amount FROM cart WHERE id = @id";
                    MySqlCommand checkCartCmd = new MySqlCommand(checkCartQuery, conn);
                    checkCartCmd.Parameters.AddWithValue("@id", id);

                    object cartResult = checkCartCmd.ExecuteScalar();

                    decimal totalCartAmount = selectedAmount;
                    if (cartResult != null)
                    {
                        // คำนวณจำนวนรวมในตะกร้า
                        decimal amountInCart = Convert.ToDecimal(cartResult);
                        totalCartAmount += amountInCart;
                    }

                    // ตรวจสอบว่าจำนวนรวมในตะกร้าไม่เกินจำนวนในฐานข้อมูล
                    if (totalCartAmount <= amountInDb)
                    {
                        if (cartResult != null)
                        {
                            // อัปเดตจำนวนในตาราง cart
                            string updateCartQuery = "UPDATE cart SET amount = @newAmount, price = @price WHERE id = @id";
                            MySqlCommand updateCartCmd = new MySqlCommand(updateCartQuery, conn);
                            updateCartCmd.Parameters.AddWithValue("@newAmount", totalCartAmount);
                            updateCartCmd.Parameters.AddWithValue("@price", price);
                            updateCartCmd.Parameters.AddWithValue("@id", id);

                            updateCartCmd.ExecuteNonQuery();

                            MessageBox.Show("อัปเดตจำนวนในตะกร้าสำเร็จ");
                        }
                        else
                        {
                            // เพิ่มข้อมูลใหม่ลงในตาราง cart
                            string insertQuery = "INSERT INTO cart (id, name, size, amount, price) VALUES (@id, @name, @size, @amount, @price)";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@id", id);
                            insertCmd.Parameters.AddWithValue("@name", selectedName);
                            insertCmd.Parameters.AddWithValue("@size", selectedSize);
                            insertCmd.Parameters.AddWithValue("@amount", selectedAmount);
                            insertCmd.Parameters.AddWithValue("@price", price);

                            insertCmd.ExecuteNonQuery();

                            MessageBox.Show("เพิ่มข้อมูลลงในตะกร้าสำเร็จ");
                        }

                        // โหลดข้อมูลใหม่ใน DataGridView
                        FillDGV3("");
                        UpdateTotalPrice();
                    }
                    else
                    {
                        MessageBox.Show("จำนวนรวมในตะกร้าเกินจำนวนในฐานข้อมูล");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
        }


        private void datagridshow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // event สำหรับวาดรายการขนาดในสีที่แตกต่างกัน
        private void SizeText_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = comboBoxsize.Items[e.Index].ToString();
            bool sizeExists = LoadSizes != null && LoadSizes.Select($"size = '{text}'").Length > 0;

            Color customBlueColor = Color.FromArgb(255, 0, 120, 215);
            Color textColor = sizeExists ? Color.Black : Color.Gray;
            Color backgroundColor = e.State.HasFlag(DrawItemState.Selected) ? customBlueColor : e.BackColor;

            if (e.State.HasFlag(DrawItemState.Selected))
            {
                textColor = Color.White;
            }

            e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.Bounds);
            e.Graphics.DrawString(text, e.Font, new SolidBrush(textColor), e.Bounds);
            e.DrawFocusRectangle();

            // เพิ่มส่วนนี้เพื่อดึงข้อมูล Amount เมื่อเลือก size
            if (e.State.HasFlag(DrawItemState.Selected))
            {
                UpdateAmountTextbox(text);
            }
        }

        private void UpdateAmountTextbox(string selectedSize)
        {
            // ใช้ MySqlConnection แทน SqlConnection เนื่องจากคุณใช้ MySQL
            using (MySqlConnection connection = databaseConnection()) // เรียกใช้ฟังก์ชัน databaseConnection() ที่คุณมีอยู่แล้ว
            using (MySqlCommand command = new MySqlCommand("SELECT Amount FROM stockpk WHERE size = @size AND name = @name", connection))
            {
                // เพิ่ม Parameter สำหรับ name ด้วย
                command.Parameters.AddWithValue("@size", selectedSize);
                command.Parameters.AddWithValue("@name", comboBoxname.Text); // ใช้ค่าจาก comboBoxname

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int amount))
                {
                    textBox1.Text = amount.ToString();
                }
                else
                {
                    textBox1.Text = "0";
                }
            }
        }
        private void LoadAvailableSizes()
        {
            string searchText = comboBoxname.Text; // รับข้อมูลที่ป้อน

            // สร้าง DataTable เพื่อทำการค้นหา
            LoadSizes = new DataTable();

            using (MySqlConnection conn = databaseConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT size FROM stockpk WHERE name = @name", conn);
                cmd.Parameters.AddWithValue("@name", searchText);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(LoadSizes);
            }
        }


        private void comboBoxsize_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadAvailableSizes();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
