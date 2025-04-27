using DataAccess.DataBase;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		private readonly AppDbContext _context;

		public OrderRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public void Add(Order order)
		{
			_context.Orders.Add(order);
		}

		public void Delete(Order order)
		{
			_context.Orders.Remove(order);
		}

		//public IEnumerable<Order> GetAll()
		//{
		//	return _context.Orders.Include(o => o.OrderItems).ToList();
		//}

		public Order GetById(int id)
		{
			return _context.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefault(o => o.Id == id);
		}

		public Order GetOrderWithProducts(int id)
		{
			return _context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Product)
				.FirstOrDefault(o => o.Id == id);
		}
		
		public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
		{
			return await _context.Orders
				.Where(o => o.UserId == userId)
				.Include(o => o.OrderItems)  // إذا كنت بحاجة لربط OrderItems
				.Include(o => o.Address)     // إذا كنت بحاجة لربط Address
				.ToListAsync();
		}

		public async Task<Order> GetOrderWithItemsAsync(int orderId)
		{
			return await _context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Product)
				.FirstOrDefaultAsync(o => o.Id == orderId);
		}

		public async Task<IEnumerable<OrderItem>> SearchByUserIdAsync(string userId)
		{
			return await _context.OrderItems
				.Where(oi => oi.Order.UserId == userId)
				.Include(oi => oi.Product)
				.ToListAsync();
		}

		public IEnumerable<Order> SearchByName(string name)
		{
			return _context.Orders
				.Include(o => o.OrderItems)
				.Include(o => o.User)
				.Where(o => o.User.UserName.Contains(name))
				.ToList();
		}

		public void Update(Order order)
		{
			_context.Orders.Update(order);
		}

		//public async Task GetAsync(Func<object, bool> value, string includeProperties)
		//{
		//	// This is not implemented clearly in your interface; adjust as needed.
		//	await Task.CompletedTask;
		//}
	}
}
