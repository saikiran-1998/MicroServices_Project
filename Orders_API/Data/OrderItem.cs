using System.ComponentModel.DataAnnotations;

namespace Orders_API.Data
{
    public class OrderItem
    {
        public int Id { get; set; }
        //[Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
