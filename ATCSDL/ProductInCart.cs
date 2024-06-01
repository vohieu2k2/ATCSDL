namespace ATCSDL
{
    public class ProductInCart
    {
        public int IDProduct {  get; set; } 
        public int NumberInCart { get; set;}
        public string Name { get; set; }
        public int Price { get; set; }
        public byte[] image { get; set; }
        public string loginSupplier { get; set; }
        public ProductInCart(int iDProduct, int numberInCart, string name, int price, byte[] image, string loginSupplier)
        {
            IDProduct = iDProduct;
            NumberInCart = numberInCart;
            Name = name;
            Price = price;
            this.image = image;
            this.loginSupplier = loginSupplier;
        }
    }
}