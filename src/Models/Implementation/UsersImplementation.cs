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

            if (_user != null && string.IsNullOrEmpty(user.SAuthToken))
            {
                return new
                {
                    success = true,
                    message = "O login informado já existe"
                };
            }

            if (string.IsNullOrEmpty(user.SAuthToken))
            {
                var password = Utils.CheckStrongPassword(user.SPassWord);

                if (!password.Success)
                {
                    return new
                    {
                        success = true,
                        message = password.Message
                    };
                }

                user.SPassWord = Encripty.GenerateSmallHash(user.SPassWord.ToUpper());
            }

            if (_user == null)
            {
                user.DDateCreated = DateTime.Now;
                user.SHash = Encripty.GenerateSmallHash(user.SLogin);

                user.UserId = await _usersRepository.Create(user);
            }

            var authorizeToken = TokenService.GerarToken(user);

            return new
            {
                success = true,
                message = "",
                authorizeToken,
                userToken = user.SHash
            };
        }

        public async Task<dynamic> SignIn(Users pUser)
        {
            var user = await _usersRepository.GetByLogin(pUser.SLogin.ToUpper());

            if (user == null)
                return new { message = "Usuário ou Senha inválidos", success = true };

            if (user.SPassWord != pUser.SPassWord)
                return new { message = "Usuário ou Senha inválidos", success = false };

            var authorizeToken = TokenService.GerarToken(user);

            return new
            {
                success = true,
                message = "",
                authorizeToken,
                userToken = user.SHash
            };
        }
    }
}