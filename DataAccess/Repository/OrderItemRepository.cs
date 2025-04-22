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
	public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
	{
		private readonly AppDbContext _context;

		public OrderItemRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
		
		public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
		{
			return await _context.OrderItems
				.Where(oi => oi.OrderId == orderId)
				.Include(oi => oi.Order)
				.ToListAsync();
		}
		
		public IEnumerable<OrderItem> GetAll()
		{
			return _context.OrderItems.ToList();
		}

		public OrderItem GetById(int id)
		{
			return _context.OrderItems.Find(id);
		}

		public void Add(OrderItem orderItem)
		{
			_context.OrderItems.Add(orderItem);
			_context.SaveChanges();
		}

		public void Update(OrderItem orderItem)
		{
			_context.OrderItems.Update(orderItem);
			_context.SaveChanges();
		}
		
		public void Delete(OrderItem orderItem)
		{
			if (orderItem != null)
			{
				_context.OrderItems.Remove(orderItem);
				_context.SaveChanges();
			}
		}
	}
}
