using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATCSDL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool typeSupplier = true;
            // Giá trị từ TextBox
            string inputAccount = textBox1.Text;
            string inputPassword = textBox2.Text;
            // Kiểm tra xem text1, text2 và text3 đã được nhập hay chưa
            if (string.IsNullOrWhiteSpace(inputAccount) || string.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho tài khoản và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng kiểm tra nếu có ít nhất một trường trống
            }

            inputPassword = HashPassword(inputPassword);

            string query = "";
            if(supplierRadio.Checked)
            {

                query = "SELECT * FROM Supplier WHERE LoginSupplier = @Account AND PassSupplier = @Password";
            }
            else if(customerRadio.Checked)
            {
                typeSupplier = false;
                query = "SELECT * FROM Customer WHERE LoginCustomer = @Account AND PassCustomer = @Password";
            }
            // Truy vấn SQL để kiểm tra thông tin đăng nhập

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Mở kết nối

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Account", inputAccount);
                    command.Parameters.AddWithValue("@Password", inputPassword);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Đăng nhập thành công
                        if (typeSupplier)
                        {
                            HomeSupplier home = new HomeSupplier(inputAccount);
                            home.Show();
                            this.Hide();
                        }
                        else
                        {
                            HomeCustomer home = new HomeCustomer(inputAccount);
                            home.Show();
                            this.Hide();
                        }
                        LoginAttemptsCounter.LoginAttempts = 0;
                    }
                    else
                    {
                        LoginAttemptsCounter.LoginAttempts++;
                        // Đăng nhập thất bại
                        if (LoginAttemptsCounter.LoginAttempts >= 5)
                        {
                            MessageBox.Show("Bạn đã đăng nhập sai quá 5 lần. Nút đăng nhập bị khóa.");
                            BtnLogin.HiddenBtn = false; // Vô hiệu hóa nút đăng nhập.
                            btnLogin.Enabled = BtnLogin.HiddenBtn;
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại. Số lần thử: " + LoginAttemptsCounter.LoginAttempts, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        // Hàm để băm mật khẩu
        public string HashPassword(string password)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static class LoginAttemptsCounter
        {
            public static int LoginAttempts { get; set; } = 0;
        }

        public static class BtnLogin
        {
            public static bool HiddenBtn { get; set; } = true;
        }

        // Sự kiện FormClosing cho Form1
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Thoát chương trình nếu người dùng đồng ý
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem CheckBox có được chọn (tích) hay không
            if (checkBox1.Checked)
            {
                // Nếu được tích, hiển thị mật khẩu bằng cách xóa thuộc tính PasswordChar
                textBox2.PasswordChar = '\0'; // '\0' là ký tự mặc định, không ẩn mật khẩu
            }
            else
            {
                // Nếu không được tích, ẩn mật khẩu bằng cách đặt PasswordChar thành '*' hoặc ký tự ẩn khác
                textBox2.PasswordChar = '*'; // Hoặc bạn có thể sử dụng ký tự khác để ẩn mật khẩu
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Tạo một instance của Form2
            Register form2 = new Register();

            form2.WindowState = FormWindowState.Maximized;
            // Hiển thị Form2
            form2.Show();

            // Đóng Form2 (tùy chọn)
            this.Hide();
        }
    }

}
