using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATCSDL
{
    public partial class CustomerBills : Form
    {
        public string loginCustomer;

        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";

        public List<Order> orders = new List<Order>();
        public CustomerBills(string loginCustomer)
        {
            InitializeComponent();
            this.loginCustomer = loginCustomer;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            HomeCustomer homeCustomer = new HomeCustomer(loginCustomer);
            homeCustomer.Show();
            this.Close();
        }

        private void Bills_Load(object sender, EventArgs e)
        {
            orders.Clear();
            orders = GetOrderFromSql("");
            DisplayOrders(orders);
        }
        public List<Order> GetOrderFromSql(string StatusOrder)
        {
            List<Order> datalist = new List<Order>();

            if (StatusOrder.Equals(""))
            {
                // Kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Truy vấn dữ liệu từ bảng Order
                    string query = "SELECT * FROM [Order] WHERE loginCustomer = @LoginCustomer";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho truy vấn để tránh tấn công SQL Injection
                        command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);

                        // Đọc dữ liệu từ truy vấn
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Duyệt qua các dòng dữ liệu
                            while (reader.Read())
                            {
                                datalist.Add(new Order(
                                    Convert.ToInt32(reader["IDOrder"]),
                                    Convert.ToInt32(reader["PriceOrder"]),
                                    ((DateTime)reader["DateOrder"]).ToString("dd-MM-yyyy"),
                                    reader["AddressOrder"].ToString(),
                                    loginCustomer,
                                    Convert.ToInt32(reader["IDPayment"]),
                                    Convert.ToInt32(reader["IDTransportation"]),
                                    reader["StatusOrder"].ToString(),
                                    reader["LoginSupplier"].ToString()
                                ));
                            }
                        }
                    }
                }
            }
            else
            {
                // Kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Truy vấn dữ liệu từ bảng Order
                    string query = "SELECT * FROM [Order] WHERE loginCustomer = @LoginCustomer AND StatusOrder = @StatusOrder";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho truy vấn để tránh tấn công SQL Injection
                        command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);
                        command.Parameters.AddWithValue("@StatusOrder", StatusOrder);

                        // Đọc dữ liệu từ truy vấn
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Duyệt qua các dòng dữ liệu
                            while (reader.Read())
                            {
                                datalist.Add(new Order(
                                    Convert.ToInt32(reader["IDOrder"]),
                                    Convert.ToInt32(reader["PriceOrder"]),
                                    ((DateTime)reader["DateOrder"]).ToString("dd-MM-yyyy"),
                                    reader["AddressOrder"].ToString(),
                                    loginCustomer,
                                    Convert.ToInt32(reader["IDPayment"]),
                                    Convert.ToInt32(reader["IDTransportation"]),
                                    reader["StatusOrder"].ToString(),
                                    reader["LoginSupplier"].ToString()
                                ));
                            }
                        }
                    }
                }
            }

            return datalist;
        }
        private void DisplayOrders(List<Order> orders)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (Order order in orders)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel orderPanel = new Panel();
                orderPanel.Width = fLPanel.Width - orderPanel.Margin.Left - orderPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                orderPanel.Height = 120; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                List<ProductInOrder> productInOrder = new List<ProductInOrder>();
                productInOrder = GetProductInOrderInfoByOrderId(order.ID);
                Product product = new Product();
                product = GetProductFromProductID(productInOrder[0].IDProduct);

                // Tạo PictureBox để hiển thị hình ảnh sản phẩm
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = orderPanel.Height - 40;
                pictureBox.Height = orderPanel.Height - 40;
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
                priceLabel.Text = "Giá: " + productInOrder[0].PriceProductOrder.ToString() + "VNĐ"; // Định dạng giá thành tiền tệ
                priceLabel.AutoSize = false;
                priceLabel.Width = 100;
                priceLabel.Height = 20;
                priceLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Tạo Label để hiển thị giá sản phẩm
                Label numberLabel = new Label();
                numberLabel.Text = "Số lượng: " + productInOrder[0].NumberProductOrder.ToString() + "kg"; // Định dạng giá thành tiền tệ
                numberLabel.AutoSize = false;
                numberLabel.Width = 100;
                numberLabel.Height = 20;
                numberLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Tạo Label để hiển thị số lượng sản phẩm 
                Label numberProductOrder = new Label();
                numberProductOrder.Text = productInOrder.Count.ToString() + " sản phẩm";
                numberProductOrder.AutoSize = false;
                numberProductOrder.Width = 100; // Thiết lập chiều rộng cho Label
                numberProductOrder.Height = 20; // Thiết lập chiều cao cho Label
                numberProductOrder.TextAlign = ContentAlignment.MiddleLeft; // Canh giữa nội dung

                // Tạo Label để hiển thị giá đơn hàng
                Label sumLabel = new Label();
                sumLabel.Text = "Thành tiền: " + order.Price.ToString() + "VNĐ"; // Định dạng giá thành tiền tệ
                sumLabel.AutoSize = false;
                sumLabel.Width = orderPanel.Width - 35 - numberProductOrder.Width;
                sumLabel.Height = 20;
                sumLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Đặt vị trí 
                pictureBox.Location = new Point(35, 5);
                nameLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, pictureBox.Location.Y);
                priceLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, nameLabel.Location.Y + 20);
                numberLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, priceLabel.Location.Y + 20);
                numberProductOrder.Location = new Point(35, pictureBox.Location.Y + pictureBox.Height + 10);
                sumLabel.Location = new Point(numberProductOrder.Location.X + numberProductOrder.Width, pictureBox.Location.Y + pictureBox.Height + 10);

                // Thêm các controls vào Panel
                orderPanel.Controls.Add(pictureBox);
                orderPanel.Controls.Add(nameLabel);
                orderPanel.Controls.Add(priceLabel);
                orderPanel.Controls.Add(numberLabel);
                orderPanel.Controls.Add(sumLabel);
                orderPanel.Controls.Add(numberProductOrder);
                orderPanel.Margin = new Padding(orderPanel.Margin.Left, 5, orderPanel.Margin.Right, 5);
                orderPanel.BackColor = Color.White;
                orderPanel.Click += new EventHandler(Panel_Click); // Gắn sự kiện Click

                // Lưu trữ thông tin list productInOrder vào Tag của panel
                orderPanel.Tag = productInOrder;

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(orderPanel);
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            List<ProductInOrder> productInOrder = clickedPanel.Tag as List<ProductInOrder>;

            string statusOrder = "";
            string loginSupllier = "";

            // Câu lệnh SQL để lấy dữ liệu từ bảng Order
            string query = "SELECT StatusOrder, LoginSupplier FROM [Order] WHERE IDOrder = @IDOrder";

            // Kết nối đến cơ sở dữ liệu và thực hiện truy vấn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu truy vấn để tránh SQL injection
                    command.Parameters.AddWithValue("@IDOrder", productInOrder[0].IDOrder);
                    connection.Open();

                    // Thực hiện câu lệnh và lấy dữ liệu về
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Lấy giá trị từ các cột
                            statusOrder = reader["StatusOrder"].ToString();
                            loginSupllier = reader["LoginSupplier"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Dừng kiểm tra nếu có ít nhất một trường trống
                        }
                    }
                }
            }


            InfoCustomerBill infoBill = new InfoCustomerBill(productInOrder, statusOrder, loginSupllier,loginCustomer, productInOrder[0].IDOrder);
            infoBill.Show();
            this.Hide();
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            Panel parentPanel = (sender as PictureBox)?.Parent as Panel;

            List<ProductInOrder> productInOrder = parentPanel.Tag as List<ProductInOrder>;

            string statusOrder = "";
            string loginSupllier = "";

            // Câu lệnh SQL để lấy dữ liệu từ bảng Order
            string query = "SELECT StatusOrder, LoginSupplier FROM [Order] WHERE IDOrder = @IDOrder";

            // Kết nối đến cơ sở dữ liệu và thực hiện truy vấn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu truy vấn để tránh SQL injection
                    command.Parameters.AddWithValue("@IDOrder", productInOrder[0].IDOrder);
                    connection.Open();

                    // Thực hiện câu lệnh và lấy dữ liệu về
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Lấy giá trị từ các cột
                            statusOrder = reader["StatusOrder"].ToString();
                            loginSupllier = reader["LoginSupplier"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Dừng kiểm tra nếu có ít nhất một trường trống
                        }
                    }
                }
            }

            InfoCustomerBill infoBill = new InfoCustomerBill(productInOrder, statusOrder, loginSupllier, loginCustomer, productInOrder[0].IDOrder);
            infoBill.Show();
            this.Hide();
        }

        private Image ByteToImage(byte[] byteArray)
        {
            using (MemoryStream m = new MemoryStream(byteArray))
            {
                return Image.FromStream(m);
            }
        }
        public List<ProductInOrder> GetProductInOrderInfoByOrderId(int orderId)
        {
            List<ProductInOrder> datalist = new List<ProductInOrder>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IDProductOrder, IDProduct, IDOrder, NumberProductOrder, PriceProductOrder " +
                               "FROM ProductInOrder " +
                               "WHERE IDOrder = @OrderId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho truy vấn để tránh tấn công SQL Injection
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            datalist.Add(new ProductInOrder(
                                // Đọc thông tin từ mỗi dòng và xử lý ở đây
                                Convert.ToInt32(reader["IDProductOrder"]),
                                Convert.ToInt32(reader["IDProduct"]),
                                Convert.ToInt32(reader["IDOrder"]),
                                Convert.ToInt32(reader["NumberProductOrder"]),
                                Convert.ToInt32(reader["PriceProductOrder"])
                            ));
                        }
                    }
                }
            }
            return datalist;
        }
        public Product GetProductFromProductID(int productID)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Product WHERE IDProduct = @IDProduct"; // Sửa truy vấn SQL

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho truy vấn để tránh tấn công SQL Injection
                    command.Parameters.AddWithValue("@IDProduct", productID);

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

                            product = data;
                        }
                    }
                }
            }
            return product;
        }

        private void allBtn_Click(object sender, EventArgs e)
        {
            orders.Clear();
            orders = GetOrderFromSql("");
            DisplayOrders(orders);
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            orders.Clear();
            orders = GetOrderFromSql("Chờ xác nhận");
            DisplayOrders(orders);
        }

        private void waitDeliBtn_Click(object sender, EventArgs e)
        {
            orders.Clear();
            orders = GetOrderFromSql("Đang giao");
            DisplayOrders(orders);
        }

        private void deliveredBtn_Click(object sender, EventArgs e)
        {
            orders.Clear();
            orders = GetOrderFromSql("Đã giao");
            DisplayOrders(orders);
        }

        private void returnBtn_Click(object sender, EventArgs e)
        {
            orders.Clear();
            orders = GetOrderFromSql("Đã hủy");
            DisplayOrders(orders);
        }
    }
}
