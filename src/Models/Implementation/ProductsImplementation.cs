using ApesWebPcp.Services;
using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using CutInLine.Repository;
using CutInLine.Services;

namespace CutInLine.Models.Implementation
{
    public class ProductsImplementation : IProducts
    {
        private readonly ProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsImplementation(ProductsRepository ProductsRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = ProductsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> Save(Products product, string token)
        {
            product.Token = token;

            if (product.ProductId == 0)
                product.ProductId = await _productsRepository.Create(product);
            else if (product.ProductId > 0)
                await _productsRepository.Update(product);

            return new { success = true };
        }

        public async Task<dynamic> GetById(int id, string token)
        {
            var product = await _productsRepository.GetById(id, token);

            return new { success = true, product };
        }

        public async Task<dynamic> Delete(int id, string token)
        {
            await _productsRepository.Delete(id, token);

            return new { success = true };
        }

        public async Task<dynamic> GetProducts(SearchHelper search, string token)
        {
            var where = Search.GetSearchString(search);

            var products = await _productsRepository.GetProducts(where, token);

            return new { success = true, products };
        }
    }
}