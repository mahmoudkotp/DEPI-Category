using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
	{
        public AppUserRepository AppUsers { get; }
        public ProductWishlistRepository ProductWishlists { get; }
        public WishlistRepository Wishlists { get; }
        public AddressRepository Addresses { get; }
		//public CategoryRepository Categories { get; }
		ICategoryRepository CategoryRepository { get; }
		//public OrderRepository Orders { get; }
		IOrderRepository Orders { get; }
		//public OrderItemRepository OrderItems { get; }
		IOrderItemRepository OrderItems { get; }


		Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

		Task SaveAsync();

    }
}
