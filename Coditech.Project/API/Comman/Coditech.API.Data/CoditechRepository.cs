using Coditech.API.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Coditech.API.Data
{
    public class CoditechRepository<T> : ICoditechRepository<T> where T : class
    {
        #region Declarations

        private readonly CoditechDbContext _context;
        private static string CreatedDate = "CreatedDate";
        private static string CreatedBy = "CreatedBy";
        private static string ModifiedDate = "ModifiedDate";
        private static string ModifiedBy = "ModifiedBy";
        private DateTime CurrentDateTime = DateTime.Now;
        public virtual bool IsUserWantToDebugSql { get; set; } = HelperMethods.IsUserWantToDebugSql;

        #endregion

        #region Constructor
        public CoditechRepository()
        {
            //#if DEBUG
            //            if (IsUserWantToDebugSql)
            //                _context.GetDatabase().Log = s => Debug.WriteLine("Component: {0} Message: {1} ", "Entity  Repository", s);
            //#endif
        }

        public CoditechRepository(CoditechDbContext context)
        {
            _context = context;
            //#if DEBUG
            // if (IsUserWantToDebugSql)
            //     _context.GetDatabase().Log = s => Debug.WriteLine ("Component: {0} Message: {1} ", "Entity  Repository", s);
            //#endif
        }

        //Command timeout value, in seconds
        public CoditechRepository(int commandTimeout)
        {
            //_context = HelperMethods.CurrentContext;
            //_context.Database.SetCommandTimeout(commandTimeout);
            /*_context.GetDatabase().CommandTimeout = commandTimeout;*/
            //#if DEBUG
            //            if (IsUserWantToDebugSql)
            //                _context.GetDatabase().Log = s => Debug.WriteLine("Component: {0} Message: {1} ", "Entity  Repository", s);
            //#endif
        }
        #endregion

        #region Properties

        //Returns an IQueryable instance for access to entities of the given type in the context
        public virtual IQueryable<T> Table => this.Entities;

        //Returns a DbSet instance for access to entities of the given type in the context
        protected virtual Microsoft.EntityFrameworkCore.DbSet<T> Entities
        {
            get
            {
                return !Equals(_context, null) ? _context.Set<T>() : null;
            }
        }

        #endregion

        #region Public Methods

        #region GetById Entity

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <typeparam name="TId">is a generic struct (eg: int, guid etc.) input</typeparam>
        /// <param name="id">is a primary key value to return entity.</param>
        /// <returns>return entity</returns>
        public virtual T GetById<TId>(TId id) where TId : struct => Entities.Find(id);

        /// <summary>
        /// Get entity by identifier Asynchronously
        /// </summary>
        /// <typeparam name="TId">is a generic struct (eg: int, guid etc.) input</typeparam>
        /// <param name="id">is a primary key value to return entity.</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync<TId>(TId id) where TId : struct => await Entities.FindAsync(id);

        /// <summary>
        /// Get entity by identifier Asynchronously
        /// </summary>
        /// <param name="id">Is a Primary key value.</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        #endregion

        #region Insert Entity
        // Insert entity
        public virtual T Insert(T entity)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));
                int createdBy = Convert.ToInt32(entity.GetProperty("CreatedBy"));
                int modifiedBy = Convert.ToInt32(entity.GetProperty("ModifiedBy"));
                Entities.Add(entity);
                int _result = SaveChangesToDB(_context, HelperMethods.GetLoginUserId());
            }
            catch (Exception ex)
            {
                //Remove added entity from objects for all the entities tracked by context.
                Entities.Remove(entity);
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
            return entity;
        }

        /// <summary>
        /// This insert method used to insert data in database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="loginUserId">UserId of login user</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual T Insert(T entity, int loginUserId)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));
                int createdBy = Convert.ToInt32(entity.GetProperty("CreatedBy"));
                int modifiedBy = Convert.ToInt32(entity.GetProperty("ModifiedBy"));
                Entities.Add(entity);
                int _result = SaveChangesToDB(_context, loginUserId);
            }
            catch (Exception ex)
            {
                //Remove added entity from objects for all the entities tracked by context.
                Entities.Remove(entity);
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
            return entity;
        }

        /// <summary>
        /// Insert entity Asynchronously
        /// </summary>
        /// <param name="entity">Is a entity data model entity</param>
        /// <returns></returns>
        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Remove added entity from objects for all the entities tracked by context.
                Entities.Remove(entity);
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
            return entity;
        }

        // Insert entities       
        public virtual IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            try
            {
                if (Equals(entities, null))
                    throw new ArgumentNullException(nameof(entities));

                _context.Set<T>().AddRange(entities);
                SaveChangesToDB(_context, HelperMethods.GetLoginUserId());
                return entities;
            }
            catch (Exception ex)
            {
                //Remove added entities from objects for all the entities tracked by context.
                Entities.RemoveRange(entities);
                EntityLogging.LogObject(typeof(IEnumerable<T>), entities, ex);
                throw;
            }
        }

        /// <summary>
        /// Insert entities Asynchronously
        /// </summary>
        /// <param name="entities">Is a list of entity data model entity</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (Equals(entities, null))
                    throw new ArgumentNullException(nameof(entities));
                _context.Set<T>().AddRange(entities);
                await _context.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                //Remove added entities from objects for all the entities tracked by context.
                Entities.RemoveRange(entities);
                EntityLogging.LogObject(typeof(IEnumerable<T>), entities, ex);
                throw;
            }
        }

        #endregion

        #region Update Entity
        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity">Is a entity data model entity</param>
        /// <returns>Returns true or false</returns>
        public virtual bool Update(T entity)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                //Map the Modified property values to the original database entity. To update only those modified values.
                bool isEntityUpdate = UpdateEntity(entity);
                if (isEntityUpdate)
                {
                    var validationContext = new ValidationContext(entity);
                    Validator.ValidateObject(entity, validationContext);
                }
                bool _result = isEntityUpdate ? SaveChangesToDB(_context, HelperMethods.GetLoginUserId()) > 0 : true;
                return _result;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
        }

        /// <summary>
        /// This method use to update record in database with login User Id.
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="loginUserId">login User Id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual bool Update(T entity, int loginUserId)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                //Map the Modified property values to the original database entity. To update only those modified values.
                bool isEntityUpdate = UpdateEntity(entity);
                if (isEntityUpdate)
                {
                    var validationContext = new ValidationContext(entity);
                    Validator.ValidateObject(entity, validationContext);
                }
                bool _result = isEntityUpdate ? SaveChangesToDB(_context, loginUserId) > 0 : true;
                return _result;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
        }

        public virtual bool BatchUpdate(IEnumerable<T> entities)
        {
            try
            {
                if (Equals(entities, null))
                    throw new ArgumentNullException(nameof(entities));

                //Map the Modified property values to the original database entity. To update only those modified values.
                bool isEntityUpdate = UpdateEntities(entities);
                return isEntityUpdate ? SaveChangesToDB(_context, HelperMethods.GetLoginUserId()) > 0 : true;

            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), entities, ex);
                throw;
            }
        }

        /// <summary>
        /// Update entity Asynchronously
        /// </summary>
        /// <param name="entity">Is a entity data model entity</param>
        /// <returns>Returns true or false</returns>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                //Map the Modified property values to the original database entity. To update only those modified values.
                bool isEntityUpdate = UpdateEntity(entity);
                return isEntityUpdate ? await _context.SaveChangesAsync() > 0 : true;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
        }
        #endregion

        #region Delete Entity
        // Delete entity
        public virtual bool Delete(T entity)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(Entities.Find(GetObjectKey(entity)));
                bool _result = _context.SaveChanges() > 0;
                return _result;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
        }

        /// <summary>
        /// Delete entity Asynchronously
        /// </summary>
        /// <param name="entity">Is a entity data model entity</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                if (Equals(entity, null))
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(await _context.Set<T>().FindAsync(entity));
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), entity, ex);
                throw;
            }
        }

        // Delete entities
        public virtual bool Delete(IEnumerable<T> entities)
        {
            try
            {
                if (Equals(entities, null))
                    throw new ArgumentNullException(nameof(entities));

                foreach (var item in entities)
                {
                    Entities.Remove(item);
                }
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(IEnumerable<T>), entities, ex);
                throw;
            }
        }


        /// <summary>
        /// Delete entities Asynchronously
        /// </summary>
        /// <param name="entities">Is a list of entity data model entity</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (Equals(entities, null))
                    throw new ArgumentNullException(nameof(entities));

                foreach (var item in entities)
                {
                    Entities.Remove(await _context.Set<T>().FindAsync(item));
                }
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(IEnumerable<T>), entities, ex);
                throw;
            }
        }

        // Delete entity using filter condition
        public virtual bool Delete(string whereClause) => Delete(whereClause, null);

        // Delete entity using filter condition
        public virtual bool Delete(string whereClause, object[] values)
        {
            try
            {
                //TO DO : Replace 'Delete()' function with 'DeleteFromQuery' function.
                int count = (!Equals(values, null)) ? Table.Where(whereClause, values).DeleteFromQuery() : Table.Where(whereClause).DeleteFromQuery();
                return count > 0;
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(IEnumerable<T>), whereClause, ex);
                throw;
            }
        }

        #endregion

        #region Get Entity
        // Returns entity using filter condition
        public virtual T GetEntity(string where) => GetEntity(where, null, null);

        // Returns entity using filter condition
        public virtual T GetEntity(string where, object[] values) => GetEntity(where, null, values);

        // Returns entity with its specified navigation
        public virtual T GetEntity(List<string> navigationProperties) => GetEntity(string.Empty, navigationProperties, null);

        // Returns entity with its specified navigation as per filter passed
        public virtual T GetEntity(string where, List<string> navigationProperties) => GetEntity(where, navigationProperties, null);

        // Returns entity with its specified navigation as per filter passed
        public virtual T GetEntity(string where, List<string> navigationProperties, object[] values)
        {
            T item = null;
            try
            {
                IQueryable<T> dbQuery = Table;
                ////Apply eager loading
                if (!Equals(navigationProperties, null))
                {
                    // TODO: This is only a workaround, a permanent solution will be required for the same.
                    PropertyInfo[] properties = dbQuery.GetType().GenericTypeArguments[0].GetProperties();
                    foreach (string navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include(properties.FirstOrDefault(x => navigationProperty.Equals(x.PropertyType.Name, StringComparison.InvariantCultureIgnoreCase)
                            || navigationProperty.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.Name);
                }

                if (!string.IsNullOrEmpty(where))
                {
                    dbQuery = (!Equals(values, null))
                             ? dbQuery.AsNoTracking().Where(where, values)
                             : dbQuery.AsNoTracking().Where(where);
                }

                item = dbQuery.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(T), item, ex);
                throw;
            }
            return item;
        }

        #endregion

        #region Get Entity List
        // Returns list of entities using filter condition
        public virtual IList<T> GetEntityList(string where) => GetEntityList(where, string.Empty, null, null);

        // Returns sorted list of entities using order by condition
        public virtual IList<T> GetEntityListByOrder(string orderBy) => GetEntityList(string.Empty, orderBy, null, null);

        // Returns list of entities using filter condition
        public virtual IList<T> GetEntityList(string where, object[] values) => GetEntityList(where, string.Empty, null, values);

        // Returns list of entities with its specified navigation
        public virtual IList<T> GetEntityList(List<string> navigationProperties) => GetEntityList(string.Empty, string.Empty, navigationProperties, null);

        // Returns sorted list of entities using order by condition as per filter passed
        public virtual IList<T> GetEntityList(string where, string orderBy) => GetEntityList(where, orderBy, null, null);

        // Returns list of entities with its specified navigation as per filter passed
        public virtual IList<T> GetEntityList(string where, List<string> navigationProperties) => GetEntityList(where, string.Empty, navigationProperties, null);

        // Returns sorted list of entities using order by condition as per filter passed
        public virtual IList<T> GetEntityList(string where, string orderBy, object[] values) => GetEntityList(where, orderBy, values);

        // Returns list of entities with its specified navigation as per filter passed
        public virtual IList<T> GetEntityList(string where, List<string> navigationProperties, object[] values) => GetEntityList(where, string.Empty, navigationProperties, values);

        // Returns sorted list of entities along with its specified navigation
        public virtual IList<T> GetEntityListByOrder(string orderBy, List<string> navigationProperties) => GetEntityList(string.Empty, orderBy, navigationProperties, null);

        // Returns sorted list of entities along with its specified navigation as per filter passed
        public virtual IList<T> GetEntityList(string where, string orderBy, List<string> navigationProperties, object[] values)
        {

            List<T> list = null;
            try
            {
                IQueryable<T> dbQuery = Table;

                if (string.IsNullOrEmpty(orderBy))
                {
                    var orderByValue = GetEntityPrimaryKey<T>();
                    orderBy = String.IsNullOrEmpty(orderByValue) ? "" : string.Format("{0} asc", orderByValue);
                }
                ////Apply eager loading
                if (!Equals(navigationProperties, null))
                {
                    PropertyInfo[] properties = dbQuery.GetType().GenericTypeArguments[0].GetProperties();
                    foreach (string navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include(properties.FirstOrDefault(x => navigationProperty.Equals(x.PropertyType.Name, StringComparison.InvariantCultureIgnoreCase)
                            || navigationProperty.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.Name);

                }
                if (!string.IsNullOrEmpty(where))
                {
                    dbQuery = (!Equals(values, null))
                        ? (string.IsNullOrEmpty(orderBy))
                        ? dbQuery.AsNoTracking().Where(where, values)
                        : dbQuery.AsNoTracking().Where(where, values).OrderBy(orderBy)
                    : (string.IsNullOrEmpty(orderBy))
                       ? dbQuery.AsNoTracking().Where(where)
                       : dbQuery.AsNoTracking().Where(where).OrderBy(orderBy);
                }
                else
                {
                    dbQuery = (string.IsNullOrEmpty(orderBy))
                        ? dbQuery
                        : dbQuery.AsNoTracking().OrderBy(orderBy);
                }
                list = dbQuery.AsNoTracking().ToList<T>();
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(IList<T>), list, ex);
                throw;
            }
            return list;
        }

        #endregion

        #region Get Entity List with Paging
        // Returns sorted paged list of entities
        public virtual IList<T> GetPagedListByOrder(string orderBy, int pageIndex, int pageSize, out int totalCount) => GetPagedList(string.Empty, orderBy, null, null, pageIndex, pageSize, out totalCount);

        // Returns paged list of entities as per filter passed
        public virtual IList<T> GetPagedList(string where, string orderBy, int pageIndex, int pageSize, out int totalCount) => GetPagedList(where, orderBy, null, null, pageIndex, pageSize, out totalCount);

        // Returns paged list of entities as per filter passed
        public virtual IList<T> GetPagedList(string where, string orderBy, object[] values, int pageIndex, int pageSize, out int totalCount) => GetPagedList(where, orderBy, null, values, pageIndex, pageSize, out totalCount);

        // Returns paged list of entities along with its specified navigation
        public virtual IList<T> GetPagedList(string orderBy, List<string> navigationProperties, int pageIndex, int pageSize, out int totalCount) => GetPagedList(string.Empty, orderBy, navigationProperties, null, pageIndex, pageSize, out totalCount);

        // Returns sorted paged list of entities along with its specified navigation as per filter passed
        public virtual IList<T> GetPagedList(string where, string orderBy, List<string> navigationProperties, object[] values, int pageIndex, int pageSize, out int totalCount)
        {
            List<T> list = null;
            try
            {
                IQueryable<T> dbQuery = Table;

                if (string.IsNullOrEmpty(orderBy))
                {
                    var orderByValue = GetEntityPrimaryKey<T>();
                    orderBy = String.IsNullOrEmpty(orderByValue) ? "" : string.Format("{0} desc", orderByValue);
                }

                if (!Equals(navigationProperties, null))
                {
                    PropertyInfo[] properties = dbQuery.GetType().GenericTypeArguments[0].GetProperties();
                    foreach (string navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include(properties.FirstOrDefault(x => navigationProperty.Equals(x.PropertyType.Name, StringComparison.InvariantCultureIgnoreCase)
                            || navigationProperty.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.Name);

                }

                if (!string.IsNullOrEmpty(where))
                {
                    dbQuery = (!Equals(values, null))
                        ? (string.IsNullOrEmpty(orderBy))
                        ? dbQuery.AsNoTracking().Where(where, values)
                        : dbQuery.AsNoTracking().Where(where, values).OrderBy(orderBy)
                    : (string.IsNullOrEmpty(orderBy))
                       ? dbQuery.AsNoTracking().Where(where)
                       : dbQuery.AsNoTracking().Where(where).OrderBy(orderBy);
                }
                else
                {
                    dbQuery = (!string.IsNullOrEmpty(orderBy))
                        ? dbQuery.AsNoTracking().OrderBy(orderBy)
                        : dbQuery;
                }

                list = new PagedList<T>(dbQuery, pageIndex, pageSize, out totalCount);
            }
            catch (Exception ex)
            {
                totalCount = 0;
                EntityLogging.LogObject(typeof(T), list, ex);
                throw;
            }
            return list;
        }
        #endregion

        #region Get Unordered Entity List

        //Get unordered list of entity using filter condition & will not have default order by clause in the query execution.
        public virtual IList<T> GetEntityListWithoutOrderBy(string where) => GetEntityListWithoutOrderBy(where, null, null);

        //Get unordered list of entity using filter condition, filter value & will not have default order by clause in the query execution.
        public virtual IList<T> GetEntityListWithoutOrderBy(string where, object[] values) => GetEntityListWithoutOrderBy(where, null, values);

        //Get unordered list of entity and specified navigation entities & will not have default order by clause in the query execution.
        public virtual IList<T> GetEntityListWithoutOrderBy(string where, List<string> navigationProperties) => GetEntityListWithoutOrderBy(where, navigationProperties, null);

        //Get unordered list of entity along with its specified navigation as per filter passed & will not have default order by clause in the query execution.
        public virtual IList<T> GetEntityListWithoutOrderBy(string where, List<string> navigationProperties, object[] values)
        {
            List<T> list = new List<T>();
            try
            {
                IQueryable<T> dbQuery = Table;

                //Apply eager loading
                if (!Equals(navigationProperties, null))
                {
                    PropertyInfo[] properties = dbQuery.GetType().GenericTypeArguments[0].GetProperties();
                    foreach (string navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include(properties.FirstOrDefault(x => navigationProperty.Equals(x.PropertyType.Name, StringComparison.InvariantCultureIgnoreCase)
                            || navigationProperty.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.Name);

                }

                //Apply filter condition
                if (!string.IsNullOrEmpty(where))
                {
                    dbQuery = (!Equals(values, null))
                        ? dbQuery.AsNoTracking().Where(where, values)
                        : dbQuery.AsNoTracking().Where(where);
                }
                else
                {
                    dbQuery = dbQuery;
                }

                list = dbQuery.AsNoTracking().ToList<T>();
            }
            catch (Exception ex)
            {
                EntityLogging.LogObject(typeof(IList<T>), list, ex);
                throw;
            }
            return list;
        }

        #endregion

        #endregion

        #region Private Methods

        //Get the Primary Key Name.
        private string GetEntityPrimaryKey<TEntity>() where TEntity : class
        {
            if (_context.Model.FindEntityType(typeof(T)).FindPrimaryKey() != null)
                return _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).FirstOrDefault().ToString();

            else return "";
        }

        // ToDo GetEntitySet method is removed because the EntitySet is not available in .net core framework.
        //private EntitySet GetEntitySet<TEntity>() where TEntity : class
        //{
        //    EntitySet entitySet = null;

        //    try
        //    {
        //        entitySet = (_context as IObjectContextAdapter).ObjectContext.CreateObjectSet<TEntity>().EntitySet;
        //    }
        //    catch (Exception ex)
        //    {
        //        EntityLogging.LogObject(typeof(TEntity), entitySet, ex);
        //        throw;
        //    }
        //    return entitySet;
        //}

        //Get the Primary Key Values.
        private object GetObjectKey(T entity)
        => typeof(T).GetProperty(GetEntityPrimaryKey<T>()).GetValue(entity, null);

        //Set Modified Values for the Entity, to update only modified values based on the original database values.
        private bool UpdateEntity(T entity)
        {
            bool isEntityUpdate = false;
            //Get the Primary Key value for the Entity.
            var objKey = GetObjectKey(entity);

            //Get the Entity Data based on the primary key. If given entity already exists in the context, then data return from the context without making the db call.
            var originalEntity = Entities.Find(objKey);
            var originalEntityEntry = _context.Entry<T>(originalEntity);

            //Get the Database values for the Entity. In case the Entity return data from context cache, then updated values will not found in the entity.
            var databaseEntity = originalEntityEntry.GetDatabaseValues();

            foreach (var property in originalEntityEntry.OriginalValues.Properties)
            {
                var original = databaseEntity.GetValue<object>(property);
                var current = entity.GetProperty(property.Name);
                if (!Equals(original, current))
                {
                    originalEntityEntry.Property(property.Name).CurrentValue = current;
                    originalEntityEntry.Property(property.Name).IsModified = true;
                    isEntityUpdate = true;
                }
            }
            return isEntityUpdate;
        }

        //Set Modified Values for the Entity, to update only modified values based on the original database values.
        private bool UpdateEntities(IEnumerable<T> entities)
        {
            bool isEntityUpdate = false;
            //Get the Primary Key value for the Entity.
            foreach (T entity in entities)
            {
                var objKey = GetObjectKey(entity);

                //Get the Entity Data based on the primary key. If given entity already exists in the context, then data return from the context without making the db call.
                var originalEntity = Entities.Find(objKey);
                var originalEntityEntry = _context.Entry<T>(originalEntity);

                //Get the Database values for the Entity. In case the Entity return data from context cache, then updated values will not found in the entity.
                var databaseEntity = originalEntityEntry.GetDatabaseValues();

                foreach (var property in originalEntityEntry.OriginalValues.Properties)
                {
                    var original = databaseEntity.GetValue<object>(property);
                    var current = entity.GetProperty(property.Name);
                    if (!Equals(original, current))
                    {
                        originalEntityEntry.Property(property.Name).CurrentValue = current;
                        originalEntityEntry.Property(property.Name).IsModified = true;
                        isEntityUpdate = true;
                    }
                }
            }
            return isEntityUpdate;
        }

        //Override Method to Insert/Update the Created/Modified Date for the Entity.
        public int SaveChangesToDB(CoditechDbContext _context, int loginUserAccountId, int createdBy = 0, int modifiedBy = 0)
        {
            try
            {
                foreach (var ent in _context.ChangeTracker.Entries().Where(p => Equals(p.State, EntityState.Added) || Equals(p.State, EntityState.Deleted) || Equals(p.State, EntityState.Modified)))
                {
                    SetDataIntoEntity(ent, loginUserAccountId, createdBy, modifiedBy);
                }
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void SetDataIntoEntity(EntityEntry dbEntry, int loginUserAccountId, int createdBy = 0, int modifiedBy = 0)
        {
            if (Equals(dbEntry.State, EntityState.Added))
            {
                dbEntry.Entity.SetPropertyValue(CreatedBy, (createdBy > 0) ? createdBy : loginUserAccountId);
                dbEntry.Entity.SetPropertyValue(ModifiedBy, (modifiedBy > 0) ? modifiedBy : loginUserAccountId);

                dbEntry.Entity.SetPropertyValue(CreatedDate, CurrentDateTime);
                dbEntry.Entity.SetPropertyValue(ModifiedDate, CurrentDateTime);
            }
            else if (Equals(dbEntry.State, EntityState.Modified))
            {
                foreach (var property in dbEntry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                {
                    if (Equals(property.Name, CreatedDate))
                    {
                        var CreatedDateTime = dbEntry.OriginalValues["CreatedDate"];
                        dbEntry.Entity.SetPropertyValue(CreatedDate, CreatedDateTime);
                    }
                    if (Equals(property.Name, CreatedBy))
                    {
                        var CreatedByUser = dbEntry.OriginalValues["CreatedBy"];
                        dbEntry.Entity.SetPropertyValue(CreatedBy, CreatedByUser);
                    }
                }
            }

            dbEntry.Entity.SetPropertyValue(ModifiedBy, modifiedBy > 0 ? modifiedBy : loginUserAccountId);
            dbEntry.Entity.SetPropertyValue(ModifiedDate, CurrentDateTime);
        }

        //Set the Entity Exception message.
        //ToDo SetEntityExceptionMessage method is commented because DbEntityValidationException is not available in the .net core framework.
        //private static string SetEntityExceptionMessage(System.Data.Entity.Validation.DbEntityValidationException e)
        //{
        //    string rs = string.Empty;
        //    foreach (var eve in e.EntityValidationErrors)
        //    {
        //        rs = $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";

        //        foreach (var ve in eve.ValidationErrors)
        //        {
        //            rs = string.Concat(rs, "<br />" + $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
        //        }
        //    }

        //    return rs;
        //}

        #endregion
    }

}