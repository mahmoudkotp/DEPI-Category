using DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> dbSet;

		public Repository(AppDbContext context)
        {
            this._context = context;
            this.dbSet = _context.Set<T>();
			_context = context;
		}

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            var entity = await query.FirstOrDefaultAsync();
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 0)
         {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (pageNumber > 0)
            {
                if (pageNumber > 50)
                {
                    pageNumber = 50;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
         }
			
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.Where(match).ToListAsync();
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.FirstOrDefaultAsync(match);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
