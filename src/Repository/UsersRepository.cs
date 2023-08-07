
using CutInLine.Repository;
using Dapper;
using CutInLine.Models.Class;

namespace CutInLine.Repository
{
    public class UsersRepository : Users
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(Users Item)
        {
            var sql = @"

            INSERT INTO Users (
                SName, 
                SLogin,
                SHash,
                SAuthToken,
                SCellPhone,
                SPassWord,
                SCountry,
                DDateCreated
            ) VALUES (
                @SName, 
                @SLogin,
                @SHash,
                @SAuthToken,
                @SCellPhone,
                @SPassWord,
                @SCountry,
                @DDateCreated

            ) RETURNING userId; ";

            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, Item);
        }

        public async Task Delete(int Id, string token)
        {
            await _unitOfWork.Connection.ExecuteAsync("delete from Users WHERE userId=@id and token=@_token ",

            new { id = Id, _token = token });
        }

        public async Task Update(Users Item)
        {
            var sql = @"

            UPDATE Users SET
                SName = @SName,
                SLogin = @SLogin,
                SAuthToken = @SAuthToken,
                SCellPhone = @SCellPhone,
                SPassWord = @SPassWord,
                SCountry = @SCountry,
                DDateCreated = @DDateCreated
            WHERE userId = @userId and SHash=@SHash; ";
            await _unitOfWork.Connection.ExecuteAsync(sql, Item);
        }

        public async Task<Users?> GetById(int id)
        {
            var sql = @"

            select                                     
              Users.*
            from Users                               
            where Users.userId = @_id  ";

            var resultado = await _unitOfWork.Connection.QueryAsync<Users>
            (sql, new { _id = id });

            return resultado.FirstOrDefault();
        }

        public async Task<Users?> GetByLogin(string login)
        {
            var sql = @"

            select                                     
              Users.*
            from Users                               
            where Users.slogin = @_login ";

            var resultado = await _unitOfWork.Connection.QueryAsync<Users>
            (sql, new { _login = login });

            return resultado.FirstOrDefault();
        }
    }
}