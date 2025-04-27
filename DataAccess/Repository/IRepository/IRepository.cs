using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(
			Expression<Func<T, bool>>?
			filter = null, 
			string? includeProperties = null,
            int pageSize = 0, 
			int pageNumber = 1);

		//IEnumerable<T> GetAllSync();

		Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null);

        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);

		
		//Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null);
		Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null);

		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task SaveChangesAsync();
	}
}
