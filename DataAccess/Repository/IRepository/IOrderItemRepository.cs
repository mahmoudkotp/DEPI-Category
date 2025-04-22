using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface IOrderItemRepository
	{

		//IEnumerable<OrderItem> SearchByName(string name);

		IEnumerable<OrderItem> GetAll();
		Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);

		//Task<OrderItem> GetOrderItemByIdAsync(int id); // عنصر واحد حسب Id


		OrderItem GetById(int id);
		void Add(OrderItem orderItem);
		void Update(OrderItem orderItem);
		void Delete(OrderItem orderItem);

	}
}
