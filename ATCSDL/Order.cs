using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ATCSDL
{
    public class Order
    {
        public int ID { get; set; }
        public int Price {  get; set; }
        public string Datetime { get; set; }
        public string address { get; set; }
        public string loginCustomer { get; set; }
        public int idPayment { get; set; }
        public int idTransport { get; set; }
        public string statusOrder { get; set; }
        public string loginSupplier { get; set; }

        public Order (int iD, int price, string datetime, string address, string loginCustomer, int idPayment, int idTransport, string statusOrder, string loginSupplier)
        {
            ID = iD;
            Price = price;
            Datetime = datetime;
            this.address = address;
            this.loginCustomer = loginCustomer;
            this.idPayment = idPayment;
            this.idTransport = idTransport;
            this.statusOrder = statusOrder;
            this.loginSupplier = loginSupplier;
        }
    }
}