using AutoMapper;
using Data.Contexts;
using Newtonsoft.Json.Serialization;
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

            services.AddScoped<ICustomerService, CustomerService>();
        }
        //----------------------------------------------------------------------

        public static void AddMyMapper(this IServiceCollection services)
        {
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
        //---------------------------------------------------------------------------
        
        //public static void AddMyJsonResolver(this IServiceCollection services)
        //{
        //    services.AddMvc().AddJsonOptions(opt =>
        //    {
        //        if (opt.SerializerSettings.ContractResolver != null)
        //        {
        //            var resolver = opt.SerializerSettings.ContractResolver as DefaultContractResolver;
        //            resolver.NamingStrategy = null;
        //        }
        //    });
        //}
        //---------------------------------------------------------------------------


    }
}

