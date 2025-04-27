using DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _context;
		private readonly DbSet<T> dbSet;

		public Repository(AppDbContext context)
		{
			this._context = context;
			this.dbSet = _context.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await dbSet.AddAsync(entity);
		}

		public async Task CreateAsync(T entity)
		{
			await dbSet.AddAsync(entity);
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
				query = query.Where(filter);

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllAsync(
			Expression<Func<T, bool>>? 
			filter = null,
			string? includeProperties = null,
			int pageSize = 0,
			int pageNumber = 1)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
				query = query.Where(filter);

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			if (pageSize > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			return await query.ToListAsync();
		}



		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
		{
			IQueryable<T> query = dbSet;

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await query.Where(match).ToListAsync();
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null)
		{
			IQueryable<T> query = dbSet;

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await query.FirstOrDefaultAsync(match);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await dbSet.FindAsync(id);
		}

		public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
		{
			return await _context.Orders
				.Where(o => o.UserId == userId)
				.Include(o => o.OrderItems)   // إذا كان لديك علاقة مع OrderItems
				.Include(o => o.Address)      // إذا كنت بحاجة للـ Address المرتبط بالطلب
				.ToListAsync();
		}


		public async Task RemoveAsync(T entity)
		{
			dbSet.Remove(entity);
			await Task.CompletedTask;
		}

		public void Update(T entity)
		{
			dbSet.Update(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
