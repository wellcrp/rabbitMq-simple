using RabbitMq.CrossCutting.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitMq.Domain.Interfaces.Products
{
    public interface IProduct
    {
        void PublishProduct(ProductDto product);
        void ConsumerProduct();
    }
}
