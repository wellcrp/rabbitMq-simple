using RabbitMq.CrossCutting.Product;
using RabbitMq.Domain.Interfaces.Products;
using RabbitMq.Message.Send.RabbitSender.Interface.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitMq.Service.Product
{
    public class ProductService : IProduct
    {
        private readonly IProductMessagesSend _productMessage;
        public ProductService(IProductMessagesSend productMessage)
        {
            _productMessage = productMessage;
        }

        public void PublishProduct(ProductDto product)
        {
            _productMessage.ProductPublish(product);
        }

        public void ConsumerProduct()
        {
            _productMessage.ProductConsumer();
        }
    }
}
