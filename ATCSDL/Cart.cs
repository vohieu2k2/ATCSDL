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

namespace ATCSDL
{
    public partial class Cart : Form
    {
        public string loginCustomer;

        public int idCart;

        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";

        public Cart(string loginCustomer, int idCart)
        {
            InitializeComponent();
            this.loginCustomer = loginCustomer;
            this.idCart = idCart;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            HomeCustomer home = new HomeCustomer(loginCustomer);
            home.Show();
            // Đóng addProduct (tùy chọn)
            this.Hide();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            //Lấy dữ liệu list productInCarts
            List<ProductInCart> productInCarts = new List<ProductInCart>();
            productInCarts = GetProductInCart();

            DisplayProducts(productInCarts);
        }

        private void DisplayProducts(List<ProductInCart> products)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (ProductInCart product in products)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel productPanel = new Panel();
                productPanel.Width = fLPanel.Width - productPanel.Margin.Left - productPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                productPanel.Height = 120; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                // Tạo Label
                Label closeLabel = new Label();
                closeLabel.Text = "X"; // Thiết lập văn bản cho Label
                closeLabel.ForeColor = Color.Red; // Thiết lập màu cho văn bản
                closeLabel.AutoSize = true; // Cho phép Label tự điều chỉnh kích thước dựa trên văn bản
                closeLabel.Font = new Font(closeLabel.Font.FontFamily, 14, FontStyle.Bold); // Thiết lập font size và font style
                // Thêm sự kiện Click cho Label
                closeLabel.Click += new EventHandler(CloseLabel_Click);


                CheckBox checkBox = new CheckBox();
                checkBox.Text = "";  // Đặt Text thành rỗng
                checkBox.Checked = false;  // Đặt trạng thái Checked
                // Đặt vị trí
                checkBox.Top = (productPanel.Height - checkBox.Height) / 2;
                checkBox.Left = 10;

                // Tạo PictureBox để hiển thị hình ảnh sản phẩm
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = productPanel.Height - 10;
                pictureBox.Height = productPanel.Height - 10;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Hiển thị ảnh theo tỉ lệ
                // Kiểm tra nếu mảng byte của ảnh là mảng byte rỗng
                if (product.image.SequenceEqual(new byte[0]))
                {
                    pictureBox.BackColor = Color.Gray; // Thiết lập màu trắng cho PictureBox
                }
                else
                {
                    // Nếu không phải mảng byte rỗng, hiển thị hình ảnh
                    pictureBox.Image = ByteToImage(product.image);
                }
                pictureBox.Click += new EventHandler(PictureBox_Click);

                // Tạo Label để hiển thị tên sản phẩm
                Label nameLabel = new Label();
                nameLabel.Text = product.Name;
                nameLabel.AutoSize = false;
                nameLabel.Width = 200; // Thiết lập chiều rộng cho Label
                nameLabel.Height = 20; // Thiết lập chiều cao cho Label
                nameLabel.TextAlign = ContentAlignment.MiddleCenter; // Canh giữa nội dung

                // Tạo Label để hiển thị giá sản phẩm
                Label priceLabel = new Label();
                priceLabel.Text = "Giá: " + product.Price.ToString() + "VNĐ"; // Định dạng giá thành tiền tệ
                priceLabel.AutoSize = false;
                priceLabel.Width = 100;
                priceLabel.Height = 20;
                priceLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Tạo Label để hiển thị giá sản phẩm
                Label numberLabel = new Label();
                numberLabel.Text = "Số lượng: " + product.NumberInCart.ToString() + "kg"; // Định dạng giá thành tiền tệ
                numberLabel.AutoSize = false;
                numberLabel.Width = 100;
                numberLabel.Height = 20;
                numberLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Tạo Button
                Button changeQuantityButton = new Button();
                changeQuantityButton.Text = "Thay đổi số lượng"; // Thiết lập văn bản cho Button
                changeQuantityButton.Click += new EventHandler(ChangeQuantityButton_Click); // Thêm sự kiện Click cho Button

