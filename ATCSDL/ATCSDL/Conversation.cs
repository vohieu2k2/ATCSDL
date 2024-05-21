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
using System.Xml.Linq;

namespace ATCSDL
{
    public partial class Conversation : Form
    {
        public string loginCustomer = "", loginSupplier = "";
        public int page;
        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";

        public Conversation(string login, int page)
        {
            InitializeComponent();
            //1 là Supplier, 2 là Customer
            this.page = page;
            if (page == 1)
            {
                loginSupplier = login;
            } else if(page == 2)
            {
                loginCustomer = login;
            }
        }

        private void Conversation_Load(object sender, EventArgs e)
        {
            List<Mes> conversations = new List<Mes>();
            //Đang là của Supplier
            if (page == 1)
            {
                conversations.Clear();
                string queryString = "SELECT * FROM Message WHERE LoginSupplier = @LoginSupplier";

                // Lấy các tin nhan cuối cùng của loginSupplier
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@LoginSupplier", loginSupplier);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bool isExist = false;
                        string loginCustomer = reader["LoginCustomer"].ToString();
                        foreach (Mes conversation in conversations)
                        {
                            if (conversation.loginCustomer == loginCustomer)
                            {
                                conversation.idMes = Convert.ToInt32(reader["IDMessage"].ToString());
                                conversation.contentMes = reader["ContentMessage"].ToString();
                                conversation.timeSendMes = ((DateTime)reader["TimeSendMessage"]).ToString("dd/MM/yyyy HH:mm");
                                conversation.loginSender = reader["LoginSender"].ToString();
                                conversation.statusMes = Convert.ToBoolean(reader["StatusMessage"]);
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            Mes conversation = new Mes(
                                Convert.ToInt32(reader["IDMessage"].ToString()),
                                reader["ContentMessage"].ToString(),
                                ((DateTime)reader["TimeSendMessage"]).ToString("dd/MM/yyyy HH:mm"),
                                reader["LoginSender"].ToString(),
                                loginCustomer,
                                loginSupplier,
                                Convert.ToBoolean(reader["StatusMessage"])
                                );
                            conversations.Add(conversation);
                        }
                    }
                    reader.Close();
                }

                // Sắp xếp các tin nhắn cuối cùng theo thời gian
                conversations.Sort((c1, c2)
                    => -DateTime.ParseExact(c1.timeSendMes, "dd/MM/yyyy HH:mm", null).CompareTo(DateTime.ParseExact(c2.timeSendMes, "dd/MM/yyyy HH:mm", null)));

                DisplaySupplierConversation(conversations);
            }
            else if (page == 2)
            {
                conversations.Clear();
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
                        bool isExist = false;
                        string loginSupplier = reader["LoginSupplier"].ToString();
                        foreach (Mes conversation in conversations)
                        {
                            if (conversation.loginSupplier == loginSupplier)
                            {
                                conversation.idMes = Convert.ToInt32(reader["IDMessage"].ToString());
                                conversation.contentMes = reader["ContentMessage"].ToString();
                                conversation.timeSendMes = ((DateTime)reader["TimeSendMessage"]).ToString("dd/MM/yyyy HH:mm");
                                conversation.loginSender = reader["LoginSender"].ToString();
                                conversation.statusMes = Convert.ToBoolean(reader["StatusMessage"]);
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            Mes conversation = new Mes(
                                Convert.ToInt32(reader["IDMessage"].ToString()),
                                reader["ContentMessage"].ToString(),
                                ((DateTime)reader["TimeSendMessage"]).ToString("dd/MM/yyyy HH:mm"),
                                reader["LoginSender"].ToString(),
                                loginCustomer,
                                loginSupplier,
                                Convert.ToBoolean(reader["StatusMessage"])
                                );
                            conversations.Add(conversation);
                        }
                    }
                    reader.Close();
                }

                // Sắp xếp các tin nhắn cuối cùng theo thời gian
                conversations.Sort((c1, c2)
                    => -DateTime.ParseExact(c1.timeSendMes, "dd/MM/yyyy HH:mm", null).CompareTo(DateTime.ParseExact(c2.timeSendMes, "dd/MM/yyyy HH:mm", null)));

