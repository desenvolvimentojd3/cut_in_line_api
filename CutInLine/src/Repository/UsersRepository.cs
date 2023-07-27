
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
            var sql = "";

            sql += "insert into Users (                             ";
            sql += "SName, SLogin, SHash, SAuthToken, SCellPhone,   ";
            sql += "SPassWord, SCountry, DDateCreated               ";
            sql += ")                                               ";
            sql += "values (                                        ";
            sql += "@SName,@SLogin,@SHash,@SAuthToken,@SCellPhone,  ";
            sql += "@SPassWord,@SCountry,@DDateCreated              ";
            sql += ") returning UserId;                             ";

            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, Item);
        }

        public async Task Delete(int Id, string token)
        {
            await _unitOfWork.Connection.ExecuteAsync("delete from Users WHERE idUsers=@id and token=@_token ",

            new { id = Id, _token = token });
        }

        public async Task Update(Users Item)
        {
            var sql = "";

            sql += "update Users set                                      ";
            sql += "SName=@SName, SLogin=@SLogin, SAuthToken=@SAuthToken, ";
            sql += "SCellPhone=@SCellPhone, SPassWord=@SPassWord,         ";
            sql += "SCountry=@SCountry, DDateCreated=@DDateCreated        ";
            sql += "where idUsers=@idUsers and token=@token               ";

            await _unitOfWork.Connection.ExecuteAsync(sql, Item);
        }

        public async Task<Users?> GetById(int id, string token)
        {
            var sql = "";

            sql += "select                           ";
            sql += "  Users.*                        ";
            sql += "from Users                       ";
            sql += "where Users.userId = @_idUsers   ";
            sql += "and   Users.token  = @_token     ";

            var resultado =
                await _unitOfWork
                    .Connection
                    .QueryAsync<Users>(sql, new { _id = id, _token = token });

            return resultado.FirstOrDefault();
        }
    }
}