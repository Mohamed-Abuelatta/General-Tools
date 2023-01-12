using AutoMapper;
using Data.Contexts;
using Services.DataServices.Repository;
using Services.DataServices.UnitOfWork;
using Tools.Models;

namespace Tools.Service.ServiceData
{
    public class CustomerService : Repository<Customer, CustomerDTO>, ICustomerService
    {
        public CustomerService(AppDbContext db, IMapper mapper) : base(db, mapper) { }
    }
}
