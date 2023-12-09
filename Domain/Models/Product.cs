namespace Psp.Pos.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; } //in cents
    }
}
