using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATCSDL
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";

        private void btnRegister_Click(object sender, EventArgs e)
        {
            String account = textBox1.Text;
            String passWord = textBox2.Text;
            String passWordcheck = textBox3.Text;
            String typeAccount = "";
            if (supplierRadio.Checked)
            {
                typeAccount = "Supplier";
            }
            else if(customerRadio.Checked)
            {
                typeAccount = "Customer";
            }
            String name = nameTxt.Text;
            String address = addressTxt.Text;
            String phone = phoneTxt.Text;
            String email = emailTxt.Text;

            // Kiểm tra xem text1, text2 và text3 đã được nhập hay chưa
            if (string.IsNullOrWhiteSpace(account) || 
                string.IsNullOrWhiteSpace(passWord) || 
                string.IsNullOrWhiteSpace(passWordcheck) || 
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng kiểm tra nếu có ít nhất một trường trống
            }

            // Kiểm tra xem text2 và text3 có giống nhau không
            if (passWord != passWordcheck)
            {
                MessageBox.Show("Hai mật khẩu không giống nhau. VUi lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                
                // Thực hiện truy vấn SQL để kiểm tra giá trị
                string query = "SELECT COUNT(*) FROM Supplier WHERE LoginSupplier = @Account";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thay đổi "@Account" thành giá trị nhập từ TextBox
                    command.Parameters.AddWithValue("@Account", account);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Tài khoản đã tồn tại trong cơ sở dữ liệu
                        MessageBox.Show("Tài khoản đã tồn tại. Vui lòng đổi tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                query = "SELECT COUNT(*) FROM Customer WHERE LoginCustomer = @Account";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thay đổi "@Account" thành giá trị nhập từ TextBox
                    command.Parameters.AddWithValue("@Account", account);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Tài khoản đã tồn tại trong cơ sở dữ liệu
                        MessageBox.Show("Tài khoản đã tồn tại. Vui lòng đổi tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (IsStrongPassword(passWord))
            {
                passWord = HashPassword(passWord);
                // Thiết lập kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Mở kết nối

                    // Chuỗi truy vấn SQL để chèn dữ liệu vào bảng
                    string insertQuery = "";

                    if (typeAccount.Equals("Supplier"))
                    {
                        insertQuery = "INSERT INTO Supplier (LoginSupplier, PassSupplier, NameSupplier, " +
                            "AddressSupplier, TelephoneSupplier, EmailSupplier) " +
                            "VALUES (@Account, @Password, @Name, @Address, @Phone, @Email)";
                    } else if (typeAccount.Equals("Customer"))
                    {
                        insertQuery = "INSERT INTO Customer (LoginCustomer, PassCustomer, NameCustomer, " +
                            "AddressCustomer, TelephoneCustomer, EmailCustomer) " +
                            "VALUES (@Account, @Password, @Name, @Address, @Phone, @Email)";
                    }

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Thay thế các tham số với giá trị thực tế
                        command.Parameters.AddWithValue("@Account", account);
                        command.Parameters.AddWithValue("@Password", passWord);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Email", email);

                        // Thực hiện truy vấn SQL để chèn dữ liệu
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            if (typeAccount.Equals("Customer"))
                            {
                                // Lấy thời gian hiện tại
                                DateTime lastUpdate = DateTime.Now.Date;

                                // Câu truy vấn SQL để thêm dữ liệu
                                string query = "INSERT INTO Cart (DateLastUpdate, LoginCustomer) VALUES (@LastUpdate, @LoginCustomer)";

                                using (SqlCommand cmd = new SqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@LastUpdate", lastUpdate);
                                    cmd.Parameters.AddWithValue("@LoginCustomer", account); // loginCustomer là biến string chứa LoginCustomer

                                    int row = cmd.ExecuteNonQuery();

                                    if (row <= 0)
                                    {
                                        MessageBox.Show("Lỗi! Đăng kí thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            // Ghi dữ liệu thành công
                            MessageBox.Show("Đăng kí thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Tạo một instance của Form1
                            Login form1 = new Login();

                            // Hiển thị Form1
                            form1.Show();

                            // Đóng Form2 (tùy chọn)
                            this.Hide();
                        }
                        else
                        {
                            // Ghi dữ liệu không thành công
                            MessageBox.Show("Lỗi! Đăng kí thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem Text1 chứa số hay không
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                // Nếu chứa số, cập nhật giá trị của Label
                labelNotice.Text = "Mật khẩu mạnh ít nhất có 8 kí tự bao gồm chữ\r\nsố, chữ cái (hoa và thường)và kí tự đặc biệt";
                labelNotice.ForeColor = Color.Black; // Đổi màu chữ thành màu đen
            }
            else
            {
                PasswordStrength strength = CheckPasswordStrength(textBox2.Text);

                switch (strength)
                {
                    case PasswordStrength.VeryWeak:
                        labelNotice.Text = "Mật khẩu rất yếu.";
                        labelNotice.ForeColor = Color.Red;
                        break;
                    case PasswordStrength.Weak:
                        labelNotice.Text = "Mật khẩu yếu.";
                        labelNotice.ForeColor = Color.Red;
                        break;
                    case PasswordStrength.Medium:
                        labelNotice.Text = "Mật khẩu trung bình.";
                        labelNotice.ForeColor = Color.DarkGoldenrod;
                        break;
                    case PasswordStrength.Strong:
                        labelNotice.Text = "Mật khẩu mạnh.";
                        labelNotice.ForeColor = Color.Green;
                        break;
                }
            }
        }

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

        enum PasswordStrength
        {
            VeryWeak,
            Weak,
            Medium,
            Strong
        }

        static PasswordStrength CheckPasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                return PasswordStrength.VeryWeak;
            }

            int score = 0;

            if (ContainsUpperCaseLetter(password))
            {
                score++;
            }

            if (ContainsLowerCaseLetter(password))
            {
                score++;
            }

            if (ContainsDigit(password))
            {
                score++;
            }

            if (ContainsSpecialCharacter(password))
            {
                score++;
            }

            if (score < 2)
            {
                return PasswordStrength.Weak;
            }
            else if (score < 4)
            {
                return PasswordStrength.Medium;
            }
            else
            {
                return PasswordStrength.Strong;
            }
        }

        static bool ContainsUpperCaseLetter(string password)
        {
            return password.Any(char.IsUpper);
        }

        static bool ContainsLowerCaseLetter(string password)
        {
            return password.Any(char.IsLower);
        }

        static bool ContainsDigit(string password)
        {
            return password.Any(char.IsDigit);
        }

        static bool ContainsSpecialCharacter(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        static bool IsStrongPassword(string password)
        {
            // Kiểm tra độ dài mật khẩu ít nhất 8 ký tự
            if (password.Length < 8)
            {
                MessageBox.Show("Mật khẩu cần ít nhất 8 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Kiểm tra có ít nhất một chữ cái thường
            if (!password.Any(char.IsLower))
            {
                MessageBox.Show("Mật khẩu cần chứa ít nhất một chữ cái thường.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Kiểm tra có ít nhất một chữ cái in hoa
            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Mật khẩu cần chứa ít nhất một chữ cái hoa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Kiểm tra có ít nhất một chữ số
            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Mật khẩu cần chứa ít nhất một số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Kiểm tra có ít nhất một ký tự đặc biệt (dùng biểu thức chính quy)
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show("Mật khẩu cần chứa ít nhất một ký tự đặc biệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Tạo một instance của Form1
            Login form1 = new Login();

            // Hiển thị Form1
            form1.Show();

            // Đóng Form2 (tùy chọn)
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem CheckBox có được chọn (tích) hay không
            if (checkBox1.Checked)
            {
                // Nếu được tích, hiển thị mật khẩu bằng cách xóa thuộc tính PasswordChar
                textBox2.PasswordChar = '\0'; // '\0' là ký tự mặc định, không ẩn mật khẩu
                textBox3.PasswordChar = '\0'; // '\0' là ký tự mặc định, không ẩn mật khẩu
            }
            else
            {
                // Nếu không được tích, ẩn mật khẩu bằng cách đặt PasswordChar thành '*' hoặc ký tự ẩn khác
                textBox2.PasswordChar = '*'; // Hoặc bạn có thể sử dụng ký tự khác để ẩn mật khẩu
                textBox3.PasswordChar = '*'; // Hoặc bạn có thể sử dụng ký tự khác để ẩn mật khẩu
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void phoneTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là phím điều khiển (như Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này vào TextBox
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 20)
            {
                textBox1.Text = textBox1.Text.Substring(0, 20);
                textBox1.SelectionStart = 20;
                textBox1.SelectionLength = 0;
            }
        }

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {
            if (nameTxt.Text.Length > 50)
            {
                nameTxt.Text = nameTxt.Text.Substring(0, 50);
                nameTxt.SelectionStart = 50;
                nameTxt.SelectionLength = 0;
            }
        }

        private void addressTxt_TextChanged(object sender, EventArgs e)
        {
            if (addressTxt.Text.Length > 100)
            {
                addressTxt.Text = addressTxt.Text.Substring(0, 100);
                addressTxt.SelectionStart = 100;
                addressTxt.SelectionLength = 0;
            }
        }

        private void phoneTxt_TextChanged(object sender, EventArgs e)
        {
            if (phoneTxt.Text.Length > 10)
            {
                phoneTxt.Text = phoneTxt.Text.Substring(0, 10);
                phoneTxt.SelectionStart = 10;
                phoneTxt.SelectionLength = 0;
            }
        }

        private void emailTxt_TextChanged(object sender, EventArgs e)
        {
            if (emailTxt.Text.Length > 50)
            {
                emailTxt.Text = emailTxt.Text.Substring(0, 50);
                emailTxt.SelectionStart = 50;
                emailTxt.SelectionLength = 0;
            }
        }
    }
}
