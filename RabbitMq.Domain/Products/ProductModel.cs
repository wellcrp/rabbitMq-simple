using System;

namespace RabbitMq.Domain.Product
{
    public class ProductModel
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int ProductQuantity { get; private set; }
        public decimal ProductPrice { get; private set; }
        public DateTime ProductExpiration { get; private set; }
    }
}
