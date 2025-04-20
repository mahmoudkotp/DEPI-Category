using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface ICategoryRepository
	{
		// دالة لجلب كاتيجوري مع المنتجات المرتبطة
		Category GetCategoryWithProducts(int id);

		IEnumerable<Category> SearchByName(string name);
		IEnumerable<Category> GetAll();
		Category GetById(int id);
		void Add(Category category);
		void Update(Category category);
		void Delete(Category category);

	}
}
