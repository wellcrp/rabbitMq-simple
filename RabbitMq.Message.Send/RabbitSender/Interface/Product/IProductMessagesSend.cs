using RabbitMq.CrossCutting.Product;

namespace RabbitMq.Message.Send.RabbitSender.Interface.Product
{
    public interface IProductMessagesSend
    {
        void ProductPublish(ProductDto product);
        void ProductConsumer();
    }
}
