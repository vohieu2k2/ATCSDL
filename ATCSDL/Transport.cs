namespace ATCSDL
{
    public class Transport
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Transport(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}