using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace ATCSDL
{
    public partial class BuyProduct : Form
    {
        public Product product;
        public string loginCustomer;
        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";
        public bool home = true;
        public BuyProduct(Product product, string loginCustomer)
        {
            InitializeComponent();
            this.product = product;
            this.loginCustomer = loginCustomer;
        }

        public BuyProduct(Product product, string loginCustomer, bool home)
        {
            InitializeComponent();
            this.product = product;
            this.loginCustomer = loginCustomer;
            this.home = home;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if (home)
            {
                HomeCustomer home = new HomeCustomer(loginCustomer);
                home.Show();
                // Đóng addProduct (tùy chọn)
                this.Close();
            }
            else
            {
                int idCart = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Câu truy vấn SQL để lấy IDCart
                    string query = "SELECT IDCart FROM Cart WHERE LoginCustomer = @LoginCustomer";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoginCustomer", loginCustomer); // loginCustomer là biến string chứa LoginCustomer

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idCart = reader.GetInt32(0); // Lấy giá trị IDCart từ cột đầu tiên
                            }
                        }
                    }
                    connection.Close();
                }
                Cart cart = new Cart(loginCustomer, idCart);
                cart.Show();
                this.Close();
            }
            
        }

        private void BuyProduct_Load(object sender, EventArgs e)
        {
            nameLabel.Text = product.Name;
            priceLabel.Text = product.Price.ToString();
            numberLabel.Text = product.number.ToString();
            if(product.category == 1)
            {
                categoryLabel.Text = "Thực vật";
            }
            else if(product.category == 2) 
            {
                categoryLabel.Text = "Động vật";
            }
            if(product.score == 0)
            {
                scoreLabel.Text = "Chưa có lượt đánh giá";
            }
            else
            {
                scoreLabel.Text=product.score.ToString();
            }
            descriptionTxt.Text = product.description;
            dateLabel.Text = product.Datetime;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Câu truy vấn SQL
                string query = "SELECT NameSupplier FROM Supplier WHERE LoginSupplier = @LoginSupplier";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoginSupplier", product.loginSupplier);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            supplierLabel.Text = reader["NameSupplier"].ToString();
                        }
                    }
                }
            }

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

        }

        private Image ByteToImage(byte[] byteArray)
        {
            using (MemoryStream m = new MemoryStream(byteArray))
            {
                return Image.FromStream(m);
            }
        }

        private void numBuyTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là phím điều khiển (như Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này vào TextBox
            }
        }

        private void addCartBtn_Click(object sender, EventArgs e)
        {
           if(string.IsNullOrEmpty(numBuyTxt.Text) || Convert.ToInt32(numBuyTxt.Text) > product.number || Convert.ToInt32(numBuyTxt.Text) < 0)
           {
                MessageBox.Show("Vui lòng nhập số lượng hàng hóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
           }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Biến chứa IDCart
                int idCart = 0;

                // Câu truy vấn SQL để lấy IDCart
                string query = "SELECT IDCart FROM Cart WHERE LoginCustomer = @LoginCustomer";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoginCustomer", loginCustomer); // loginCustomer là biến string chứa LoginCustomer

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idCart = reader.GetInt32(0); // Lấy giá trị IDCart từ cột đầu tiên
                        }

                        if (idCart == 0)
                        {
                            MessageBox.Show("Thêm vào thất bại. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                int numberInCart = 0;
                // Câu lệnh SQL để lấy NumberProductInCart từ bảng ProductInCart
                string selectQuery = "SELECT NumberProductInCart FROM ProductInCart WHERE IDCart = @IDCart AND IDProduct = @IDProduct";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IDCart", idCart);
                    selectCommand.Parameters.AddWithValue("@IDProduct", product.ID);

                    // Thực thi câu lệnh SQL và đọc giá trị trả về
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read()) // Nếu có dữ liệu
                        {
                            numberInCart = reader.GetInt32(0); // Lấy giá trị NumberProductInCart
                        }
                    }
                }

                if (numberInCart == 0)
                {
                    // Câu lệnh SQL để chèn dữ liệu vào bảng ProductInCart
                    string insertQuery = @"INSERT INTO ProductInCart (IDProduct, IDCart, NumberProductInCart)
                       VALUES (@IDProduct, @IDCart, @NumberProductInCart)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@IDProduct", product.ID); // Giả sử IDProductInCart bạn muốn chèn giống IDProduct
                        insertCommand.Parameters.AddWithValue("@IDCart", idCart);
                        insertCommand.Parameters.AddWithValue("@NumberProductInCart", Convert.ToInt32(numBuyTxt.Text));

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm vào thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Thêm vào thất bại. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                } else if(numberInCart > 0 && (numberInCart + Convert.ToInt32(numBuyTxt.Text) <= product.number))
                {
                    // Câu lệnh SQL để cập nhật NumberProductInCart
                    string updateQuery = "UPDATE ProductInCart SET NumberProductInCart = @NewNumberInCart WHERE IDCart = @IDCart AND IDProduct = @IDProduct";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@NewNumberInCart", numberInCart + Convert.ToInt32(numBuyTxt.Text));
                        updateCommand.Parameters.AddWithValue("@IDCart", idCart);
                        updateCommand.Parameters.AddWithValue("@IDProduct", product.ID);

                        // Thực thi câu lệnh SQL cập nhật
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm vào thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Thêm vào thất bại. Vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                } else if (numberInCart > 0 && (numberInCart + Convert.ToInt32(numBuyTxt.Text) > product.number))
                {
                    MessageBox.Show("Vui lòng nhập số lượng hàng hóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

        }

        private void cartBtn_Click(object sender, EventArgs e)
        {
            // Biến chứa IDCart
            int idCart = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Câu truy vấn SQL để lấy IDCart
                string query = "SELECT IDCart FROM Cart WHERE LoginCustomer = @LoginCustomer";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoginCustomer", loginCustomer); // loginCustomer là biến string chứa LoginCustomer

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idCart = reader.GetInt32(0); // Lấy giá trị IDCart từ cột đầu tiên
                        }
                    }
                }
                connection.Close();
            }

            Cart cart = new Cart(loginCustomer, idCart);
            cart.Show();
            this.Hide();
        }

        private void buyBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numBuyTxt.Text))
            {
                PayBill directBuy = new PayBill(1, loginCustomer, product, Convert.ToInt32(numBuyTxt.Text));
                directBuy.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số lượng đặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                
        }

        private void commentBtn_Click(object sender, EventArgs e)
        {
            ShowComment showComment = new ShowComment(loginCustomer, product.ID);
            showComment.Show();
            this.Close();
        }

        private void mesBtn_Click(object sender, EventArgs e)
        {
            SendMessage sendMessage = new SendMessage(product.loginSupplier, loginCustomer, 3, product);
            sendMessage.Show();
            this.Close();
        }
    }
}