                // Đặt vị trí 
                pictureBox.Location = new Point(35, 5);
                nameLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, pictureBox.Location.Y);
                priceLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, nameLabel.Location.Y + nameLabel.Height + 20);
                numberLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, priceLabel.Location.Y + 20);
                // Đặt vị trí của Label ở góc trên bên phải của Panel
                closeLabel.Location = new Point(productPanel.Width - 20, 0);
                changeQuantityButton.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, numberLabel.Location.Y + 20); // Đặt vị trí cho Button

                // Thêm các controls vào Panel
                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(nameLabel);
                productPanel.Controls.Add(priceLabel);
                productPanel.Controls.Add(numberLabel);
                productPanel.Controls.Add(closeLabel);
                productPanel.Controls.Add(checkBox);
                productPanel.Controls.Add(changeQuantityButton);
                productPanel.Margin = new Padding(productPanel.Margin.Left, 5, productPanel.Margin.Right, 5);
                productPanel.BackColor = Color.White;

                // Lưu trữ thông tin Product vào Tag của panel
                productPanel.Tag = product;
                productPanel.Click += new EventHandler(Panel_Click); // Gắn sự kiện Click

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(productPanel);
            }
        }

        private void ChangeQuantityButton_Click(object sender, EventArgs e)
        {
            // Lấy productPanel chứa Button đang được click
            Panel panel = (Panel)((Button)sender).Parent;

            // Lấy thông tin sản phẩm từ Tag của Panel
            ProductInCart product = (ProductInCart)panel.Tag;

            int numberProduct = 0;

            // Cập nhật số lượng mới cho sản phẩm
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Khởi tạo câu truy vấn SELECT
                string query = "SELECT NumberProduct FROM Product WHERE IDProduct = @IDProduct";

                // Tạo đối tượng SqlCommand
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Thay thế tham số trong câu truy vấn
                    cmd.Parameters.AddWithValue("@IDProduct", product.IDProduct);

                    // Thực thi câu truy vấn SELECT
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        numberProduct = Convert.ToInt32(result);
                    }
                }
            }

            // Hiển thị MessageBox để yêu cầu người dùng nhập số
            string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập số lượng mới:(< " + numberProduct.ToString() + " )", "Thay đổi số lượng", product.NumberInCart.ToString());

            int newQuantity;

            // Kiểm tra nếu người dùng không hủy bỏ dialog và giá trị nhập vào là số
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out newQuantity) && newQuantity > 0)
            {
                if (newQuantity <= numberProduct)
                {
                    // Cập nhật số lượng mới cho sản phẩm
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        // Câu lệnh SQL để cập nhật NumberProductInCart
                        string updateQuery = "UPDATE ProductInCart SET NumberProductInCart = @NewNumberInCart WHERE IDCart = @IDCart AND IDProduct = @IDProduct";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@NewNumberInCart", newQuantity);
                            updateCommand.Parameters.AddWithValue("@IDCart", idCart);
                            updateCommand.Parameters.AddWithValue("@IDProduct", product.IDProduct);

                            // Thực thi câu lệnh SQL cập nhật
                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thay đổi thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                //Lấy dữ liệu list productInCarts
                                List<ProductInCart> productInCarts = new List<ProductInCart>();
                                productInCarts = GetProductInCart();

                                DisplayProducts(productInCarts);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Thay đổi thất bại. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số lượng.");
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập một số lượng.");
                return;
            }
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            Panel parentPanel = (sender as Label)?.Parent as Panel;

            ProductInCart product = parentPanel.Tag as ProductInCart;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Khởi tạo câu truy vấn DELETE
                string query = "DELETE FROM ProductInCart WHERE IDProduct = @IDProduct AND IDCart = @IDCart";

                // Tạo đối tượng SqlCommand
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Thay thế tham số trong câu truy vấn
                    cmd.Parameters.AddWithValue("@IDProduct", product.IDProduct);
                    cmd.Parameters.AddWithValue("@IDCart", idCart);

                    // Thực thi câu truy vấn DELETE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa sản phẩm khỏi giỏ hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //Lấy dữ liệu list productInCarts
                        List<ProductInCart> productInCarts = new List<ProductInCart>();
                        productInCarts = GetProductInCart();

                        DisplayProducts(productInCarts);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại. Vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            Panel parentPanel = (sender as PictureBox)?.Parent as Panel;

            ProductInCart product = parentPanel.Tag as ProductInCart;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Product WHERE IDProduct = @IDProduct";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDProduct", product.IDProduct);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product data = new Product(
                            Convert.ToInt32(reader["IDProduct"]),
                            reader["NameProduct"].ToString(),
                            Convert.ToInt32(reader["PriceProduct"]),
                            Convert.ToInt32(reader["NumberProduct"]),
                            ((DateTime)reader["DateProduct"]).ToString("dd-MM-yyyy"),
                            /*(byte[])reader["ImageProduct"]*/ reader["ImageProduct"] != DBNull.Value ?
                            (byte[])reader["ImageProduct"] : new byte[0],
                            reader["DescriptionProduct"].ToString(),
                            reader["AverageScoreProduct"] != DBNull.Value ?
                            Convert.ToDouble(reader["AverageScoreProduct"]) : 0,
                            Convert.ToInt32(reader["IDCategory"]),
                            reader["loginSupplier"].ToString()
                            );

                        BuyProduct buyProduct = new BuyProduct(data, loginCustomer, false);
                        buyProduct.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            ProductInCart product = clickedPanel.Tag as ProductInCart;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Product WHERE IDProduct = @IDProduct";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDProduct", product.IDProduct);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product data = new Product(
                            Convert.ToInt32(reader["IDProduct"]),
                            reader["NameProduct"].ToString(),
                            Convert.ToInt32(reader["PriceProduct"]),
                            Convert.ToInt32(reader["NumberProduct"]),
                            ((DateTime)reader["DateProduct"]).ToString("dd-MM-yyyy"),
                            /*(byte[])reader["ImageProduct"]*/ reader["ImageProduct"] != DBNull.Value ?
                            (byte[])reader["ImageProduct"] : new byte[0],
                            reader["DescriptionProduct"].ToString(),
                            reader["AverageScoreProduct"] != DBNull.Value ?
                            Convert.ToDouble(reader["AverageScoreProduct"]) : 0,
                            Convert.ToInt32(reader["IDCategory"]),
                            reader["loginSupplier"].ToString()
                            );

                        BuyProduct buyProduct = new BuyProduct(data, loginCustomer, false);
                        buyProduct.Show();
                        this.Hide();
                    }
                }
            }
        }

        public List<ProductInCart> GetProductInCart()
        {
            List<ProductInCart> dataList = new List<ProductInCart>();

            List<(int IDProduct, int NumberProductInCart)> productsInCart = new List<(int, int)>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string firstQuery = "SELECT IDProduct, NumberProductInCart FROM ProductInCart WHERE IDCart = @IDCart";
                SqlCommand command = new SqlCommand(firstQuery, connection);
                command.Parameters.AddWithValue("@IDCart", idCart);

                connection.Open();
                SqlDataReader firstReader = command.ExecuteReader();

                while (firstReader.Read())
                {
                    int idProduct = Convert.ToInt32(firstReader["IDProduct"]);
                    int numberProductInCart = Convert.ToInt32(firstReader["NumberProductInCart"]);
                    productsInCart.Add((idProduct, numberProductInCart));
                }

                firstReader.Close();


                foreach ((int IDProduct, int NumberProductInCart) product in productsInCart)
                {
                    string secondQuery = "SELECT * FROM Product WHERE IDProduct = @IDProduct";

                    SqlCommand cmd = new SqlCommand(secondQuery, connection);
                    cmd.Parameters.AddWithValue("@IDProduct", product.IDProduct);

                    using (SqlDataReader secondReader = cmd.ExecuteReader())
                    {
                        while (secondReader.Read())
                        {
                            ProductInCart data = new ProductInCart(
                                product.IDProduct,
                                product.NumberProductInCart,
                                secondReader["NameProduct"].ToString(),
                                Convert.ToInt32(secondReader["PriceProduct"]),
                                /*(byte[])reader["ImageProduct"]*/ secondReader["ImageProduct"] != DBNull.Value ?
                                (byte[])secondReader["ImageProduct"] : new byte[0],
                                secondReader["loginSupplier"].ToString()
                                );

                            dataList.Add(data);
                        }

                        secondReader.Close();
                    }
                }

            }

            return dataList;
        }

        private Image ByteToImage(byte[] byteArray)
        {
            using (MemoryStream m = new MemoryStream(byteArray))
            {
                return Image.FromStream(m);
            }
        }

        private void buyBtn_Click(object sender, EventArgs e)
        {
            // Tạo danh sách để lưu trữ tất cả các Panel có CheckBox được tích
            List<ProductInCart> products = new List<ProductInCart>();

            // Lặp qua tất cả các Control trong form
            foreach (Control control in fLPanel.Controls)
            {
                // Kiểm tra nếu control là Panel
                if (control is Panel)
                {
                    Panel panel = control as Panel;

                    // Kiểm tra tất cả các Control bên trong Panel
                    foreach (Control innerControl in panel.Controls)
                    {
                        // Kiểm tra nếu control là CheckBox
                        if (innerControl is CheckBox)
                        {
                            CheckBox checkBox = innerControl as CheckBox;
                            // Kiểm tra nếu CheckBox được tích chọn
                            if (checkBox.Checked)
                            {
                                ProductInCart product = panel.Tag as ProductInCart;
                                products.Add(product);
                                break; // Thoát khỏi vòng lặp nội để kiểm tra Panel tiếp theo
                            }
                        }
                    }
                }
            }

            if (products.Count > 0)
            {
                PayBill paybill = new PayBill(2, loginCustomer, products, idCart);
                paybill.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        } 
    }
}
