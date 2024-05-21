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
    public partial class HomeSupplier : Form
    {

        public string loginSupplier = "";

        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";

        public HomeSupplier(string inputAcconut)
        {
            InitializeComponent();
            loginSupplier = inputAcconut;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct(loginSupplier);
            addProduct.Show();
            this.Hide();
        }

        private void HomeSupplier_Load(object sender, EventArgs e)
        {
            //// Khởi tạo danh sách sản phẩm và gọi hàm hiển thị
            List<Product> products = new List<Product>();
            products = GetProductFromSql();
            DisplayProducts(products);
        }

        private void DisplayProducts(List<Product> products)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (Product product in products)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel productPanel = new Panel();
                productPanel.Width = 200; // Thiết lập chiều rộng cho Panel
                productPanel.Height = 250; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                // Tạo PictureBox để hiển thị hình ảnh sản phẩm
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = 150;
                pictureBox.Height = 150;
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
                numberLabel.Text = "Số lượng: "+ product.number.ToString() + "kg"; // Định dạng giá thành tiền tệ
                numberLabel.AutoSize = false;
                numberLabel.Width = 100;
                numberLabel.Height = 20;
                numberLabel.TextAlign = ContentAlignment.MiddleRight;

                // Tạo Label để hiển thị giá sản phẩm
                Label categoryLabel = new Label();
                if(product.category == 1)
                {
                    categoryLabel.Text = "Danh mục: Thực vật";
                }
                else
                {
                    categoryLabel.Text = "Danh mục: Động vật";
                }
                categoryLabel.AutoSize = false;
                categoryLabel.Width = 200;
                categoryLabel.Height = 20;
                categoryLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Tạo Label để hiển thị giá sản phẩm
                Label dateLabel = new Label();
                dateLabel.Text = "Ngày đăng: " + product.Datetime;
                dateLabel.AutoSize = false;
                dateLabel.Width = 200;
                dateLabel.Height = 20;
                dateLabel.TextAlign = ContentAlignment.MiddleLeft;

                // Đặt vị trí 
                pictureBox.Location = new Point(25, 0);
                nameLabel.Location = new Point(0, pictureBox.Height + 5);
                priceLabel.Location = new Point(0, nameLabel.Location.Y + nameLabel.Height + 5);
                numberLabel.Location = new Point(priceLabel.Width, priceLabel.Location.Y);
                categoryLabel.Location = new Point(0, numberLabel.Location.Y + numberLabel.Height + 5);
                dateLabel.Location = new Point(0, categoryLabel.Location.Y + categoryLabel.Height + 5);

                // Thêm các controls vào Panel
                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(nameLabel);
                productPanel.Controls.Add(priceLabel);
                productPanel.Controls.Add(numberLabel);
                productPanel.Controls.Add(categoryLabel);
                productPanel.Controls.Add(dateLabel);
                productPanel.Margin = new Padding(5);
                productPanel.BackColor = Color.White;
                // Lưu trữ thông tin Product vào Tag của panel
                productPanel.Tag = product;
                productPanel.Click += new EventHandler(Panel_Click); // Gắn sự kiện Click

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(productPanel);
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            Panel parentPanel = (sender as PictureBox)?.Parent as Panel;

            Product product = parentPanel.Tag as Product;

            EditProduct editProduct = new EditProduct(product);

            editProduct.Show();
            this.Hide();
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            Product product = clickedPanel.Tag as Product;

            EditProduct editProduct = new EditProduct(product);

            editProduct.Show();
            this.Hide();
        }

        public List<Product> GetProductFromSql()
        {
            List<Product> dataList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Product WHERE LoginSupplier = @LoginSupplier"; // Thay columnName và tableName bằng tên cột và bảng của bạn
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);
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
                            (byte[])reader["ImageProduct"],
                            reader["DescriptionProduct"].ToString(),
                            reader["AverageScoreProduct"] != DBNull.Value ?
                            Convert.ToDouble(reader["AverageScoreProduct"]) : 0,
                            Convert.ToInt32(reader["IDCategory"]),
                            reader["loginSupplier"].ToString()
                            );
              
                        dataList.Add(data);
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

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            // Đóng addProduct (tùy chọn)
            this.Hide();
        }


        private void mesBtn_Click(object sender, EventArgs e)
        {
            Conversation conversation = new Conversation(loginSupplier, 1);
            conversation.Show();
            this.Close();
        }

        private void billsBtn_Click(object sender, EventArgs e)
        {
            SupplierBills supplierBills = new SupplierBills(loginSupplier);
            supplierBills.Show();
            this.Hide();
        }
    }
}
