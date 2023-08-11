using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutInLine.Controllers;

[ApiController]
[Route("product")]
public class ProductsController : ControllerBase
{
    private readonly IProducts _product;

    public ProductsController(IProducts Product)
    {
        _product = Product;
    }

    [HttpPut]
    [Authorize]
    public async Task<dynamic> SignUp([FromHeader] string token, [FromBody] Products product) => await _product.Save(product, token);

    [HttpPost]
    [Route("get")]
    public async Task<dynamic> GetProducst([FromHeader] string token, [FromBody] SearchHelper search) => await _product.GetProducts(search, token);

    [HttpDelete]
    [Route("{id}")]
    public async Task<dynamic> GetProducst([FromHeader] string token, int id) => await _product.Delete(id, token);

    [HttpGet]
    [Route("{id}")]
    public async Task<dynamic> GetById([FromHeader] string token, int id) => await _product.GetById(id, token);
}
