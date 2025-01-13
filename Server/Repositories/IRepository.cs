using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Server.Database.Contracts;
using Server.Interfaces.Database;
using System.Linq.Expressions;

namespace Server.Repositories
{
    public interface IRepository
    {
        IAppDbContext Context { get; set; }
        Task AddAsync<T>(T entity) where T : class, IAuditableEntity;
        Task<T?> GetByIdAsync<T>(Guid Id) where T : class, IAuditableEntity;
        Task RemoveAsync<T>(T entity) where T : class, IAuditableEntity;
        Task RemoveRangeAsync<T>(List<T> entities) where T : class, IAuditableEntity;
        Task UpdateAsync<T>(T entity) where T : class, IAuditableEntity;
        Task<T?> GetAsync<T>(
           Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes = null!,
           Expression<Func<T, bool>> Criteria = null!,
           Expression<Func<T, object>> OrderBy = null!) where T : class, IAuditableEntity;
        Task<List<T>> GetAllAsync<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes = null!,
            Expression<Func<T, bool>> Criteria = null!,
            Expression<Func<T, object>> OrderBy = null!) where T : class, IAuditableEntity;

    }
    public class Repository : IRepository
    {
        public IAppDbContext Context { get; set; }

        public Repository(IAppDbContext context)
        {
            Context = context;
        }

        public Task UpdateAsync<T>(T entity) where T : class, IAuditableEntity
        {
            Context.Set<T>().Update(entity);

            return Task.CompletedTask;

        }

        public async Task AddAsync<T>(T entity) where T : class, IAuditableEntity
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public Task RemoveAsync<T>(T entity) where T : class, IAuditableEntity
        {
            Context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public Task RemoveRangeAsync<T>(List<T> entities) where T : class, IAuditableEntity
        {
            Context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
        public async Task<T?> GetByIdAsync<T>(Guid Id) where T : class, IAuditableEntity
        {
            var result = await Context.Set<T>().FindAsync(Id);
            return result;
        }


        public async Task<T?> GetAsync<T>(
            Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes = null!,
            Expression<Func<T, bool>> Criteria = null!,
           Expression<Func<T, object>> OrderBy = null!) where T : class, IAuditableEntity
        {
            var query = Context.Set<T>()
               .AsQueryable();


            if (Includes != null)
            {
                query = Includes(query);
            }
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }

            if (OrderBy != null)
            {
                query = query.OrderBy(OrderBy);
            }
            try
            {
                var result = await query.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }
            return null;
        }
        public async Task<List<T>> GetAllAsync<T>(
            Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes = null!,
            Expression<Func<T, bool>> Criteria = null!,
           Expression<Func<T, object>> OrderBy = null!) where T : class, IAuditableEntity
        {
            var query = Context.Set<T>()
                .AsQueryable();

            if (Includes != null)
            {
                query = Includes(query);
            }
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }

            if (OrderBy != null)
            {
                query = query.OrderBy(OrderBy);
            }
            return await query.ToListAsync();
        }

    }
}
