using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATCSDL
{
    public partial class EditProduct : Form
    {
        public Product product;

        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";

        public EditProduct(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private void EditProduct_Load(object sender, EventArgs e)
        {
            LoadCategoriesIntoComboBox(categoryCb);
            nameTxt.Text = product.Name;
            priceTxt.Text = product.Price.ToString();
            categoryCb.SelectedValue = product.category;
            numberTxt.Text = product.number.ToString();
            descripTxt.Text = product.description.ToString();
            if (product.image.SequenceEqual(new byte[0]))
            {
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.BackColor = Color.Gray; // Thiết lập màu trắng cho PictureBox
            }
            else
            {
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                // Nếu không phải mảng byte rỗng, hiển thị hình ảnh
                pictureBox.Image = ByteToImage(product.image);
            }
        }

        private Image ByteToImage(byte[] byteArray)
        {
            using (MemoryStream m = new MemoryStream(byteArray))
            {
                return Image.FromStream(m);
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            HomeSupplier homeSupplier = new HomeSupplier(product.loginSupplier);
            homeSupplier.Show();
            // Đóng addProduct (tùy chọn)
            this.Hide();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem text1, text2 và text3 đã được nhập hay chưa
            if (string.IsNullOrWhiteSpace(nameTxt.Text) ||
                string.IsNullOrWhiteSpace(priceTxt.Text) ||
                string.IsNullOrWhiteSpace(numberTxt.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng kiểm tra nếu có ít nhất một trường trống
            }

            // Khởi tạo biến cho dữ liệu ảnh
            byte[] imageData = ImageToByte(pictureBox.Image);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                string query = "UPDATE Product SET NameProduct = @Name, PriceProduct = @Price, NumberProduct = @Number, " +
                    "ImageProduct = @Image, DescriptionProduct = @Description, IDCategory = @Category WHERE IDProduct = @ID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    // Thiết lập các tham số
                    command.Parameters.AddWithValue("@ID", product.ID);
                    command.Parameters.AddWithValue("@Name", nameTxt.Text);
                    command.Parameters.AddWithValue("@Price", Convert.ToInt32(priceTxt.Text)); // Giá trị Price mới
                    command.Parameters.AddWithValue("@Number", Convert.ToInt32(numberTxt.Text));   // Số lượng mới
                    command.Parameters.AddWithValue("@Description", descripTxt.Text);
                    // Kiểm tra và thêm dữ liệu ảnh hoặc NULL
                    if (imageData != null)
                    {
                        command.Parameters.AddWithValue("@Image", imageData);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Image", new byte[0]);
                    }

                    if (categoryCb.SelectedItem != null)
                    {
                        command.Parameters.AddWithValue("@Category", (int)categoryCb.SelectedValue);
                    }

                    // Thực hiện truy vấn SQL để chèn dữ liệu
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Ghi dữ liệu thành công
                        MessageBox.Show("Sửa sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        HomeSupplier homeSupplier = new HomeSupplier(product.loginSupplier);
                        homeSupplier.Show();
                        // Đóng addProduct (tùy chọn)
                        this.Hide();
                    }
                    else
                    {
                        // Ghi dữ liệu không thành công
                        MessageBox.Show("Lỗi! Sửa sản phẩm thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            
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
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
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

        private void deleteBtn_Click(object sender, EventArgs e)
        {

            deleteProductInOrder();
            deleteProductInCart();
            deleteProduct();
        }

        private void deleteProduct()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                string query = "DELETE FROM Product WHERE IDProduct = @ID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thiết lập tham số ID
                    command.Parameters.AddWithValue("@ID", product.ID);

                    // Thực hiện truy vấn SQL để xóa dữ liệu
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Xóa dữ liệu thành công
                        MessageBox.Show("Xóa sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        HomeSupplier homeSupplier = new HomeSupplier(product.loginSupplier);
                        homeSupplier.Show();
                        // Đóng form hiện tại
                        this.Hide();
                    }
                    else
                    {
                        // Xóa dữ liệu không thành công
                        MessageBox.Show("Lỗi! Xóa sản phẩm thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void deleteProductInCart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                string query = "DELETE FROM ProductInCart WHERE IDProduct = @ID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thiết lập tham số ID
                    command.Parameters.AddWithValue("@ID", product.ID);

                    // Thực hiện truy vấn SQL để xóa dữ liệu
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void deleteProductInOrder()
        {
            // Lấy các giá trị idOrder chứa idProduct  
            List<int> idOrders = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo truy vấn SQL để lấy giá trị IDOrder từ bảng ProductInOrder khi có IDProduct trùng với giá trị cho trước
                string query = "SELECT IDOrder FROM ProductInOrder WHERE IDProduct = @IDProduct";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thiết lập tham số IDProduct
                    command.Parameters.AddWithValue("@IDProduct", product.ID); // productId là giá trị IDProduct cho trước

                    // Sử dụng ExecuteReader để thực hiện truy vấn SQL và đọc kết quả
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Kiểm tra xem có kết quả nào hay không
                        if (reader.HasRows)
                        {
                            // Đọc kết quả
                            while (reader.Read())
                            {
                                int orderId = Convert.ToInt32(reader["IDOrder"]);
                                // Xử lý giá trị orderId ở đây
                                idOrders.Add(orderId);
                            }
                        }
                    }
                }
            }

            if(idOrders.Count > 0)
            {
                // Tiến hành xóa Product trong ProductInOrder
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo truy vấn SQL DELETE để xóa các hàng có IDProduct và IDOrder trùng với giá trị cho trước
                    string query = "DELETE FROM ProductInOrder WHERE IDProduct = @IDProduct";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thiết lập tham số IDProduct và IDOrder
                        command.Parameters.AddWithValue("@IDProduct", product.ID); // productId là giá trị IDProduct cho trước

                        // Thực hiện truy vấn SQL để xóa hàng
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }

                // Lấy giá trị các idOrder khi xóa IDProduct thì ko còn chứa sản phẩm nào khác
                List<int> deleteIDOrder = new List<int>();
                foreach (int idOrder in idOrders)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Tạo truy vấn SQL để đếm số lượng hàng có IDOrder bằng giá trị cho trước
                        string query = "SELECT COUNT(*) FROM ProductInOrder WHERE IDOrder = @IDOrder";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Thiết lập tham số IDOrder
                            command.Parameters.AddWithValue("@IDOrder", idOrder); // orderId là giá trị IDOrder cho trước

                            // Sử dụng ExecuteScalar để lấy số lượng hàng phù hợp
                            int rowCount = (int)command.ExecuteScalar();

                            if(rowCount == 0)
                            {
                                deleteIDOrder.Add(idOrder);
                            }
                        }
                    }
                }

                // Tiến hành xóa các hàng đã lấy ở trên
                foreach(int idOrder in deleteIDOrder)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Tạo truy vấn SQL DELETE để xóa các hàng có IDOrder trùng với giá trị cho trước
                        string query = "DELETE FROM [Order] WHERE IDOrder = @IDOrder";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Thiết lập tham số IDOrder
                            command.Parameters.AddWithValue("@IDOrder", idOrder); // orderId là giá trị IDOrder cho trước

                            // Thực hiện truy vấn SQL để xóa hàng
                            int rowsAffected = command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadCategoriesIntoComboBox(System.Windows.Forms.ComboBox comboBox)
        {

            // Câu lệnh SQL để lấy dữ liệu từ bảng Category
            string query = "SELECT IDCategory, NameCategory FROM Category";

            // Sử dụng SqlConnection và SqlDataAdapter để lấy dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                // Mở kết nối và lấy dữ liệu
                connection.Open();
                dataAdapter.Fill(dataTable);

                // Thiết lập DataSource, DisplayMember và ValueMember cho ComboBox
                comboBox.DataSource = dataTable;
                comboBox.DisplayMember = "NameCategory";
                comboBox.ValueMember = "IDCategory";
            }
        }
        
        private void descripTxt_TextChanged(object sender, EventArgs e)
        {
            if (descripTxt.Text.Length > 250)
            {
                descripTxt.Text = descripTxt.Text.Substring(0, 250);
                descripTxt.SelectionStart = 250;
                descripTxt.SelectionLength = 0;
            }
        }
    }
}
