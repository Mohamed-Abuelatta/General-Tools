using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext db;

        public UnitOfWork(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<string> CommitAsync()
        {
            try
            {
                if ((await db.SaveChangesAsync()) > 0)
                {
                    return "200";
                }
                else
                {
                    return "400";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "/" + ex.InnerException + "/" + ex.Source; 
            }
        }

        public string Commit()
        {
            try
            {
                if (db.SaveChanges() > 0)
                {
                    return "200";
                }
                else
                {
                    return "400";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "/" + ex.InnerException + "/" + ex.Source;
            }
        }
    }
}
