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

namespace ATCSDL
{
    public partial class EditComment : Form
    {
        public Comment comment;
        public string connectionString = "Data Source = ACER\\VTH;Initial Catalog = ShoppingOnlineProduct; Integrated Security = True;";
        public EditComment(Comment comment)
        {
            InitializeComponent();
            this.comment = comment;
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
                    // Thiết lập câu lệnh SQL UPDATE
                    string updateCommand = "UPDATE Comment SET ContentComment = @ContentComment, ScoreComment = @ScoreComment, DateLastUpdate = @DateLastUpdate WHERE IDComment = @IDComment";

                    // Tạo và mở kết nối đến cơ sở dữ liệu
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Tạo và thực thi đối tượng SqlCommand
                        using (SqlCommand command = new SqlCommand(updateCommand, connection))
                        {
                            // Thay đổi các giá trị parameter của câu lệnh
                            command.Parameters.AddWithValue("@ContentComment", commentTxt.Text);
                            command.Parameters.AddWithValue("@ScoreComment", number); // Giả sử điểm số mới là 5
                            command.Parameters.AddWithValue("@DateLastUpdate", DateTime.Now.Date);
                            command.Parameters.AddWithValue("@IDComment", comment.idComment); // ID của comment cần cập nhật

                            // Thực thi câu lệnh UPDATE
                            int rowsAffected = command.ExecuteNonQuery();
                            // Kiểm tra xem dữ liệu đã được chèn thành công hay không
                            if (rowsAffected > 0)
                            {
                                AverageScoreProduct();
                                MessageBox.Show("Sửa đánh giá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Đưa qua form khác
                                ShowComment showComment = new ShowComment(comment.loginCustomer, comment.idProduct);
                                showComment.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Xảy ra lỗi, vui lòng sửa lại đánh giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void scoreTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là phím điều khiển (như Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này vào TextBox
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Tạo câu lệnh SQL DELETE
            string deleteQuery = "DELETE FROM Comment WHERE IDComment = @IDComment";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo và thi hành Command
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    // Thêm tham số cho IDComment
                    command.Parameters.AddWithValue("@IDComment", comment.idComment);

                    // Thực hiện truy vấn DELETE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có hàng nào bị ảnh hưởng không
                    if (rowsAffected > 0)
                    {
                        AverageScoreProduct();
                        MessageBox.Show("Xóa đánh giá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Đưa qua form khác
                        ShowComment showComment = new ShowComment(comment.loginCustomer, comment.idProduct);
                        showComment.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xảy ra lỗi, vui lòng xóa lại đánh giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
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
                    command.Parameters.AddWithValue("@IDProduct", comment.idProduct);

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
                    command.Parameters.AddWithValue("@IDProduct", comment.idProduct);

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

        private void EditComment_Load(object sender, EventArgs e)
        {
            commentTxt.Text = comment.content;
            scoreTxt.Text = comment.score.ToString();
        }

        private void commentTxt_TextChanged(object sender, EventArgs e)
        {
            if (commentTxt.Text.Length > 150)
            {
                commentTxt.Text = commentTxt.Text.Substring(0, 150);
                commentTxt.SelectionStart = 150;
                commentTxt.SelectionLength = 0;
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            ShowComment show = new ShowComment(comment.loginCustomer, comment.idProduct);
            show.Show();
            this.Close();
        }
    }
}
