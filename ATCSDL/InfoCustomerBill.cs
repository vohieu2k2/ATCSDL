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
    public partial class InfoCustomerBill : Form
    {
        public string loginCustomer, loginSupplier, statusOrder;
        public int idOrder;
        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";
        public List<ProductInOrder> productInOrders = new List<ProductInOrder>();
        public InfoCustomerBill(List<ProductInOrder> productInOrders, string statusOrder, string loginSupplier, string loginCustomer, int idOrder)
        {
            InitializeComponent();
            this.productInOrders = productInOrders;
            this.idOrder = idOrder;
            this.loginCustomer = loginCustomer;
            this.loginSupplier = loginSupplier;
            this.statusOrder = statusOrder;
            if(statusOrder.Equals("Chờ xác nhận"))
            {
                statusLabel.Text = "Chờ xác nhận";
                commitBtn.Text = "Hủy đơn hàng";
            } else if(statusOrder.Equals("Đang giao"))
            {
                statusLabel.Text = "Đang giao";
                commitBtn.Text = "Nhận được hàng";
            } else if(statusOrder.Equals("Đã giao"))
            {
                statusLabel.Text = "Đã giao";
                commitBtn.Text = "Trả hàng";
            } else if(statusOrder.Equals("Đã hủy"))
            {
                statusLabel.Text = "Đã hủy";
                commitBtn.Visible = false;
            }
        }

        private void commitBtn_Click(object sender, EventArgs e)
        {
            if (statusOrder.Equals("Chờ xác nhận"))
            {

                // Sửa lại giá trị NumberProduct trong Product
                foreach (ProductInOrder productInOrder in productInOrders)
                {
                    // Câu lệnh SQL UPDATE
                    string sqlQuery = "UPDATE Product SET NumberProduct = NumberProduct + @ValueToSubtract WHERE IDProduct = @ProductID";

                    // Tạo kết nối SQL
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Tạo đối tượng SqlCommand
                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        // Thêm các tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@ValueToSubtract", productInOrder.NumberProductOrder);
                        command.Parameters.AddWithValue("@ProductID", productInOrder.IDProduct);

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

                // Câu lệnh SQL để cập nhật StatusOrder trong bảng Order
                string updateQuery = "UPDATE [Order] SET StatusOrder = @NewStatusOrder WHERE IDOrder = @IDOrder";

                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand với câu lệnh SQL và kết nối
                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.AddWithValue("@IDOrder", idOrder);
                    command.Parameters.AddWithValue("@NewStatusOrder", "Đã hủy");

                    // Mở kết nối
                    connection.Open();

                    // Thực hiện câu lệnh UPDATE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Hiển thị thông tin về số hàng đã được cập nhật
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã hủy đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CustomerBills customerBills = new CustomerBills(loginCustomer);
                        customerBills.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else if (statusOrder.Equals("Đang giao"))
            {
                // Câu lệnh SQL để cập nhật StatusOrder trong bảng Order
                string updateQuery = "UPDATE [Order] SET StatusOrder = @NewStatusOrder WHERE IDOrder = @IDOrder";

                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand với câu lệnh SQL và kết nối
                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.AddWithValue("@IDOrder", idOrder);
                    command.Parameters.AddWithValue("@NewStatusOrder", "Đã giao");

                    // Mở kết nối
                    connection.Open();

                    // Thực hiện câu lệnh UPDATE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Hiển thị thông tin về số hàng đã được cập nhật
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CustomerBills customerBills = new CustomerBills(loginCustomer);
                        customerBills.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else if (statusOrder.Equals("Đã giao"))
            {
                // Câu lệnh SQL để cập nhật StatusOrder trong bảng Order
                string updateQuery = "UPDATE [Order] SET StatusOrder = @NewStatusOrder WHERE IDOrder = @IDOrder";

                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand với câu lệnh SQL và kết nối
                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.AddWithValue("@IDOrder", idOrder);
                    command.Parameters.AddWithValue("@NewStatusOrder", "Đã hủy");

                    // Mở kết nối
                    connection.Open();

                    // Thực hiện câu lệnh UPDATE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Hiển thị thông tin về số hàng đã được cập nhật
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CustomerBills customerBills = new CustomerBills(loginCustomer);
                        customerBills.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void InfoCustomerBill_Load(object sender, EventArgs e)
        {
            LoadInfoBill();
            LoadProductInBill(productInOrders);
        }

        private void LoadProductInBill(List<ProductInOrder> productInOrders)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (ProductInOrder productInOrder in productInOrders)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel productPanel = new Panel();
                productPanel.Width = fLPanel.Width - productPanel.Margin.Left - productPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                productPanel.Height = 120; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                Product product = new Product();
                product = GetProductFromProductID(productInOrder.IDProduct);

                // Tạo PictureBox để hiển thị hình ảnh sản phẩm
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = productPanel.Height - 40;
                pictureBox.Height = productPanel.Height - 40;
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
                priceLabel.Text = "Giá: " + productInOrder.PriceProductOrder.ToString() + "VNĐ"; // Định dạng giá thành tiền tệ
                priceLabel.AutoSize = false;
                priceLabel.Width = 100;
                priceLabel.Height = 20;
                priceLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Tạo Label để hiển thị giá sản phẩm
                Label numberLabel = new Label();
                numberLabel.Text = "Số lượng: " + productInOrder.NumberProductOrder.ToString() + "kg"; // Định dạng giá thành tiền tệ
                numberLabel.AutoSize = false;
                numberLabel.Width = 100;
                numberLabel.Height = 20;
                numberLabel.TextAlign = ContentAlignment.MiddleLeft;


                // Đặt vị trí 
                pictureBox.Location = new Point(35, 5);
                nameLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, pictureBox.Location.Y);
                priceLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, nameLabel.Location.Y + 20);
                numberLabel.Location = new Point(pictureBox.Width + 10 + pictureBox.Location.X, priceLabel.Location.Y + 20);

                // Thêm các controls vào Panel
                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(nameLabel);
                productPanel.Controls.Add(priceLabel);
                productPanel.Controls.Add(numberLabel);
                productPanel.Margin = new Padding(productPanel.Margin.Left, 5, productPanel.Margin.Right, 5);
                productPanel.BackColor = Color.White;

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
            CustomerBills customerBill = new CustomerBills(loginCustomer);
            customerBill.Show();
            this.Close();
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

        private void LoadInfoBill()
        {
            int idPayment = 0, idTransportation = 0;

            // Câu lệnh SQL để lấy dữ liệu từ bảng Order
            string query = "SELECT NameOrder, TelephoneOrder ,PriceOrder, DateOrder, AddressOrder, IDPayment, IDTransportation FROM [Order] WHERE IDOrder = @IDOrder";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Tạo SqlCommand với câu lệnh SQL và kết nối
                SqlCommand command = new SqlCommand(query, connection);

                // Thêm tham số cho câu lệnh SQL
                command.Parameters.AddWithValue("@IDOrder", idOrder);
                // Mở kết nối
                connection.Open();

                // Thực hiện câu lệnh và lấy dữ liệu về
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Lấy giá trị từ các cột
                        sumLabel.Text = reader["PriceOrder"].ToString();
                        dateLabel.Text = reader.GetDateTime(reader.GetOrdinal("DateOrder")).ToString("dd/MM/yyyy");
                        addressLabel.Text = reader["AddressOrder"].ToString();
                        nameLabel.Text = reader.GetString(reader.GetOrdinal("NameOrder"));
                        phoneLabel.Text = reader.GetString(reader.GetOrdinal("TelephoneOrder"));
                        idPayment = Convert.ToInt32(reader["IDPayment"].ToString());

                        idTransportation = reader.GetInt32(reader.GetOrdinal("IDTransportation"));


                    }
                }
            }

            // Câu lệnh SQL để lấy dữ liệu từ bảng Payment
            query = "SELECT TypePayment FROM [Payment] WHERE IDPayment = @IDPayment";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Tạo SqlCommand với câu lệnh SQL và kết nối
                SqlCommand command = new SqlCommand(query, connection);

                // Thêm tham số cho câu lệnh SQL
                command.Parameters.AddWithValue("@IDPayment", idPayment);

                // Mở kết nối
                connection.Open();

                // Thực hiện câu lệnh và lấy dữ liệu về
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Lấy giá trị từ các cột
                        paymentLabel.Text = reader.GetString(reader.GetOrdinal("TypePayment"));
                    }
                }
            }

            // Câu lệnh SQL để lấy dữ liệu từ bảng Transportation
            query = "SELECT TypeTransportation FROM [Transportation] WHERE IDTransportation = @IDTransportation";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Tạo SqlCommand với câu lệnh SQL và kết nối
                SqlCommand command = new SqlCommand(query, connection);

                // Thêm tham số cho câu lệnh SQL
                command.Parameters.AddWithValue("@IDTransportation", idTransportation);

                // Mở kết nối
                connection.Open();

                // Thực hiện câu lệnh và lấy dữ liệu về
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Lấy giá trị từ các cột
                        transportationLabel.Text = reader.GetString(reader.GetOrdinal("TypeTransportation"));
                    }
                }
            }

            // Câu lệnh SQL để lấy dữ liệu từ bảng Customer
            query = "SELECT NameSupplier, TelephoneSupplier FROM [Supplier] WHERE LoginSupplier = @LoginSupplier";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Tạo SqlCommand với câu lệnh SQL và kết nối
                SqlCommand command = new SqlCommand(query, connection);

                // Thêm tham số cho câu lệnh SQL
                command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);

                // Mở kết nối
                connection.Open();

                // Thực hiện câu lệnh và lấy dữ liệu về
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Lấy giá trị từ các cột
                        nameSupplierLabel.Text = reader.GetString(reader.GetOrdinal("NameSupplier"));
                        phoneSupplierLabel.Text = reader.GetString(reader.GetOrdinal("TelephoneSupplier"));

                    }
                }
            }
        }
    }
}
