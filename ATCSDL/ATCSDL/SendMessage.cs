using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ATCSDL
{
    public partial class SendMessage : Form
    {
        public string loginSupplier, loginCustomer;
        public int page;
        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";
        public List<Mes> messages = new List<Mes>();
        public Product product;

        public SendMessage(string loginSupplier, string loginCustomer, int page)
        {
            InitializeComponent();
            this.loginSupplier = loginSupplier;
            this.loginCustomer = loginCustomer;
            this.page = page;
        }

        public SendMessage(string loginSupplier, string loginCustomer, int page, Product product)
        {
            InitializeComponent();
            this.loginSupplier = loginSupplier;
            this.loginCustomer = loginCustomer;
            this.page = page;
            this.product = product;
        }

        private void SendMessage_Load(object sender, EventArgs e)
        {
            // load nameLabel
            //1 Supplier, 2 Customer
            if(page == 1)
            {
                string query = "SELECT NameCustomer FROM Customer WHERE LoginCustomer = @LoginCustomer";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginCustomer", loginCustomer);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameLabel.Text = reader["NameCustomer"].ToString();
                    }
                }
            } else if( page == 2 || page == 3) 
            {
                string query = "SELECT NameSupplier FROM Supplier WHERE LoginSupplier = @LoginSupplier";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginSupplier", loginSupplier);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameLabel.Text = reader["NameSupplier"].ToString();
                    }
                }
            }

            // lấy tin nhắn
            if(page == 1)
            {
                messages = GetMessageSupplier();
            }
            else
            {
                messages = GetMessageCustomer();
            }

            DisplayMessageInConversation(messages);
        }

        private void DisplayMessageInConversation(List<Mes> messages)
        {
            //1 cho Supplier, 2 3 cho Customer
            if (page == 1)
            {
                fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

                foreach (Mes message in messages)
                {
                    // Tạo Panel chứa thông tin sản phẩm
                    Panel mesPanel = new Panel();
                    mesPanel.Width = fLPanel.Width - mesPanel.Margin.Left - mesPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                    mesPanel.Height = 70; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                    // Tạo Label để hiển thị ngày
                    Label dateLabel = new Label();
                    dateLabel.Text = message.timeSendMes; // Định dạng giá thành tiền tệ
                    dateLabel.AutoSize = false;
                    dateLabel.Width = mesPanel.Width - 70;
                    dateLabel.Height = 20;
                    dateLabel.TextAlign = ContentAlignment.MiddleCenter;

                    // Tạo Label để hiển thị nội dung
                    Label contentLabel = new Label();
                    contentLabel.Text = message.contentMes;
                    contentLabel.AutoSize = false;
                    contentLabel.Width = mesPanel.Width - 35;
                    contentLabel.Height = 40;
                    if (message.loginSender.Equals(loginSupplier))
                    {
                        contentLabel.TextAlign = ContentAlignment.MiddleRight;
                    }
                    else
                    {
                        contentLabel.TextAlign = ContentAlignment.MiddleLeft;
                    }

                    // Đặt vị trí 
                    dateLabel.Location = new Point(35, 5);
                    contentLabel.Location = new Point(35, dateLabel.Location.Y + dateLabel.Height);

                    // Thêm các controls vào Panel
                    mesPanel.Controls.Add(dateLabel);
                    mesPanel.Controls.Add(contentLabel);
                    mesPanel.Margin = new Padding(mesPanel.Margin.Left, 0, mesPanel.Margin.Right, 0);
                    mesPanel.BackColor = Color.White;

                    // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                    fLPanel.Controls.Add(mesPanel);
                }
            }
            else if (page == 2 || page == 3)
            {
                fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

                foreach (Mes message in messages)
                {
                    // Tạo Panel chứa thông tin sản phẩm
                    Panel mesPanel = new Panel();
                    mesPanel.Width = fLPanel.Width - mesPanel.Margin.Left - mesPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                    mesPanel.Height = 70; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                    // Tạo Label để hiển thị ngày sản phẩm
                    Label dateLabel = new Label();
                    dateLabel.Text = message.timeSendMes; // Định dạng giá thành tiền tệ
                    dateLabel.AutoSize = false;
                    dateLabel.Width = mesPanel.Width - 70;
                    dateLabel.Height = 20;
                    dateLabel.TextAlign = ContentAlignment.MiddleCenter;

                    // Tạo Label để hiển thị nội dung
                    Label contentLabel = new Label();
                    contentLabel.Text = message.contentMes; // Định dạng giá thành tiền tệ
                    contentLabel.AutoSize = false;
                    contentLabel.Width = mesPanel.Width - 35;
                    contentLabel.Height = 40;
                    if (message.loginSender.Equals(loginCustomer))
                    {
                        contentLabel.TextAlign = ContentAlignment.MiddleRight;
                    }
                    else
                    {
                        contentLabel.TextAlign = ContentAlignment.MiddleLeft;
                    }

                    // Đặt vị trí 
                    dateLabel.Location = new Point(35, 5);
                    contentLabel.Location = new Point(35, dateLabel.Location.Y + dateLabel.Height);

                    // Thêm các controls vào Panel
                    mesPanel.Controls.Add(dateLabel);
                    mesPanel.Controls.Add(contentLabel);
                    mesPanel.Margin = new Padding(mesPanel.Margin.Left, 0, mesPanel.Margin.Right, 0);
                    mesPanel.BackColor = Color.White;

                    // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                    fLPanel.Controls.Add(mesPanel);
                }
            }
        }

        private List<Mes> GetMessageSupplier()
        {
            List<Mes> datalist = new List<Mes>();

            string queryString = "SELECT * FROM Message WHERE LoginSupplier = @LoginSupplier";

            // Lấy các tin nhan cuối cùng của loginCustomer
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mes data = new Mes(
                            Convert.ToInt32(reader["IDMessage"].ToString()),
                            reader["ContentMessage"].ToString(),
                            ((DateTime)reader["TimeSendMessage"]).ToString("dd/MM/yyyy HH:mm"),
                            reader["LoginSender"].ToString(),
                            loginCustomer,
                            loginSupplier,
                            Convert.ToBoolean(reader["StatusMessage"])
                            );
                    datalist.Add(data);
                }
                reader.Close();
            }

            return datalist;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sendMesTxt.Text))
            {
                MessageBox.Show("Vui lòng nhập tin nhắn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng kiểm tra nếu có ít nhất một trường trống
            }
            // 1 Supplier, 2 3 Customer
            if (page == 1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Message (ContentMessage, TimeSendMessage, LoginSender, LoginCustomer, LoginSupplier, StatusMessage) " +
                                   "VALUES (@ContentMessage, @TimeSendMessage, @LoginSender, @LoginCustomer, @LoginSupplier, @StatusMessage)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContentMessage", sendMesTxt.Text);
                        command.Parameters.AddWithValue("@TimeSendMessage", DateTime.Now);
                        command.Parameters.AddWithValue("@LoginSender", loginSupplier);
                        command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);
                        command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);
                        command.Parameters.AddWithValue("@StatusMessage", false);

                        connection.Open();
                        // Thực hiện truy vấn SQL để chèn dữ liệu
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            messages.Clear();
                            messages = GetMessageSupplier();
                            DisplayMessageInConversation(messages);
                            sendMesTxt.Text = "";
                        }
                        else
                        {
                            // Ghi dữ liệu không thành công
                            MessageBox.Show("Lỗi! Vui lòng gửi lại tin nhắn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } else if(page == 2 || page == 3) 
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Message (ContentMessage, TimeSendMessage, LoginSender, LoginCustomer, LoginSupplier, StatusMessage) " +
                                   "VALUES (@ContentMessage, @TimeSendMessage, @LoginSender, @LoginCustomer, @LoginSupplier, @StatusMessage)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContentMessage", sendMesTxt.Text);
                        command.Parameters.AddWithValue("@TimeSendMessage", DateTime.Now);
                        command.Parameters.AddWithValue("@LoginSender", loginCustomer);
                        command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);
                        command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);
                        command.Parameters.AddWithValue("@StatusMessage", false);

                        connection.Open();
                        // Thực hiện truy vấn SQL để chèn dữ liệu
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            messages.Clear();
                            messages = GetMessageCustomer();
                            DisplayMessageInConversation(messages);
                            sendMesTxt.Text = "";
                        }
                        else
                        {
                            // Ghi dữ liệu không thành công
                            MessageBox.Show("Lỗi! Vui lòng gửi lại tin nhắn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if(page == 1)
            {
                Conversation conversation = new Conversation(loginSupplier, 1);
                conversation.Show();
                this.Close();
            } else if(page == 2)
            {
                Conversation conversation = new Conversation(loginCustomer, 2);
                conversation.Show();
                this.Close();
            } else if(page == 3)
            {
                BuyProduct buyProduct = new BuyProduct(product, loginCustomer);
                buyProduct.Show();
                this.Close();
            }
        }

        private List<Mes> GetMessageCustomer()
        {
            List<Mes> datalist = new List<Mes>();

            string queryString = "SELECT * FROM Message WHERE LoginCustomer = @LoginCustomer";

            // Lấy các tin nhan cuối cùng của loginCustomer
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mes data = new Mes(
                            Convert.ToInt32(reader["IDMessage"].ToString()),
                            reader["ContentMessage"].ToString(),
                            ((DateTime)reader["TimeSendMessage"]).ToString("dd/MM/yyyy HH:mm"),
                            reader["LoginSender"].ToString(),
                            loginCustomer,
                            loginSupplier,
                            Convert.ToBoolean(reader["StatusMessage"])
                            );
                    datalist.Add(data);
                }
                reader.Close();
            }

            return datalist;
        }
    }
}
