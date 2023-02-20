
using AutoMapper;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Tools.Tools.CustomAttributes;
using static Tools.Tools.CustomAttributes.AttrEnum;
using Tools.Tools.Grid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using NuGet.Protocol;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Protocol.Core.Types;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.Common;
using Pal.Data;
using Tools.Models;

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
                EntityState es = _context.Entry(await _dbSet.FindAsync(id)).State = EntityState.Detached;
                var model = _mapper.Map<TEntityDTO>(es);
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
                TEntity model = _dbSet.Find(id);
                _context.Entry(model).State = EntityState.Detached;
                TEntityDTO result = _mapper.Map<TEntityDTO>(model);
                return result;
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


        public JObject getEnums(params Type[] enums)
        {
            var records = new JObject();
            foreach (var item in enums)
            {
                //var values = Enum.GetValues(item).Cast<int>();
                //var enumDictionary = values.ToDictionary(value => Enum.GetName(item, value));
                records.Add(item.Name, JsonConvert.SerializeObject(Enum.GetNames(item)));
            }
            return records;
        }

        public JObject getDDLs(params Type[] DDLs)
        {
            var records = new JObject();
            foreach (var item in DDLs)
            {
                IEntityType modelType = _context.Model.GetEntityTypes().FirstOrDefault(w => w.ClrType == item)!;
                var tableName = modelType.GetTableName();
                var sql = sqlCmdReadAsync(tableName).Result;
                records.Add(item.ShortDisplayName().ToLower(), JsonConvert.SerializeObject(sql));
            }
            return records;
        }

        public async Task<TEntityDTO> AddAsync(TEntityDTO entity)
        {
            TEntityDTO result = null;
            try
            {
                var model = _mapper.Map<TEntity>(entity);
                await _dbSet.AddAsync(model);
                await _context.SaveChangesAsync();
                result = _mapper.Map<TEntityDTO>(model);
                //Dictionary<string, object> dicJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToJson());
            }
            catch 
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
        public IEnumerable<TEntityDTO> Include(Expression<Func<TEntity, object>> expression)
        {
            var model = _dbSet.Include(expression).AsQueryable().AsNoTracking();
            return _mapper.Map<IEnumerable<TEntityDTO>>(model);
        }

        public string IncludeMultiple(int page = 0, params Expression<Func<TEntity, object>>[] includes)
        {
            GridSetting gs = GetGrid();
            IQueryable<TEntity> rows = _dbSet.Skip((page == 0 ? page : page - 1) * gs.ItemsPerPage).Take(gs.ItemsPerPage).AsQueryable();

            if (includes != null)
            { rows = includes.Aggregate(rows, (current, include) => current.Include(include)); }

            IEnumerable<TEntityDTO> rowsDTO = _mapper.Map<IEnumerable<TEntityDTO>>(rows);

            string result =
            JsonConvert.SerializeObject(rowsDTO, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return result;
        }

        public TEntityDTO Remove(object id, int page)
        {
            TEntityDTO result = null;
            try
            {
                TEntity model = _mapper.Map<TEntity>(GetById(id));
                var entry = _dbSet.Remove(model);
                _context.SaveChanges();
                result = _mapper.Map<TEntityDTO>(model);
            }
            catch
            {
                return result;
            }
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


        public GridSetting GetGrid()
        {
            InitGrid grid = new InitGrid();
            IEntityType entityType = _context.Model.FindEntityType(_dbSet.EntityType.Name);
            Type t = entityType.ClrType;
            GridSetting gridSetting = (GridSetting)Attribute.GetCustomAttribute(t, typeof(GridSetting));
            return gridSetting;
        }

        public IEnumerable<TEntityDTO> getRows(int page = 0)
        {
            GridSetting gridSetting = GetGrid();
            IQueryable<TEntity> rows = _dbSet.Skip((page == 0 ? page : page - 1) * gridSetting.ItemsPerPage).Take(gridSetting.ItemsPerPage).AsQueryable();
            return _mapper.Map<IEnumerable<TEntityDTO>>(rows);
        }

        // sooooooooooooooooo important 
        // https://www.entityframeworktutorial.net/
        // sooooooooooooooooo important 
        // https://www.entityframeworktutorial.net/EntityFramework4.3/raw-sql-query-in-entity-framework.aspx
        public async Task<DataTable> sqlCmdReadAsync(string tableName)
        {
            var connection = new SqlConnection(ConnectionStrings.AppConnectionString);
            string execSQL = string.Format($"select * from {tableName}");
            var command = new SqlCommand(execSQL, connection);
            DataTable DT = new DataTable();
            await connection.OpenAsync();
            using (SqlDataReader DR = await command.ExecuteReaderAsync())
            {
                DT.Load(DR);
            }
            await connection.CloseAsync();
            return DT;
        }
        
        public Footer getFooter(int firstBtn = 1, int activeBtn = 1)
        { 
            GridSetting gridSetting = GetGrid();
            int tableCount = _dbSet.Count();
            decimal PagesCount = Math.Ceiling((decimal)((float)tableCount / gridSetting.ItemsPerPage));
            List<int> TableRange = Enumerable.Range(firstBtn, (int)PagesCount).ToList();
            List<int> PagerRange = TableRange.Skip(firstBtn - 1).Take(gridSetting.PagerSize).ToList();

            Footer footer = new Footer {
                activeBtn = activeBtn,
                firstBtn = firstBtn,
                lastBtn = (firstBtn + PagerRange.Count()),
                isNextDisabled = TableRange.Max() == PagerRange.Max() ? "disabled" : "",
                isPrevDisabled = PagerRange.Min() == 1 ? "disabled" : "",
                prevBtn = firstBtn - gridSetting.PagerSize
            };
            return footer;
        }

        public List<ColumnSetting> getColumns(TEntityDTO entityDTO)
        {
            List<ColumnSetting> columns = new List<ColumnSetting>();
            IEnumerable<IProperty> tableProperties = _dbSet.EntityType.GetProperties();
            string PkName = tableProperties.FirstOrDefault(i => i.IsPrimaryKey()).Name;
            IEnumerable<string> FkNames = tableProperties.Where(i => i.IsForeignKey()).Select(s => s.Name);

            PropertyInfo[] propsInfos = entityDTO.GetType().GetProperties();

            foreach (var item in propsInfos)
            {
                ColumnSetting column = (ColumnSetting)item.GetCustomAttribute(typeof(ColumnSetting));
                column.ColName = item.Name;
                column.keyType = item.Name == PkName ? KeyType.PK : FkNames.Contains(item.Name) ? KeyType.FK : KeyType.Normal;
                column.isvisible = column.keyType == KeyType.PK ? false : true;
                columns.Add(column);
            }

            columns.Add(new ColumnSetting { ColName= "msg", keyType= KeyType.msg, isvisible = false });
            columns.Add(new ColumnSetting { ColName= "ctrl", keyType= KeyType.ctrl, isvisible = true });
            return columns;
        }

    }
}
