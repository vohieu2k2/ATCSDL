using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ATCSDL
{
    public class ProductInOrder
    {
        public int ID { get; set; }
        public int IDProduct { get; set; }
        public int IDOrder { get; set; }
        public int NumberProductOrder { get; set; }
        public int PriceProductOrder { get; set; }

        public ProductInOrder(int Id, int IdProduct, int IdOrder, int Number, int Price)
        {
            this.ID = Id;
            this.IDProduct = IdProduct;
            this.IDOrder = IdOrder;
            this.NumberProductOrder = Number;
            this.PriceProductOrder = Price;
        }
    }
}