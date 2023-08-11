using CutInLine.Models.Implementation;
using CutInLine.Models.Interface;
using CutInLine.Repository;

namespace CutInLine
{
    public static class InterfaceInjection
    {
        public static IServiceCollection AddInterfaceDepencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkImplementation>();
            services.AddScoped<IUsers, UsersImplementation>();
            services.AddScoped<IEvents, EventsImplementation>();
            services.AddScoped<IProducts, ProductsImplementation>();

            return services;
        }
    }
}