using System;

namespace RabbitMq.Infrastructure.Crosscutting.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductExpiration { get; set; }
    }
}
