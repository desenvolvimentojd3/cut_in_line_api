
using CutInLine.Repository;

namespace CutInLine
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositoryDepencyInjection(this IServiceCollection services)
        {
            //Injeção de dependencia dos repositorios
            services.AddScoped<UsersRepository>();

            return services;
        }
    }
}