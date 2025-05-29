using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.DataAccess.UnitOfWork.Concrete;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.UnitOfWork.Abstract;
using BookStore.Business.Abstract.GenericServiceInterface;
using BookStore.Business.Concrete.GenericService;

namespace BookStore.Business.IOC
{
    public static class CustomIocExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DataContext>(opt => opt.UseOracle(configuration.GetConnectionString("database"), x =>
            {
                x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
                x.MigrationsAssembly("BookStore.DataAccess");
            }));


            services.AddScoped<IUnitOfWorkBookStore, UnitOfWorkBookStore>();
            services.AddScoped<IGenericServiceBookStore, GenericServiceBookStore>();
        }
    }
}
