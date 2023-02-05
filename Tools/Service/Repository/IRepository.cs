
using System.Linq.Expressions;
using Tools.Service;
using Tools.Tools.CustomAttributes;
using Tools.Tools.Grid;

namespace Services.DataServices.Repository
{
    public interface IRepository<TEntity, TEntityDTO> where TEntity : class where TEntityDTO : class
    {
        Task<TEntityDTO> AddAsync(TEntityDTO entity);
        Task<IEnumerable<TEntityDTO>> AddRangeAsync(IEnumerable<TEntityDTO> entities);
        Task<IEnumerable<TEntityDTO>> GetAllAsync();
        IQueryable<TEntityDTO> GetAllAsQueryable();
        TEntityDTO GetById(object id);
        Task<TEntityDTO> GetByIdAsync(object id);
        IQueryable<TEntityDTO> Include(Expression<Func<TEntity, object>> expression);
        Task<TEntityDTO> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntityDTO Update(TEntityDTO entity);
        IQueryable<TEntityDTO> Where(Expression<Func<TEntity, bool>> expression);

        TEntityDTO Remove(object id, int page);
        // -------------------------------------------------------------------------------


        GridSetting GetGrid();
        InitGrid InitGrid();
        Footer getFooter(int firstBtn = 1, int activeBtn = 1);
        string getRows(int page);
    }
}
