using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMq.CrossCutting.Product;
using RabbitMq.Domain.Interfaces.Products;
using System.Threading.Tasks;

namespace RabbitMq.Application.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        /// <summary>
        /// EndPoint para publicar produto.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorno sucesso de produto</response>
        /// <response code="400">retorno error de publicar o produto na fila</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult Product(ProductDto product)
        {
            _product.PublishProduct(product);

            return Ok();
        }

        /// <summary>
        /// EndPoint para consumir o produto no rabbitMq.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorno sucesso de produto</response>
        /// <response code="400">retorno error de publicar o produto na fila</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> AllProduct()
        {
            _product.ConsumerProduct();

            return Ok();
        }
    }
}
