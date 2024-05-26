using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATCSDL
{
    public partial class PayBill : Form
    {
        public int page;

        public string loginCustomer;

        public Product product;

        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";

        public List<ProductInCart> products = new List<ProductInCart>();

        public int order = 0;

        public int idCart = 0;

        public int transportMoney = 0;

        public int productMoney = 0;

        public List<string> loginSuppliers = new List<string>();

        public PayBill(int page, string loginCustomer, Product product, int order)
        {
            InitializeComponent();
            this.page = page;
            this.loginCustomer = loginCustomer;
            this.product = product;
            this.order = order;
        }

        public PayBill(int page, string loginCustomer, List<ProductInCart> products, int idCart)
        {
            InitializeComponent();
            this.page = page;
            this.loginCustomer = loginCustomer;
            this.products = products;
            this.idCart = idCart;
        }

        private void DirectBuy_Load(object sender, EventArgs e)
        {
            LoadPaymentsIntoComboBox(paymentCb);
            // Tạo câu truy vấn SQL
            string query = "SELECT NameCustomer, AddressCustomer, TelephoneCustomer FROM Customer WHERE LoginCustomer = @LoginCustomer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số vào câu truy vấn để tránh SQL injection
                    command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lấy dữ liệu từ mỗi cột
                            nameCustomerTxt.Text = reader["NameCustomer"].ToString();
                            addressTxt.Text = reader["AddressCustomer"].ToString();
                            phoneTxt.Text = reader["TelephoneCustomer"].ToString();
                        }
                    }
                }
            }

            if (page == 1)
            {
                fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel
                products.Add(new ProductInCart(product.ID, order, product.Name, product.Price * order, product.image, product.loginSupplier));
                productMoney = product.Price *order;
                backBtn.Text = "Xem sản phẩm";
            }
            else if (page == 2)
            {
                fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel
                foreach (var product in products)
                {
                    // Cập nhật giá mới cho sản phẩm
                    product.Price = product.Price * product.NumberInCart;
                    productMoney += product.Price;
                }
                backBtn.Text = "Về giỏ hàng";
            }

            // Tạo danh sách không trùng lặp của nhà cung cấp sản phẩm từ danh sách sản phẩm
            loginSuppliers = products
                .Select(product => product.loginSupplier)
                .Distinct()
                .ToList();

            CalculateMoney();
            DisplayProducts(products);
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

                // Đặt vị trí 
                pictureBox.Location = new Point(35, 5);
                nameLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, pictureBox.Location.Y);
                priceLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, nameLabel.Location.Y + nameLabel.Height + 20);
                numberLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, priceLabel.Location.Y + 20);

                // Thêm các controls vào Panel
                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(nameLabel);
                productPanel.Controls.Add(priceLabel);
                productPanel.Controls.Add(numberLabel);
                productPanel.Margin = new Padding(productPanel.Margin.Left, 5, productPanel.Margin.Right, 5);
                productPanel.BackColor = Color.White;

                // Lưu trữ thông tin Product vào Tag của panel
                productPanel.Tag = product;

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(productPanel);
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
            if(page == 1)
            {
                BuyProduct buyProduct = new BuyProduct(product, loginCustomer);
                buyProduct.Show();
                this.Hide();
            } else if(page == 2) {
                Cart cart = new Cart(loginCustomer, idCart);
                cart.Show();
                this.Hide();
            }
        }

        private void addCartBtn_Click(object sender, EventArgs e)
        {
            foreach (var loginSupplier in loginSuppliers)
            {
                int idOrder = 0;
                //thêm dữ liệu vào bảng Order
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [Order] (PriceOrder, DateOrder, AddressOrder, LoginCustomer, IDPayment, IDTransportation, StatusOrder, LoginSupplier)
                         OUTPUT INSERTED.IDOrder
                         VALUES (@PriceOrder, @DateOrder, @AddressOrder, @LoginCustomer, @IDPayment, @IDTransportation, @StatusOrder, @LoginSupplier)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        cmd.Parameters.AddWithValue("@PriceOrder", Convert.ToInt32(sumLabel.Text.Replace("VNĐ", "").Trim()));
                        cmd.Parameters.AddWithValue("@DateOrder", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@AddressOrder", addressTxt.Text);
                        cmd.Parameters.AddWithValue("@LoginCustomer", loginCustomer);
                        cmd.Parameters.AddWithValue("@IDPayment", paymentCb.SelectedValue);

                        // Kiểm tra và gán giá trị cho @IDTransportation
                        if (standDeliRadio.Checked)
                        {
                            if (addressTxt.Text.Contains("Hồ Chí Minh") || addressTxt.Text.Contains("Hà Nội"))
                            {
                                cmd.Parameters.AddWithValue("@IDTransportation", 1);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IDTransportation", 3);

                            }
                        }
                        else if (fastDeliRadio.Checked)
                        {
                            if (addressTxt.Text.Contains("Hồ Chí Minh") || addressTxt.Text.Contains("Hà Nội"))
                            {
                                cmd.Parameters.AddWithValue("@IDTransportation", 2);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IDTransportation", 4);

                            }
                        }
                        cmd.Parameters.AddWithValue("@StatusOrder", "Chờ xác nhận");
                        cmd.Parameters.AddWithValue("@LoginSupplier", loginSupplier);

                        idOrder = (int)cmd.ExecuteScalar();
                        if (idOrder == 0)
                        {
                            MessageBox.Show("Lỗi! Mua hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // thêm dữ liệu vào bảng ProductInOrder
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach(ProductInCart product in products)
                    {
                        if (product.loginSupplier == loginSupplier)
                        {
                            string query = @"INSERT INTO ProductInOrder (IDProduct, IDOrder, NumberProductOrder, PriceProductOrder)
                                            VALUES (@IDProduct, @IDOrder, @NumberProductOrder, @PriceProductOrder)";

                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDProduct", product.IDProduct);
                                cmd.Parameters.AddWithValue("@IDOrder", idOrder);
                                cmd.Parameters.AddWithValue("@NumberProductOrder", product.NumberInCart);
                                cmd.Parameters.AddWithValue("@PriceProductOrder", product.Price);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected <= 0)
                                {
                                    MessageBox.Show("Đặt hàng thất bại. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }


                        }
                            
                    }

                }
            }

            // Sửa lại giá trị NumberProduct trong Product
            foreach(ProductInCart productInCart in products)
            {
                // Câu lệnh SQL UPDATE
                string sqlQuery = "UPDATE Product SET NumberProduct = NumberProduct - @ValueToSubtract WHERE IDProduct = @ProductID";

                // Tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo đối tượng SqlCommand
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    // Thêm các tham số cho câu lệnh SQL
                    command.Parameters.AddWithValue("@ValueToSubtract", productInCart.NumberInCart);
                    command.Parameters.AddWithValue("@ProductID", productInCart.IDProduct);

                    // Mở kết nối
                    connection.Open();

                    // Thực thi câu lệnh SQL
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected <= 0)
                    {
                        MessageBox.Show("Đặt hàng thất bại. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    connection.Close();

                }
            }

            if(page == 2)
            {
                foreach(ProductInCart product in products)
                {
                    // Khởi tạo kết nối SQL
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Tạo câu lệnh SQL DELETE với điều kiện WHERE phù hợp
                        string sqlDelete = "DELETE FROM ProductInCart WHERE IDProduct = @IDProduct AND IDCart = @IDCart";

                        // Khởi tạo và thiết lập tham số cho câu lệnh SQL
                        using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                        {
                            // Đặt giá trị cho tham số IDProduct và IDCart
                            command.Parameters.AddWithValue("@IDProduct", product.IDProduct);
                            command.Parameters.AddWithValue("@IDCart", idCart);

                            // Thực thi câu lệnh Delete
                            command.ExecuteNonQuery();
                        }

                        // Đóng kết nối
                        connection.Close();
                    }
                }

                // Khởi tạo kết nối SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu lệnh SQL UPDATE với điều kiện WHERE phù hợp
                    string sqlUpdate = "UPDATE Cart SET DateLastUpdate = @DateLastUpdate WHERE IdCart = @IdCart";

                    // Khởi tạo và thiết lập tham số cho câu lệnh SQL
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        // Đặt giá trị cho tham số LastUpdateDate và IdCart
                        command.Parameters.AddWithValue("@DateLastUpdate", DateTime.Now.Date); // Thời điểm hiện tại
                        command.Parameters.AddWithValue("@IdCart", idCart);

                        // Thực thi câu lệnh UPDATE
                        command.ExecuteNonQuery();
                    }

                    // Đóng kết nối
                    connection.Close();
                }
            }

            MessageBox.Show("Đặt hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HomeCustomer home = new HomeCustomer(loginCustomer);
            home.Show();
            this.Hide();

        }
        private void CalculateMoney()
        {
            if(standDeliRadio.Checked)
            {
                if (addressTxt.Text.Contains("Hồ Chí Minh") || addressTxt.Text.Contains("Hà Nội"))
                {
                    transportMoney = 15000;
                }
                else
                {
                    transportMoney = 35000;
                }
            } else
            {
                if (addressTxt.Text.Contains("Hồ Chí Minh") || addressTxt.Text.Contains("Hà Nội"))
                {
                    transportMoney = 35000;
                }
                else
                {
                    transportMoney = 50000;
                }
            }

            transportLabel.Text = (transportMoney * loginSuppliers.Count).ToString() + "VNĐ";
            sumLabel.Text = (productMoney + transportMoney * loginSuppliers.Count).ToString() + "VNĐ";
        }

        private void standDeliRadio_CheckedChanged(object sender, EventArgs e)
        {
            CalculateMoney();
        }

        private void addressTxt_Leave(object sender, EventArgs e)
        {
            CalculateMoney();
        }

        private void LoadPaymentsIntoComboBox(System.Windows.Forms.ComboBox comboBox)
        {

            // Câu lệnh SQL để lấy dữ liệu từ bảng Category
            string query = "SELECT IDPayment, TypePayment FROM Payment";

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
                comboBox.DisplayMember = "TypePayment";
                comboBox.ValueMember = "IDPayment";
            }
        }
    }
}
