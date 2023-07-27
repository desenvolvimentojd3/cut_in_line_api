
using CutInLine.Models.Class;

namespace CutInLine.Models.Interface
{
    public interface IUsers
    {
        Task<dynamic> Delete(int id, string token);
        Task<dynamic> Save(Users user, string token);
        Task<dynamic> Authenticate(Users user, string token);
        Task<dynamic> GetById(int id, string token);
    }
}
