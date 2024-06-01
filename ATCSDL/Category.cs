namespace ATCSDL
{
    public class Category
    {
        public int IDCategory { get; set; }
        public string NameCategory { get; set; }

        public Category(int IDCategory, string NameCategory)
        {
            this.IDCategory = IDCategory;
            this.NameCategory = NameCategory;
        }
    }
}