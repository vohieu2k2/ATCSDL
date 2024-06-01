using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATCSDL
{
    public partial class ShowComment : Form
    {
        public string loginCustomer = "";
        public int idProduct = 0;
        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";

        public ShowComment(string loginCustomer, int idProduct)
        {
            InitializeComponent();
            this.loginCustomer = loginCustomer;
            this.idProduct = idProduct;
        }

        private void changeInfoBtn_Click(object sender, EventArgs e)
        {
            AddComment addComment = new AddComment(loginCustomer, idProduct);
            addComment.Show();
            this.Close();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Product WHERE IDProduct = @IDProduct";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDProduct", idProduct);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = new Product(
                            Convert.ToInt32(reader["IDProduct"]),
                            reader["NameProduct"].ToString(),
                            Convert.ToInt32(reader["PriceProduct"]),
                            Convert.ToInt32(reader["NumberProduct"]),
                            ((DateTime)reader["DateProduct"]).ToString("dd-MM-yyyy"),
                            reader["ImageProduct"] != DBNull.Value ?
                            (byte[])reader["ImageProduct"] : new byte[0],
                            reader["DescriptionProduct"].ToString(),
                            reader["AverageScoreProduct"] != DBNull.Value ?
                            Convert.ToDouble(reader["AverageScoreProduct"]) : 0,
                            Convert.ToInt32(reader["IDCategory"]),
                            reader["loginSupplier"].ToString()
                        );
                    }
                }
            }
            BuyProduct buyProduct = new BuyProduct(product, loginCustomer);
            buyProduct.Show();
            this.Close();
        }

        private void ShowComment_Load(object sender, EventArgs e)
        {
            List<Comment> comments = new List<Comment>();
            comments = GetCommentByIdProduct(idProduct);
            DisplayComments(comments);
        }

        private void DisplayComments(List<Comment> comments)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (Comment comment in comments)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel commentPanel = new Panel();
                commentPanel.Width = fLPanel.Width - commentPanel.Margin.Left - commentPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                commentPanel.Height = 100; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                string query = "SELECT NameCustomer FROM Customer WHERE LoginCustomer = @LoginCustomer";
                string nameCustomer = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginCustomer", comment.loginCustomer);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameCustomer = reader["NameCustomer"].ToString();
                        
                    }
                }

                // Tạo Label để hiển thị tên sản phẩm
                Label nameLabel = new Label();
                nameLabel.Text = nameCustomer;
                if (loginCustomer.Equals(comment.loginCustomer))
                {
                    nameLabel.ForeColor = Color.Red;
                }
                nameLabel.AutoSize = false;
                nameLabel.Width = commentPanel.Width - 150 - 35; // Thiết lập chiều rộng cho Label
                nameLabel.Height = 20; // Thiết lập chiều cao cho Label
                nameLabel.TextAlign = ContentAlignment.MiddleLeft; // Canh giữa nội dung

                // Tạo Label để hiển thị giá sản phẩm
                Label datePanel = new Label();
                datePanel.Text = comment.date; // Định dạng giá thành tiền tệ
                datePanel.AutoSize = false;
                datePanel.Width = commentPanel.Width - nameLabel.Width - 35;
                datePanel.Height = 20;
                datePanel.TextAlign = ContentAlignment.MiddleRight;

                // Tạo Label để hiển thị giá sản phẩm
                Label contentComment = new Label();
                contentComment.Text = comment.content; // Định dạng giá thành tiền tệ
                contentComment.AutoSize = false;
                contentComment.Width = commentPanel.Width;
                contentComment.Height = 40;
                contentComment.TextAlign = ContentAlignment.MiddleLeft;
                contentComment.Click += new EventHandler(ContentLabel_Click);

                // Tạo Label để hiển thị giá sản phẩm
                Label scoreComment = new Label();
                scoreComment.Text = comment.score.ToString(); // Định dạng giá thành tiền tệ
                scoreComment.AutoSize = false;
                scoreComment.Width = 100;
                scoreComment.Height = 20;
                scoreComment.TextAlign = ContentAlignment.MiddleLeft;

                // Đặt vị trí 
                nameLabel.Location = new Point(35, 5);
                datePanel.Location = new Point(nameLabel.Location.X + nameLabel.Width, 5);
                contentComment.Location = new Point(35, nameLabel.Location.Y + nameLabel.Height);
                scoreComment.Location = new Point(35, contentComment.Location.Y + contentComment.Height);

                // Thêm các controls vào Panel
                commentPanel.Controls.Add(nameLabel);
                commentPanel.Controls.Add(datePanel);
                commentPanel.Controls.Add(contentComment);
                commentPanel.Controls.Add(scoreComment);
                commentPanel.Margin = new Padding(commentPanel.Margin.Left, 5, commentPanel.Margin.Right, 5);
                commentPanel.BackColor = Color.White;
                commentPanel.Tag = comment;
                commentPanel.Click += new EventHandler(Panel_Click); // Gắn sự kiện Click

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(commentPanel);
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            Comment comment = clickedPanel.Tag as Comment;

            EditComment editComment = new EditComment(comment);

            editComment.Show();
            this.Close();
        }

        private void ContentLabel_Click(object sender, EventArgs e)
        {
            Panel panelContainingLabel = (Panel)((Label)sender).Parent;

            Comment comment = panelContainingLabel.Tag as Comment;

            EditComment editComment = new EditComment(comment);

            editComment.Show();
            this.Close();

        }

        private List<Comment> GetCommentByIdProduct(int idProduct)
        {
            List<Comment> datalist = new List<Comment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Comment WHERE IDProduct = @IDProduct";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDProduct", idProduct);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment data = new Comment(
                            Convert.ToInt32(reader["IDComment"]),
                            reader["ContentComment"].ToString(),
                            Convert.ToInt32(reader["ScoreComment"]),
                            Convert.ToInt32(reader["IDProduct"]),
                            Convert.ToDateTime(reader["DateLastUpdate"]).ToString("dd/MM/yyyy"),
                            reader["LoginCustomer"].ToString()
                            );
                        datalist.Add(data);
                    }
                }
            }
            return datalist;
        }
    }
}
