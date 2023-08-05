using ApesWebPcp.Services;
using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using CutInLine.Repository;
using CutInLine.Services;

namespace CutInLine.Models.Implementation
{
    public class UsersImplementation : IUsers
    {
        private readonly UsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersImplementation(UsersRepository usersRepository, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> SignUp(Users user)
        {
            //Verify if exists same login
            var _user = await _usersRepository.GetByLogin(user.SLogin);

            if (_user != null)
            {
                return new
                {
                    success = true,
                    message = "O login informado já existe"
                };
            }

            var password = Utils.CheckStrongPassword(user.SPassWord);

            if (!password.Success)
            {
                return new
                {
                    success = true,
                    message = password.Message
                };
            }

            user.SHash = Encripty.GenerateSmallHash(user.SLogin);
            user.SPassWord = Encripty.GenerateSmallHash(user.SPassWord.ToUpper());

            _unitOfWork.Begin();

            await _usersRepository.Create(user);

            _unitOfWork.Commit();

            return new
            {
                success = true,
                message = ""
            };

        }
    }
}