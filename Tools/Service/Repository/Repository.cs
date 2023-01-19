

using AutoMapper;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Tools.Tools.CustomAttributes;
using static Tools.Tools.CustomAttributes.AttrEnum;
using Tools.Tools.Grid;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json.Linq;
using Tools.Service;

namespace Services.DataServices.Repository
{
    public class Repository<TEntity, TEntityDTO> : IRepository<TEntity, TEntityDTO> where TEntity : class where TEntityDTO : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public Repository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TEntityDTO> GetByIdAsync(object id)
        {
            try
            {
                var model = _mapper.Map<TEntityDTO>(await _dbSet.FindAsync(id));
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TEntityDTO GetById(object id)
        {
            try
            {
                var model = _mapper.Map<TEntityDTO>(_dbSet.Find(id));
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TEntityDTO>> GetAllAsync()
        {
            var model = _mapper.Map<IEnumerable<TEntityDTO>>(await _dbSet.ToListAsync());
            return model;

        }

        public IQueryable<TEntityDTO> GetAllAsQueryable()
        {
            var model = _mapper.Map<IQueryable<TEntityDTO>>(_dbSet.AsQueryable().AsNoTracking());
            return model;
        }

        public async Task<TEntityDTO> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var model = _mapper.Map<TEntityDTO>(await _dbSet.SingleOrDefaultAsync(predicate));
            return model;
        }

        public IQueryable<TEntityDTO> Where(Expression<Func<TEntity, bool>> expression)
        {
            var model = _mapper.Map<IQueryable<TEntityDTO>>(_dbSet.Where(expression).AsQueryable().AsNoTracking());
            return model;
        }

        public IQueryable<TEntityDTO> Include(Expression<Func<TEntity, bool>> expression)
        {
            var model = _mapper.Map<IQueryable<TEntityDTO>>(_dbSet.Include(expression).AsQueryable().AsNoTracking());
            return model;
        }

        public async Task<TEntityDTO> AddAsync(TEntityDTO entity)
        {
            TEntityDTO result = null;
            try
            {
                var model = _mapper.Map<TEntity>(entity);
                await _dbSet.AddAsync(model);
                await _context.SaveChangesAsync();
                result =_mapper.Map<TEntityDTO>(model);

                Dictionary<string, object> dicJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToJson());
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;
        }

        public async Task<IEnumerable<TEntityDTO>> AddRangeAsync(IEnumerable<TEntityDTO> entities)
        {
            var model = _mapper.Map<IEnumerable<TEntity>>(entities);
            await _dbSet.AddRangeAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<IEnumerable<TEntityDTO>>(model);
            return result;

        }

        public TEntityDTO Update(TEntityDTO entity)
        {
            try
            {
                TEntity result = _mapper.Map<TEntity>(entity);
                _dbSet.Update(result);
                _context.SaveChangesAsync();
                entity = _mapper.Map<TEntityDTO>(result);
            }
            catch
            {
                return entity;
            }
            return entity;
        }

        public ResponseResult Remove(object id, int page)
        {
            ResponseResult response = new ResponseResult();
            try
            {
                var model = _mapper.Map<TEntity>(GetById(id));
                var result = _dbSet.Remove(model);
                response.data = result != null ? getRows(page) : "#";
                response.isOk = true;
            }
            catch (Exception ex)
            {
                response.data = ex.Message + "<br />" + ex.InnerException.Message + "<br />" + ex.InnerException.Data;
                response.isOk = false;
            }
            return response;
        }

        public GridSetting GetGrid()
        {
            InitGrid grid = new InitGrid();
            IEntityType entityType = _context.Model.FindEntityType(_dbSet.EntityType.Name);
            Type t = entityType.ClrType;
            GridSetting gridSetting = (GridSetting)Attribute.GetCustomAttribute(t, typeof(GridSetting));
            return gridSetting;
        }

        public InitGrid InitGrid()
        {
            InitGrid grid = new InitGrid();
            grid.grid = GetGrid();
            grid.columns = getColumns();
            grid.rows = JsonConvert.DeserializeObject(getRows());
            grid.footer = getFooter();
            return grid;
        }

        // rows + footer header
        public string getRows(int page = 0)
        {
            GridSetting gridSetting = GetGrid();
            List<TEntity> rows = _dbSet.Skip((page == 0 ? page : page - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).ToList();
            string jsonRows = rows.ToJson();
            return jsonRows;
        }

        public Footer getFooter(int currentBtn = 1, string PageAction = "next")
        {
            GridSetting gridSetting = GetGrid();
            int tableCount = _dbSet.Count();
            decimal PagesCount = Math.Ceiling((decimal)((float)tableCount / gridSetting.ItemsPerPage));
            List<int> TableRange = Enumerable.Range(1, (int)PagesCount).ToList();
            List<int> PagerRange = new List<int>();
            if (PageAction == "next")
            {
                PagerRange = TableRange.Skip(currentBtn-1).Take(gridSetting.PagerSize).ToList();
            }
            else if (PageAction == "prev")
            {
                PagerRange = TableRange.Skip(currentBtn - gridSetting.PagerSize).Take(gridSetting.PagerSize).ToList();
            }

            //int lastPageRowsCount = _dbSet.Skip(((int)PagesCount - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).Count();

            Footer footer = new Footer {
                activeBtn = (PageAction == "next" ? PagerRange.Min() : currentBtn),
                isNextDisabled = TableRange.Max() == PagerRange.Max() ? "disabled" : "",
                isPrevDisabled = PagerRange.Min() == 1 ? "disabled" : "",
                fRange = PagerRange
            };
            return footer;
        }

        private List<ColumnSetting> getColumns()
        {
            List<ColumnSetting> columns = new List<ColumnSetting>();
            IEnumerable<IProperty> tableProperties = _dbSet.EntityType.GetProperties();

            foreach (var item in tableProperties)
            {
                PropertyInfo propInfo = item.PropertyInfo; 
                ColumnSetting column = (ColumnSetting)propInfo.GetCustomAttribute(typeof(ColumnSetting));
                column.ColName = propInfo.Name;
                column.KeyType = item.IsPrimaryKey() ? keyType.PK : item.IsForeignKey() ? keyType.FK : keyType.Normal;
                switch (propInfo.PropertyType.Name)
                {
                    case "string":
                        column.InputType = inputType.text;
                        break;
                    case "Int32":
                    case "Decimal":
                    case "Float":
                        column.InputType = inputType.number;
                        break;
                    case "DateTime":
                        column.InputType = inputType.date;
                        break;
                    case "Boolean":
                        column.InputType = inputType.checkbox;
                        break;
                    default:
                        column.InputType = inputType.text;
                        break;
                }
                columns.Add(column);
            }
            return columns;
        }

    }
}
