
using CutInLine.Models.Class;

namespace CutInLine.Models.Interface
{
    public interface IUsers
    {
        Task<dynamic> SignUp(Users user);
        Task<dynamic> SignIn(Users pUser);
    }
}
