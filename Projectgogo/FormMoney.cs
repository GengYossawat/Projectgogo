using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using QRCoder;
using Saladpuk.PromptPay.Contracts;
using Saladpuk.PromptPay.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saladpuk.PromptPay;
using System.Globalization;
using System.IO;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
//using iTextSharp.text;


namespace Projectgogo
{
    public partial class FormMoney : Form
    {
        decimal totalPriceIncVat;
        public FormMoney()
        {
            InitializeComponent();
            FillDGV3("");
            UpdateTotalPrice();
            GenerateQRCode();
            UpdateTotal();
        }

        private void FillDGV3(string valueToSearch)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID, Name, Price, Size, Amount FROM cart WHERE CONCAT(Name, Price) LIKE @valueToSearch";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@valueToSearch", "%" + valueToSearch + "%");

                    MySqlDataAdapter adapter3 = new MySqlDataAdapter(command);
                    DataTable table3 = new DataTable();
                    adapter3.Fill(table3);
                    datagridshoworder.DataSource = table3;
                }
            }

            datagridshoworder.RowTemplate.Height = 40;
            datagridshoworder.AllowUserToAddRows = false;
        }

        private void datagridshoworder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDGV3("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isConfirmed = false;

        public object PromptPayFactory { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            string phoneNumber = textBox1.Text;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์");
                return;
            }

            string name = GetNameByPhoneNumber(phoneNumber);

            if (!string.IsNullOrEmpty(name))
            {
                if (!isConfirmed)
                {
                    DialogResult result = MessageBox.Show($"พบข้อมูลชื่อ: {name} \nยืนยันว่าใช่หรือไม่ \nกดเพื่อรับส่วนลด 5%?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // คำนวณส่วนลด 5%
                        decimal discountRate = 0.05m;
                        decimal totalPrice = Convert.ToDecimal(label4.Text.Replace("฿", "").Replace(",", ""));
                        decimal discountAmount = totalPrice * discountRate;  // คำนวณส่วนลด
                        decimal discountedPrice = totalPrice - discountAmount;  // หักส่วนลดจากยอดรวม

                        // อัปเดตราคาหลังหักส่วนลดใน label4
                        label4.Text = discountedPrice.ToString("C2", CultureInfo.CurrentCulture);

                        // แสดงส่วนลด 5% ใน totalprice.Text
                        totalprice.Text = discountAmount.ToString("C2", CultureInfo.CurrentCulture);

                        textBox1.Text = name;
                        isConfirmed = true;

                        // สร้าง QR Code ใหม่หลังจากการคำนวณยอดรวมล่าสุด
                        GenerateQRCode();
                        UpdateVAT();
                    }
                    else
                    {
                        textBox1.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("คุณได้ทำการยืนยันข้อมูลนี้ไปแล้ว");
                }
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูลสำหรับเบอร์โทรศัพท์นี้");
            }
        }



        private string GetNameByPhoneNumber(string phoneNumber)
        {
            string name = string.Empty;

            try
            {
                string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT name FROM user WHERE phone = @phone";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", phoneNumber);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name = reader["name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                LogMySqlException(mysqlEx);
                MessageBox.Show("MySQL error occurred: " + mysqlEx.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return name;
        }

        private void LogMySqlException(MySqlException mysqlEx)
        {
            // บันทึกข้อมูลข้อผิดพลาด MySQL
            // คุณสามารถเพิ่มโค้ดสำหรับบันทึกข้อผิดพลาดลงไฟล์หรือฐานข้อมูลได้ที่นี่
            Console.WriteLine(mysqlEx.ToString());
        }

        private void LogException(Exception ex)
        {
            // บันทึกข้อมูลข้อผิดพลาดทั่วไป
            // คุณสามารถเพิ่มโค้ดสำหรับบันทึกข้อผิดพลาดลงไฟล์หรือฐานข้อมูลได้ที่นี่
            Console.WriteLine(ex.ToString());
        }

        private void UpdateTotalPrice()
        {
            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;";
            decimal totalPriceExVat = 0;
            decimal totalPriceIncVat = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT SUM(Amount * Price) AS TotalPrice FROM cart";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            totalPriceExVat = Convert.ToDecimal(result);
                            decimal vat = totalPriceExVat;
                            totalPriceIncVat = totalPriceExVat;
                        }
                        else
                        {
                            totalPriceIncVat = 0;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการคำนวณยอดรวม: " + ex.Message);
            }

            // แสดงผลใน label4
            label4.Text = totalPriceIncVat.ToString("C2", CultureInfo.CurrentCulture);
        }
        private void UpdateTotal()
        {
            string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=stock;";
            decimal totalPriceExVat = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT SUM(Amount * Price) AS TotalPrice FROM cart";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            totalPriceExVat = Convert.ToDecimal(result);
                        }
                        else
                        {
                            totalPriceExVat = 0;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการคำนวณยอดรวม: " + ex.Message);
            }

            // แสดงผลใน label4 โดยไม่รวม VAT
            TOTALlabel.Text = totalPriceExVat.ToString("C2", CultureInfo.CurrentCulture);
        }
        private decimal UpdateVAT()
        {
            decimal vatRate = 0.07m; // อัตรา VAT 7%

            // แปลงยอดใน label4 (ราคาหลังหักส่วนลด) เป็น decimal
            decimal priceAfterDiscount = Convert.ToDecimal(label4.Text.Replace("฿", "").Replace(",", ""));

            // คำนวณจำนวน VAT จากราคาหลังลด
            decimal vatAmount = priceAfterDiscount * vatRate;

            // แสดงจำนวน VAT ที่ต้องเสีย
            VATLABEL.Text = vatAmount.ToString("C2", CultureInfo.CurrentCulture);

            return vatAmount; // ส่งจำนวน VAT ที่คำนวณได้กลับไป
        }

        private void VATLABEL_Click(object sender, EventArgs e)
        {
            
        }

        private void TOTALlabel_Click(object sender, EventArgs e)
        {
            UpdateTotal();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private void GenerateQRCode()
        {
            try
            {
                // รับค่า totalWithVat จาก TextBox ที่แสดงผลรวมทั้งหมดพร้อม VAT
                decimal totalWithVat;
                if (!decimal.TryParse(label4.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out totalWithVat))
                {
                    MessageBox.Show("รูปแบบของยอดรวมไม่ถูกต้อง");
                    return;
                }

                // แปลงค่า totalWithVat เป็น double
                double qr_price = Convert.ToDouble(totalWithVat);

                // สร้างรหัส QR โดยใช้ PPay
                string qr = PPay.DynamicQR.MobileNumber("0973582102").Amount(qr_price).CreateCreditTransferQrCode();

                // สร้าง QR Code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(8);

                // แสดงรูปภาพใน PictureBox 
                guna2PictureBox1.Image = qrCodeImage;

                // พิมพ์ค่า totalWithVat เพื่อการตรวจสอบ
                Console.WriteLine(totalWithVat);
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการสร้าง QR Code: " + ex.Message);
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            GenerateQRCode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void buttoncash_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คุณต้องการยืนยันที่จะจ่ายด้วยเงินสดหรือไม่?", "ยืนยันการจ่ายเงินสด", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string connectionString = "Server=127.0.0.1;Port=3306;Database=stock;Uid=root;Pwd=;";

                decimal totalWithVat;
                if (!decimal.TryParse(label4.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out totalWithVat))
                {
                    MessageBox.Show("รูปแบบของยอดรวมไม่ถูกต้อง");
                    return;
                }

                double จำนวนเงินรวม = Convert.ToDouble(totalWithVat);
                string querySelectCart = "SELECT id, name, Amount FROM cart";

                string รายการสินค้า = "";
                string ชื่อลูกค้า = textBox1.Text;
                string วันที่เวลา = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int จำนวนสินค้ารวม = 0;
                List<Tuple<int, int>> cartItems = new List<Tuple<int, int>>(); // To store id and amount

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand commandSelectCart = new MySqlCommand(querySelectCart, connection))
                        {
                            using (MySqlDataReader reader = commandSelectCart.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idOrder = int.Parse(reader["id"].ToString());
                                    string ชื่อสินค้า = reader["name"].ToString();
                                    int จำนวนสินค้า = int.Parse(reader["Amount"].ToString());

                                    รายการสินค้า += $"{ชื่อสินค้า}, ";
                                    จำนวนสินค้ารวม += จำนวนสินค้า;
                                    cartItems.Add(new Tuple<int, int>(idOrder, จำนวนสินค้า));
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(รายการสินค้า))
                        {
                            รายการสินค้า = รายการสินค้า.Substring(0, รายการสินค้า.Length - 2);
                        }

                        string queryInsertBill = "INSERT INTO bill (idorder, nameorder, price, amount, nameuser, date, pay) VALUES (@idorder, @nameorder, @price, @amount, @nameuser, @date, @pay)";

                        using (MySqlCommand commandInsertBill = new MySqlCommand(queryInsertBill, connection))
                        {
                            commandInsertBill.Parameters.AddWithValue("@idorder", cartItems[0].Item1); // Assuming idorder is the id of the first item
                            commandInsertBill.Parameters.AddWithValue("@nameorder", รายการสินค้า);
                            commandInsertBill.Parameters.AddWithValue("@price", จำนวนเงินรวม);
                            commandInsertBill.Parameters.AddWithValue("@amount", จำนวนสินค้ารวม);
                            commandInsertBill.Parameters.AddWithValue("@nameuser", ชื่อลูกค้า);
                            commandInsertBill.Parameters.AddWithValue("@date", วันที่เวลา);
                            commandInsertBill.Parameters.AddWithValue("@pay", "Cash");

                            commandInsertBill.ExecuteNonQuery();
                        }

                        MessageBox.Show("บันทึกข้อมูลในตาราง 'bill' เรียบร้อยแล้ว");

                        foreach (var item in cartItems)
                        {
                            string queryUpdateStock = "UPDATE stockpk SET Amount = Amount - @amount WHERE id = @id";
                            using (MySqlCommand commandUpdateStock = new MySqlCommand(queryUpdateStock, connection))
                            {
                                commandUpdateStock.Parameters.AddWithValue("@amount", item.Item2);
                                commandUpdateStock.Parameters.AddWithValue("@id", item.Item1);

                                commandUpdateStock.ExecuteNonQuery();
                            }
                        }

                        string queryDeleteCart = "DELETE FROM cart";
                        using (MySqlCommand commandDeleteCart = new MySqlCommand(queryDeleteCart, connection))
                        {
                            commandDeleteCart.ExecuteNonQuery();
                        }

                        MessageBox.Show("ลบข้อมูลในตาราง 'cart' เรียบร้อยแล้ว");
                    }
                    GeneratePDFReceipt(label4.Text);
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("ยกเลิกการจ่ายด้วยเงินสด");
            }
        }

        private void buttonqr_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คุณต้องการยืนยันที่จะจ่ายด้วยqr-codeหรือไม่?", "ยืนยันการจ่ายqrcode", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string connectionString = "Server=127.0.0.1;Port=3306;Database=stock;Uid=root;Pwd=;";

                decimal totalWithVat;
                if (!decimal.TryParse(label4.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out totalWithVat))
                {
                    MessageBox.Show("รูปแบบของยอดรวมไม่ถูกต้อง");
                    return;
                }

                double จำนวนเงินรวม = Convert.ToDouble(totalWithVat);
                string querySelectCart = "SELECT id, name, Amount FROM cart";

                string รายการสินค้า = "";
                string ชื่อลูกค้า = textBox1.Text;
                string วันที่เวลา = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int จำนวนสินค้ารวม = 0;
                List<Tuple<int, int>> cartItems = new List<Tuple<int, int>>(); // To store id and amount

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand commandSelectCart = new MySqlCommand(querySelectCart, connection))
                        {
                            using (MySqlDataReader reader = commandSelectCart.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idOrder = int.Parse(reader["id"].ToString());
                                    string ชื่อสินค้า = reader["name"].ToString();
                                    int จำนวนสินค้า = int.Parse(reader["Amount"].ToString());

                                    รายการสินค้า += $"{ชื่อสินค้า}, ";
                                    จำนวนสินค้ารวม += จำนวนสินค้า;
                                    cartItems.Add(new Tuple<int, int>(idOrder, จำนวนสินค้า));
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(รายการสินค้า))
                        {
                            รายการสินค้า = รายการสินค้า.Substring(0, รายการสินค้า.Length - 2);
                        }

                        string queryInsertBill = "INSERT INTO bill (idorder, nameorder, price, amount, nameuser, date, pay) VALUES (@idorder, @nameorder, @price, @amount, @nameuser, @date, @pay)";

                        using (MySqlCommand commandInsertBill = new MySqlCommand(queryInsertBill, connection))
                        {
                            commandInsertBill.Parameters.AddWithValue("@idorder", cartItems[0].Item1); // Assuming idorder is the id of the first item
                            commandInsertBill.Parameters.AddWithValue("@nameorder", รายการสินค้า);
                            commandInsertBill.Parameters.AddWithValue("@price", จำนวนเงินรวม);
                            commandInsertBill.Parameters.AddWithValue("@amount", จำนวนสินค้ารวม);
                            commandInsertBill.Parameters.AddWithValue("@nameuser", ชื่อลูกค้า);
                            commandInsertBill.Parameters.AddWithValue("@date", วันที่เวลา);
                            commandInsertBill.Parameters.AddWithValue("@pay", "Qr-Code");

                            commandInsertBill.ExecuteNonQuery();
                        }

                        MessageBox.Show("บันทึกข้อมูลในตาราง 'bill' เรียบร้อยแล้ว");

                        foreach (var item in cartItems)
                        {
                            string queryUpdateStock = "UPDATE stockpk SET Amount = Amount - @amount WHERE id = @id";
                            using (MySqlCommand commandUpdateStock = new MySqlCommand(queryUpdateStock, connection))
                            {
                                commandUpdateStock.Parameters.AddWithValue("@amount", item.Item2);
                                commandUpdateStock.Parameters.AddWithValue("@id", item.Item1);

                                commandUpdateStock.ExecuteNonQuery();
                            }
                        }

                        string queryDeleteCart = "DELETE FROM cart";
                        using (MySqlCommand commandDeleteCart = new MySqlCommand(queryDeleteCart, connection))
                        {
                            commandDeleteCart.ExecuteNonQuery();
                        }

                        MessageBox.Show("ลบข้อมูลในตาราง 'cart' เรียบร้อยแล้ว");
                    }
                    GeneratePDFReceipt(label4.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("ยกเลิกการจ่ายด้วยเงินสด");
            }
            // เปิดหน้า Form1 หลังจากดำเนินการเสร็จสิ้น
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน (ถ้ามีความต้องการ)
        }


        private void GeneratePDFReceipt(string totalPriceExVat)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = "Receipt.pdf";
                string filePath = Path.Combine(desktopPath, fileName);

                int count = 1;
                while (File.Exists(filePath))
                {
                    fileName = $"Receipt_{count}.pdf";
                    filePath = Path.Combine(desktopPath, fileName);
                    count++;
                }

                using (Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    // โหลดฟอนท์ภาษาไทย
                    BaseFont baseFont = BaseFont.CreateFont("D:\\geng\\Projectgogo\\TH Sarabun New Regular.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font thaiFont = new Font(baseFont, 12);

                    // Adding content to PDF
                    doc.Add(new Paragraph("GRD SHOP", new Font(baseFont, 20)));  // ใช้ฟอนท์ภาษาไทยที่โหลดไว้
                    doc.Add(new Paragraph($"Adress: 408 หมู่ 6 อำเภอสมเด็จ จังหวัดกาฬสินธุ์ รหัสไปรษณีย์ 46150", thaiFont));
                    doc.Add(new Paragraph($"Date: {DateTime.Now:dd/MM/yyyy}", thaiFont));
                    doc.Add(new Paragraph($"User Name: {textBox1.Text}", thaiFont));  // ใช้ฟอนท์ภาษาไทยสำหรับข้อความใน textBox1
                    doc.Add(new Paragraph("\n"));

                    // Adding table
                    PdfPTable table = new PdfPTable(datagridshoworder.Columns.Count);
                    table.WidthPercentage = 100;
                    table.SpacingBefore = 10;
                    table.SpacingAfter = 10;

                    // Adding headers
                    foreach (DataGridViewColumn column in datagridshoworder.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                        cell.BackgroundColor = new BaseColor(240, 240, 240);
                        table.AddCell(cell);
                    }

                    // Adding data rows
                    foreach (DataGridViewRow row in datagridshoworder.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string cellValue = cell.Value?.ToString() ?? string.Empty;  // ตรวจสอบค่าของเซลล์
                            if (decimal.TryParse(cellValue, out decimal number))  // ตรวจสอบว่าค่าเป็นตัวเลขหรือไม่
                            {
                                // ฟอร์แมตตัวเลขให้มีเครื่องหมาย ,
                                cellValue = number.ToString("#,##0.00");
                            }

                            PdfPCell pdfCell = new PdfPCell(new Phrase(cellValue));  // สร้างเซลล์ใหม่ใน PDF
                            table.AddCell(pdfCell);  // เพิ่มเซลล์ในตาราง
                        }
                    }


                    doc.Add(table);

                    // Adding total
                    doc.Add(new Paragraph($"Total: {TOTALlabel.Text}", FontFactory.GetFont("Arial", 12)));
                    doc.Add(new Paragraph($"Discount: {totalprice.Text}", FontFactory.GetFont("Arial", 12)));
                    doc.Add(new Paragraph($"VAT 7%: {VATLABEL.Text}", FontFactory.GetFont("Arial", 12)));
                    doc.Add(new Paragraph($"Total Price + VAT 7%: {label4.Text}", FontFactory.GetFont("Arial", 12)));
                    doc.Close();
                }

                MessageBox.Show($"Receipt has been generated and saved to Desktop as {fileName}", "PDF Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GeneratePDFReceipt(label4.Text);
        }

        // เรียกใช้ฟังก์ชัน GeneratePDFReceipt โดยส่งผ่านค่า label4.Text
        private void SomeButton_Click(object sender, EventArgs e)
        {
            GeneratePDFReceipt(label4.Text);
        }


        private void GeneratePDFContent(object sender, PrintPageEventArgs e)
        {
            using (System.Drawing.Font font = new System.Drawing.Font("Arial", 12))
            {
                int startX = 10;
                int startY = 10;
                int offsetY = 20;

                // Drawing header
                e.Graphics.DrawString("GRD SHOP", font, Brushes.Black, startX + 200, startY);

                // Drawing date
                e.Graphics.DrawString("Date: " + DateTime.Now.ToString("dd/MM/yyyy"), font, Brushes.Black, startX, startY + offsetY);

                // Drawing user name
                e.Graphics.DrawString("User Name: " + textBox1.Text, font, Brushes.Black, startX, startY + 2 * offsetY);

                // Drawing column headers
                e.Graphics.DrawString("Name", font, Brushes.Black, startX, startY + 4 * offsetY);
                e.Graphics.DrawString("Amount", font, Brushes.Black, startX + 200, startY + 4 * offsetY);
                e.Graphics.DrawString("Price", font, Brushes.Black, startX + 400, startY + 4 * offsetY);

                // Drawing data from DataGridView
                int tableX = startX;
                int tableY = startY + 6 * offsetY;
                int cellWidth = 200;
                int cellHeight = 20;

                for (int i = 1; i < datagridshoworder.Rows.Count; i++)
                {
                    for (int j = 0; j < datagridshoworder.Columns.Count; j++)
                    {
                        e.Graphics.DrawString(datagridshoworder.Rows[i].Cells[j].Value.ToString(), font, Brushes.Black, tableX + j * cellWidth, tableY + i * cellHeight);
                    }
                }

                // Drawing total
                e.Graphics.DrawString("Total: " + TOTALlabel.Text, font, Brushes.Black, startX, tableY +cellHeight + 3 * offsetY);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
