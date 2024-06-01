namespace ATCSDL
{
    public class Comment
    {
        public string content, date, loginCustomer;
        public int idProduct, score, idComment;

        public Comment(int idComment, string content, int score, int idProduct, string date, string loginCustomer)
        {
            this.idComment = idComment;
            this.content = content;
            this.score = score;
            this.idProduct = idProduct;
            this.date = date;
            this.loginCustomer = loginCustomer;
        }
    }
}