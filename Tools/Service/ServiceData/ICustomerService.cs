using Services.DataServices.Repository;
using Tools.Models;

namespace Tools.Service.ServiceData
{
    public interface ICustomerService : IRepository<Customer, CustomerDTO> {
        string getRowsWithIncludes(int page = 0);
    }
}
