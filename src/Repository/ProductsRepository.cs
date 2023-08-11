
using CutInLine.Repository;
using Dapper;
using CutInLine.Models.Class;

namespace CutInLine.Repository
{
    public class ProductsRepository : Products
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(Products Item)
        {
            var sql = @"

            INSERT INTO Products (
                SDescription, 
                SDetail,
                SGroupName,
                NValue,
                Token,
                DDateCreated
            ) VALUES (
                @SDescription, 
                @SDetail,
                @SGroupName,
                @NValue,
                @Token,
                @DDateCreated

            ) RETURNING ProductId; ";

            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, Item);
        }

        public async Task Delete(int id, string token)
        {
            await _unitOfWork.Connection.ExecuteAsync("delete from Products WHERE userId=@_id and token=@_token ",

            new { _id = id, _token = token });
        }

        public async Task Update(Products Item)
        {
            var sql = @"

            UPDATE Products SET
                SDescription = @SDescription,
                SDetail = @SDetail,
                NValue = @NValue,               
                DDateCreated = @DDateCreated
            WHERE ProductId = @ProductId and Token=@Token; ";
            await _unitOfWork.Connection.ExecuteAsync(sql, Item);
        }

        public async Task<Products?> GetById(int id, string token)
        {
            var sql = @"

            select                                     
              Products.*
            from Products                               
            where Products.ProductId = @_id  
            and   Products.Token     = @_token
            ";

            var resultado = await _unitOfWork.Connection.QueryAsync<Products>
            (sql, new { _id = id, _token = token });

            return resultado.FirstOrDefault();
        }

        public async Task<List<Products>> GetProducts(string token, string where)
        {
            var sql = @$"
            select
            products.*
            from
            (
                select                                     
                Products.*
                from Products                               
                where Products.ProductId = @_id  
            ) Products
             where 1=1 {where}
            ";

            var resultado = await _unitOfWork.Connection.QueryAsync<Products>
            (sql, new { _token = token });

            return resultado.ToList();
        }
    }
}