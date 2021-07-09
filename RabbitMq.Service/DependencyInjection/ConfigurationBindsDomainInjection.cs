using Microsoft.Extensions.DependencyInjection;
using RabbitMq.Domain.Interfaces.Products;
using RabbitMq.Message.Send.RabbitSender.Interface.Product;
using RabbitMq.Message.Send.RabbitSender.Product;
using RabbitMq.Service.Product;

namespace RabbitMq.Service.DependencyInjection
{
    public class ConfigurationBindsDomainInjection
    {
        public static void RegisterBindings(IServiceCollection services)
        {
            services.AddSingleton<IProduct, ProductService>();
            services.AddSingleton<IProductMessagesSend, ProductPublisBus>();
        }
    }
}
