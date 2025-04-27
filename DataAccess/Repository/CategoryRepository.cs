using DataAccess.DataBase;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly AppDbContext _context;

		public CategoryRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public Category GetCategoryWithProducts(int id)
		{
			return _context.Categories.Include(c => c.Products) 
						   .FirstOrDefault(c => c.Id == id);
		}

		public IEnumerable<Category> SearchByName(string name)
		{
			return _context.Categories
						   .Where(c => c.Name.Contains(name))
						   .ToList();
		}

		public IEnumerable<Category> GetAll()
		{
			return _context.Categories.ToList();
		}

	
		public Category GetById(int id)
		{
			return _context.Categories.Include(c => c.Products)
					.FirstOrDefault(c => c.Id == id);
		}


		public void Add(Category category)
		{
			_context.Categories.Add(category);
			//_context.SaveChanges();
		}

		public void Update(Category category)
		{
			_context.Categories.Update(category);
			//_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				//_context.SaveChanges();
			}
		}
	}
}
