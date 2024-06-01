namespace ATCSDL
{
    public class Mes
    {
        public int idMes;
        public string contentMes, timeSendMes, loginSender, loginCustomer, loginSupplier;
        public bool statusMes;

        public Mes() {}
        public Mes(int idMes, string contentMes, string timeSendMes, string loginSender, string loginCustomer, string loginSupplier, bool statusMes)
        {
            this.idMes = idMes;
            this.contentMes = contentMes;
            this.timeSendMes = timeSendMes;
            this.loginSender = loginSender;
            this.loginCustomer = loginCustomer;
            this.loginSupplier = loginSupplier;
            this.statusMes = statusMes;
        }
    }
}