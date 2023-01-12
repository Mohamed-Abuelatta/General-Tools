
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.UnitOfWork
{
    public interface IUnitOfWork
    {

        Task<string> CommitAsync();
        string Commit();
    }
}
