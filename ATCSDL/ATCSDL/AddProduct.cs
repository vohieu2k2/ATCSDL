using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ATCSDL
{
    public partial class AddProduct : Form
    {
        public string loginSupplier;


        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";

        public AddProduct(string login)
        {
            InitializeComponent();
            loginSupplier = login;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string name = nameTxt.Text;
            string price = priceTxt.Text;
            string number = numberTxt.Text;
            int category = 0;
            string descrip = descripTxt.Text;
            if (plantRadio.Checked)
            {
                category = 1;
            }
            else if (animalRadio.Checked)
            {
                category = 2;
            }

            // Kiểm tra xem text1, text2 và text3 đã được nhập hay chưa
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(price) ||
                string.IsNullOrWhiteSpace(number) ||
                category == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng kiểm tra nếu có ít nhất một trường trống
            }

            // Khởi tạo biến cho dữ liệu ảnh
            byte[] imageData = ImageToByte(pictureBox.Image);

            // Tạo đối tượng SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Chuỗi truy vấn SQL để chèn dữ liệu vào bảng
                string insertQuery = "INSERT INTO Product(NameProduct, PriceProduct, NumberProduct, " +
                        "DateProduct, ImageProduct, DescriptionProduct, IDCategory, LoginSupplier) " +
                        "VALUES (@Name, @Price, @Number, @Date, @Image, @Description, @IDCategory, @LoginSupplier)"; ;

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Thay thế các tham số với giá trị thực tế
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", Convert.ToInt32(price));
                    command.Parameters.AddWithValue("@Number", Convert.ToInt32(number));
                    command.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                    // Kiểm tra và thêm dữ liệu ảnh hoặc NULL
                    if (imageData != null)
                    {
                        command.Parameters.AddWithValue("@Image", imageData);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Image", new byte[0]);
                    }
                    command.Parameters.AddWithValue("@Description", descrip);
                    command.Parameters.AddWithValue("@IDCategory", category);
                    command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);

                    // Thực hiện truy vấn SQL để chèn dữ liệu
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Ghi dữ liệu thành công
                        MessageBox.Show("Đăng tải sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        HomeSupplier homeSupplier = new HomeSupplier(loginSupplier);
                        homeSupplier.Show();
                        // Đóng addProduct (tùy chọn)
                        this.Hide();
                    }
                    else
                    {
                        // Ghi dữ liệu không thành công
                        MessageBox.Show("Lỗi! Đăng tải sản phẩm thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private byte[] ImageToByte(Image image)
        {
            if (image == null)
            {
                return null;
            }

            using (MemoryStream m = new MemoryStream())
            {
                try
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, ImageFormat.Png);
                        return memoryStream.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chuyển đổi ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        private void imageBtn_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập các thuộc tính cho OpenFileDialog
            openFileDialog.Title = "Chọn ảnh"; // Tiêu đề của hộp thoại
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif"; // Bộ lọc các định dạng ảnh
            openFileDialog.Multiselect = false; // Cho phép chọn nhiều tệp tin (ở đây là chỉ chọn một tệp)
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // Thư mục mặc định để mở

            // Mở hộp thoại và kiểm tra nếu người dùng đã chọn ảnh
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của tệp ảnh đã chọn
                string imagePath = openFileDialog.FileName;

                // Hiển thị ảnh trong PictureBox
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Image = Image.FromFile(imagePath);
            }
        }

        private void priceTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là phím điều khiển (như Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này vào TextBox
            }
        }
        private void numberTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là phím điều khiển (như Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này vào TextBox
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            HomeSupplier homeSupplier = new HomeSupplier(loginSupplier);
            homeSupplier.Show();
            // Đóng addProduct (tùy chọn)
            this.Hide();
        }
    }
}
