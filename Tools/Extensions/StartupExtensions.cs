using AutoMapper;
using Data.Contexts;
using NuGet.Protocol.Core.Types;
using Services.DataServices.Repository;
using System.Drawing;
using Tools.Service.ServiceData;
using Tools.Tools.Grid;

namespace Academy.Extensions
{
    public static class StartupExtensions
    {
        public static void AddMyContexts(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
        }

        public static void AddMyModels(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddTransient<Icons>(); // important to inject icons in any view
            services.AddScoped<ICustomerService, CustomerService>();
        }
        //----------------------------------------------------------------------

        public static void AddMyServices(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
        //---------------------------------------------------------------------------


    }
}

