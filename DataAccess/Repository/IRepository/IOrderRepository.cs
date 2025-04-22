using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
			
		Order GetOrderWithProducts(int id);


		Task<IEnumerable<OrderItem>> SearchByUserIdAsync(string userId);
		Task<Order> GetOrderWithItemsAsync(int orderId);

		IEnumerable<Order> SearchByName(string name);
		//IEnumerable<Order> GetAll();
		Order GetById(int id);
		void Add(Order order);
		void Update(Order order);
		void Delete(Order order);
		//Task GetAsync(Func<object, bool> value, string includeProperties);
	}
}