                DisplayCustomerConversation(conversations);
            }
        }

        private void DisplayCustomerConversation(List<Mes> conversations)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (Mes conversation in conversations)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel mesPanel = new Panel();
                mesPanel.Width = fLPanel.Width - mesPanel.Margin.Left - mesPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                mesPanel.Height = 80; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                string query = "SELECT NameSupplier FROM Supplier WHERE LoginSupplier = @LoginSupplier";
                string nameSupplier = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginSupplier", conversation.loginSupplier);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameSupplier = reader["NameSupplier"].ToString();
                    }
                }

                // Tạo Label để hiển thị tên sản phẩm
                Label nameLabel = new Label();
                nameLabel.Text = nameSupplier;
                nameLabel.AutoSize = false;
                nameLabel.Width = mesPanel.Width - 150 - 35; // Thiết lập chiều rộng cho Label
                nameLabel.Height = 25; // Thiết lập chiều cao cho Label
                nameLabel.TextAlign = ContentAlignment.MiddleLeft; // Canh giữa nội dung

                // Tạo Label để hiển thị ngày sản phẩm
                Label dateLabel = new Label();
                dateLabel.Text = conversation.timeSendMes; // Định dạng giá thành tiền tệ
                dateLabel.AutoSize = false;
                dateLabel.Width = mesPanel.Width - nameLabel.Width - 35;
                dateLabel.Height = 20;
                dateLabel.TextAlign = ContentAlignment.MiddleRight;

                query = "SELECT NameCustomer FROM Customer WHERE LoginCustomer = @LoginCustomer";
                string nameCustomer = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginCustomer", conversation.loginCustomer);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameCustomer = reader["NameCustomer"].ToString();
                    }
                }

                string nameSender = "";
                if (conversation.loginSender.Equals(conversation.loginCustomer))
                {
                    nameSender = nameCustomer;
                }
                else if (conversation.loginSender.Equals(conversation.loginSupplier))
                {
                    nameSender = nameSupplier;
                }

                // Tạo Label để hiển thị nội dung
                Label contentLabel = new Label();
                contentLabel.Text = nameSender + ": " + conversation.contentMes; // Định dạng giá thành tiền tệ
                contentLabel.AutoSize = false;
                contentLabel.Width = mesPanel.Width;
                contentLabel.Height = 40;
                contentLabel.TextAlign = ContentAlignment.MiddleLeft;
                contentLabel.Click += new EventHandler(ContentLabel_Click);

                if (!conversation.statusMes && !conversation.loginSender.Equals(loginCustomer))
                {
                    nameLabel.Font = new Font(nameLabel.Font, FontStyle.Bold);
                    dateLabel.Font = new Font(dateLabel.Font, FontStyle.Bold);
                    contentLabel.Font = new Font(contentLabel.Font, FontStyle.Bold);

                }

                // Đặt vị trí 
                nameLabel.Location = new Point(35, 5);
                dateLabel.Location = new Point(nameLabel.Location.X + nameLabel.Width, 5);
                contentLabel.Location = new Point(35, nameLabel.Location.Y + nameLabel.Height);

                // Thêm các controls vào Panel
                mesPanel.Controls.Add(nameLabel);
                mesPanel.Controls.Add(dateLabel);
                mesPanel.Controls.Add(contentLabel);
                mesPanel.Margin = new Padding(mesPanel.Margin.Left, 5, mesPanel.Margin.Right, 5);
                mesPanel.BackColor = Color.White;
                mesPanel.Tag = conversation;
                mesPanel.Click += new EventHandler(Panel_Click); // Gắn sự kiện Click

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(mesPanel);
            }
        }

        private void DisplaySupplierConversation(List<Mes> conversations)
        {
            fLPanel.Controls.Clear(); // Xóa các controls hiện có trong FlowLayoutPanel

            foreach (Mes conversation in conversations)
            {
                // Tạo Panel chứa thông tin sản phẩm
                Panel mesPanel = new Panel();
                mesPanel.Width = fLPanel.Width - mesPanel.Margin.Left - mesPanel.Margin.Right; // Thiết lập chiều rộng cho Panel
                mesPanel.Height = 80; // Tăng chiều cao để hiển thị tên và giá sản phẩm

                string query = "SELECT NameCustomer FROM Customer WHERE LoginCustomer = @LoginCustomer";
                string nameCustomer = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginCustomer", conversation.loginCustomer);

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
                nameLabel.AutoSize = false;
                nameLabel.Width = mesPanel.Width -150 - 35; // Thiết lập chiều rộng cho Label
                nameLabel.Height = 25; // Thiết lập chiều cao cho Label
                nameLabel.TextAlign = ContentAlignment.MiddleLeft; // Canh giữa nội dung

                // Tạo Label để hiển thị ngày sản phẩm
                Label dateLabel = new Label();
                dateLabel.Text = conversation.timeSendMes; // Định dạng giá thành tiền tệ
                dateLabel.AutoSize = false;
                dateLabel.Width = mesPanel.Width - nameLabel.Width - 35;
                dateLabel.Height = 20;
                dateLabel.TextAlign = ContentAlignment.MiddleRight;

                query = "SELECT NameSupplier FROM Supplier WHERE LoginSupplier = @LoginSupplier";
                string nameSupplier = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@LoginSupplier", conversation.loginSupplier);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameSupplier = reader["NameSupplier"].ToString();
                    }
                }

                string nameSender = "";
                if (conversation.loginSender.Equals(conversation.loginCustomer))
                {
                    nameSender = nameCustomer;
                }
                else if (conversation.loginSender.Equals(conversation.loginSupplier))
                {
                    nameSender = nameSupplier;
                }

                // Tạo Label để hiển thị nội dung
                Label contentLabel = new Label();
                contentLabel.Text = nameSender + ": " + conversation.contentMes; // Định dạng giá thành tiền tệ
                contentLabel.AutoSize = false;
                contentLabel.Width = mesPanel.Width;
                contentLabel.Height = 40;
                contentLabel.TextAlign = ContentAlignment.MiddleLeft;
                contentLabel.Click += new EventHandler(ContentLabel_Click);

                if (!conversation.statusMes && !conversation.loginSender.Equals(loginSupplier))
                {
                    nameLabel.Font = new Font(nameLabel.Font, FontStyle.Bold);
                    dateLabel.Font = new Font(dateLabel.Font, FontStyle.Bold);
                    contentLabel.Font = new Font(contentLabel.Font, FontStyle.Bold);

                }

                // Đặt vị trí 
                nameLabel.Location = new Point(35, 5);
                dateLabel.Location = new Point(nameLabel.Location.X + nameLabel.Width, 5);
                contentLabel.Location = new Point(35, nameLabel.Location.Y + nameLabel.Height);

                // Thêm các controls vào Panel
                mesPanel.Controls.Add(nameLabel);
                mesPanel.Controls.Add(dateLabel);
                mesPanel.Controls.Add(contentLabel);
                mesPanel.Margin = new Padding(mesPanel.Margin.Left, 5, mesPanel.Margin.Right, 5);
                mesPanel.BackColor = Color.White;
                mesPanel.Tag = conversation;
                mesPanel.Click += new EventHandler(Panel_Click); // Gắn sự kiện Click

                // Thêm Panel chứa thông tin sản phẩm vào FlowLayoutPanel
                fLPanel.Controls.Add(mesPanel);
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            Mes conversation = clickedPanel.Tag as Mes;

            if(page == 1 && !conversation.loginSender.Equals(loginCustomer) && !conversation.statusMes)
            {
                // Tạo kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu lệnh SQL UPDATE
                    string updateQuery = "UPDATE Message SET StatusMessage = @StatusMessage WHERE IDMessage = @IDMessage";

                    // Sử dụng SqlCommand để thực thi câu lệnh
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Thiết lập tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@StatusMessage", true);
                        command.Parameters.AddWithValue("@IDMessage", conversation.idMes);

                        // Thực thi câu lệnh
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            } else if (page == 2 && !conversation.loginSender.Equals(loginCustomer) && !conversation.statusMes)
            {
                // Tạo kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu lệnh SQL UPDATE
                    string updateQuery = "UPDATE Message SET StatusMessage = @StatusMessage WHERE IDMessage = @IDMessage";

                    // Sử dụng SqlCommand để thực thi câu lệnh
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Thiết lập tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@IDMessage", conversation.idMes);
                        command.Parameters.AddWithValue("@StatusMessage", true);

                        // Thực thi câu lệnh
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }

            SendMessage showMessage = new SendMessage(conversation.loginSupplier, conversation.loginCustomer, page);

            showMessage.Show();
            this.Close();
        }

        private void ContentLabel_Click(object sender, EventArgs e)
        {
            Panel panelContainingLabel = (Panel)((Label)sender).Parent;

            Mes conversation = panelContainingLabel.Tag as Mes;

            if (page == 1 && !conversation.loginSender.Equals(loginSupplier) && !conversation.statusMes)
            {
                // Tạo kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu lệnh SQL UPDATE
                    string updateQuery = "UPDATE Message SET StatusMessage = @StatusMessage WHERE IDMessage = @IDMessage";

                    // Sử dụng SqlCommand để thực thi câu lệnh
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Thiết lập tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@StatusMessage", true);
                        command.Parameters.AddWithValue("@IDMessage", conversation.idMes);

                        // Thực thi câu lệnh
                        command.ExecuteNonQuery();
                    }
                }
            }
            else if (page == 2 && !conversation.loginSender.Equals(loginCustomer) && !conversation.statusMes)
            {
                // Tạo kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo câu lệnh SQL UPDATE
                    string updateQuery = "UPDATE Message SET StatusMessage = @StatusMessage WHERE IDMessage = @IDMessage";

                    // Sử dụng SqlCommand để thực thi câu lệnh
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Thiết lập tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@StatusMessage", true);
                        command.Parameters.AddWithValue("@IDMessage", conversation.idMes);

                        // Thực thi câu lệnh
                        command.ExecuteNonQuery();
                    }
                }
            }

            SendMessage showMessage = new SendMessage(conversation.loginSupplier, conversation.loginCustomer, page);

            showMessage.Show();
            this.Close();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if(page == 1)
            {
                HomeSupplier home = new HomeSupplier(loginSupplier);
                home.Show();
                this.Close();
            }
            else if(page ==2)
            {
                HomeCustomer home = new HomeCustomer(loginCustomer);
                home.Show();
                this.Close();
            }
        }

    }
}
