

using AutoMapper;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Tools.Tools.Grid;
using Column = Tools.Tools.Grid.Column;
using InputType = Tools.Tools.Grid.inputType;
using Microsoft.EntityFrameworkCore.Metadata;
using Tools.Tools.CustomAttributes;

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
            catch (Exception ex)
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
            IEntityType entityType = _context.Model.FindEntityType(_dbSet.EntityType.Name);
            Type t = entityType.ClrType;
            GridSetting gridSetting = (GridSetting)Attribute.GetCustomAttribute(t, typeof(GridSetting));
            string JsonTable = _dbSet.Skip((page==0 ? page : page - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).ToList().ToJson();
            List<Column> cols = getColumns();
            var pk = cols.FirstOrDefault(pk => pk.KeyType == keyType.PK).ColName;
            //resultDT.ToList().ForEach(row => row["RowKey"] = ( "row" + row[pk] ));
            return gridSetting;
        }

        private List<Column> getColumns()
        {
            List<Column> columns = new List<Column>();

            var tableProperties = _dbSet.EntityType.GetProperties();
            foreach (var item in tableProperties)
            {
                var propInfo = item.PropertyInfo; InputType inputType;
                switch (propInfo.PropertyType.Name)
                {
                    case "string":
                        inputType = InputType.text;
                        break;
                    case "Int32":
                    case "Decimal":
                    case "Float":
                        inputType = InputType.number;
                        break;
                    case "DateTime":
                        inputType = InputType.date;
                        break;
                    case "Boolean":
                        inputType = InputType.checkbox;
                        break;
                    default:
                        inputType = InputType.text;
                        break;
                }
                string DisplayName = string.Empty;
                var attribute = propInfo.GetCustomAttribute<DisplayAttribute>();
                if (attribute != null) DisplayName = attribute.Name;

                columns.Add(
                    new Column
                    {
                        ColDName = DisplayName,
                        ColName = item.Name,
                        ColIndex = item.GetIndex(),
                        ColWidth = 200,
                        IsVisable = true,
                        InputType = inputType,
                        KeyType = item.IsPrimaryKey() ? keyType.PK : item.IsForeignKey() ? keyType.FK : keyType.Normal
                    });

            }
            return columns;
        }

    }
}
