using AutoMapper;
using Data.Contexts;
using NuGet.Protocol;
using Services.DataServices.Repository;
using Tools.Models;

namespace Tools.Service.ServiceData
{
    public class CustomerService : Repository<Customer, CustomerDTO>, ICustomerService
    {
        public CustomerService(AppDbContext db, IMapper mapper) : base(db, mapper) { }

        public string getRowsWithIncludes(int page = 0)
        {
            return IncludeMultiple(page, i => i.city, i => i.age);
        }
    }
}
