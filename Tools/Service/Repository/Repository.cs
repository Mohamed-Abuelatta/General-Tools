

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

        public EntityEntry<TEntityDTO> Remove(object id)
        {
            var model = _mapper.Map<TEntity>(GetById(id));
            EntityEntry<TEntityDTO> result = _mapper.Map<EntityEntry<TEntityDTO>>(_dbSet.Remove(model));
            return result;
        }

        public GridSetting GetGrid(int page = 0)
        {
            Grid grid = new Grid();
            IEntityType entityType = _context.Model.FindEntityType(_dbSet.EntityType.Name);
            Type t = entityType.ClrType;
            GridSetting gridSetting = (GridSetting)Attribute.GetCustomAttribute(t, typeof(GridSetting));
            return gridSetting;
        }

        public Grid InitGrid()
        {
            Grid grid = new Grid();
            grid.grid = GetGrid();
            grid.columns = getColumns();
            grid.rows = getRows();
            grid.Footer = getFooter();

            return grid;
        }

        public DataTable getRows(int page = 0)
        {
            Grid grid = new Grid();
            IEntityType entityType = _context.Model.FindEntityType(_dbSet.EntityType.Name);
            Type t = entityType.ClrType;
            GridSetting gridSetting = (GridSetting)Attribute.GetCustomAttribute(t, typeof(GridSetting));
            string jsonObj = _dbSet.Skip((page == 0 ? page : page - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).ToList().ToJson();

            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonObj);
            return dataTable;
        }
        public string getFooter(string nav, int page = 0)
        {
            IEntityType entityType = _context.Model.FindEntityType(_dbSet.EntityType.Name);
            Type t = entityType.ClrType;
            GridSetting gridSetting = (GridSetting)Attribute.GetCustomAttribute(t, typeof(GridSetting));

            if (nav == "prev")
            {

            }
            else if (nav == "next")
            {

            }
            else 
            {
                string jsonObj = _dbSet.Skip((page == 0 ? page : page - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).ToList().ToJson();
            }
            //string jsonObj = _dbSet.Skip((page == 0 ? page : page - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).ToList().ToJson();

            //int entitySize = _dbSet.Count();
            //int pageSize = gridSetting.ItemsPerPage;
            //int footerStart = page;
            //int footerRange = gridSetting.PagerSize;

            //Enumerable.Range(footerStart, footerRange).ToArray();

            // entitySize:100, pageSize:10, footerStart:76, footerRange:5
            // let footer = {isPrevDisabled: false, isNextDisabled: true, fRange: ['prev',1,2,3,4,5,'next']}
            return "";
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
