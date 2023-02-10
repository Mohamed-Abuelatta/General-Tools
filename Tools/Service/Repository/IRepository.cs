
using System.Linq.Expressions;
using Tools.Service;
using Tools.Tools.CustomAttributes;
using Tools.Tools.Grid;

namespace Services.DataServices.Repository
{
    public interface IRepository<TEntity, TEntityDTO> where TEntity : class where TEntityDTO : class
    {
        Task<TEntityDTO> GetByIdAsync(object id);
        Task<TEntityDTO> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntityDTO> Where(Expression<Func<TEntity, bool>> expression);

        TEntityDTO GetById(object id);
        IQueryable<TEntityDTO> GetAllAsQueryable();
        Task<IEnumerable<TEntityDTO>> GetAllAsync();

        Task<IEnumerable<TEntityDTO>> AddRangeAsync(IEnumerable<TEntityDTO> entities);
        Task<TEntityDTO> AddAsync(TEntityDTO entity);
        TEntityDTO Update(TEntityDTO entity);
        TEntityDTO Remove(object id, int page);
        // -------------------------------------------------------------------------------

        GridSetting GetGrid();
        InitGrid InitGrid(TEntityDTO entityDTO, object rows);
        Footer getFooter(int firstBtn = 1, int activeBtn = 1);

        IEnumerable<TEntityDTO> getRows(int page);
        IEnumerable<TEntityDTO> Include(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntityDTO> IncludeMultiple(int page = 0, params Expression<Func<TEntity, object>>[] includes);
    }
}
