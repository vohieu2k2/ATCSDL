using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATCSDL
{
    public partial class AddComment : Form
    {
        public string loginCustomer;
        public int idProduct = 0;
        public string connectionString = "Data Source=ACER;Initial Catalog=SQLShoppingOnline;Integrated Security=True;";

        public AddComment(string loginCustomer, int idProduct)
        {
            InitializeComponent();
            this.loginCustomer = loginCustomer;
            this.idProduct = idProduct;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là phím điều khiển (như Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này vào TextBox
            }
        }

        private void changeInfoBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(commentTxt.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung bình luận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thử chuyển đổi chuỗi thành số nguyên
            if (int.TryParse(scoreTxt.Text, out int number))
            {
                // Kiểm tra xem số có nằm trong khoảng từ 1 đến 10 hay không
                if (number >= 1 && number <= 10)
                {
                    // Chuỗi truy vấn SQL để chèn dữ liệu vào bảng Comment
                    string query = "INSERT INTO Comment (ContentComment, ScoreComment, DateLastUpdate, LoginCustomer, IDProduct) VALUES (@ContentComment, @ScoreComment, @DateLastUpdate, @LoginCustomer, @IDProduct)";

                    // Tạo kết nối đến cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Mở kết nối
                        connection.Open();

                        // Tạo và thực thi một SqlCommand để chèn dữ liệu
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Thêm các tham số vào câu lệnh SQL
                            command.Parameters.AddWithValue("@ContentComment", commentTxt.Text);
                            command.Parameters.AddWithValue("@ScoreComment", number);
                            command.Parameters.AddWithValue("@DateLastUpdate", DateTime.Now.Date);
                            command.Parameters.AddWithValue("@LoginCustomer", loginCustomer);
                            command.Parameters.AddWithValue("@IDProduct", idProduct);

                            // Thực thi truy vấn
                            int rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra xem dữ liệu đã được chèn thành công hay không
                            if (rowsAffected > 0)
                            {
                                AverageScoreProduct();
                                MessageBox.Show("Thêm đánh giá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Đưa qua form khác
                                ShowComment showComment = new ShowComment(loginCustomer, idProduct);
                                showComment.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Xảy ra lỗi, vui lòng đánh giá lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập lại điểm đánh giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập lại điểm đánh giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            // Đưa qua form khác
            ShowComment showComment = new ShowComment(loginCustomer, idProduct);
            showComment.Show();
            this.Close();
        }

        private void AverageScoreProduct()
        {
            // Tạo câu lệnh SQL SELECT để lấy các giá trị ScoreComment của các Comment có IDProduct tương ứng
            string selectQuery = "SELECT ScoreComment FROM Comment WHERE IDProduct = @IDProduct";
            double averageScore = 0;

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thi hành Command
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    // Thêm tham số cho IDProduct
                    command.Parameters.AddWithValue("@IDProduct", idProduct);

                    // Sử dụng ExecuteReader để lấy các giá trị ScoreComment từ kết quả truy vấn
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int totalScore = 0;
                        int commentCount = 0;

                        // Đọc từng dòng kết quả và tính tổng điểm và số lượng comment
                        while (reader.Read())
                        {
                            int score = Convert.ToInt32(reader["ScoreComment"]);
                            totalScore += score;
                            commentCount++;
                        }

                        // Kiểm tra xem có comment nào không trước khi tính điểm trung bình
                        if (commentCount > 0)
                        {
                            // Tính điểm trung bình
                            averageScore = Math.Round((double)totalScore / commentCount, 2);
                        }
                    }
                }
            }

            // Tạo câu lệnh SQL UPDATE để cập nhật giá trị của trường AverageScore
            string updateQuery = "UPDATE Product SET AverageScoreProduct = @NewAverageScore WHERE IDProduct = @IDProduct";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thi hành Command
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Thêm tham số cho NewAverageScore và IDProduct
                    command.Parameters.AddWithValue("@NewAverageScore", averageScore);
                    command.Parameters.AddWithValue("@IDProduct", idProduct);

                    // Thực hiện truy vấn UPDATE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có hàng nào bị ảnh hưởng không
                    if (rowsAffected > 0)
                    {
                    }
                    else
                    {
                        MessageBox.Show("Xảy ra lỗi, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
    }
}
