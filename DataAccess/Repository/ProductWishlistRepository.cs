using DataAccess.DataBase;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductWishlistRepository : Repository<ProductWishlist>
    {
        public ProductWishlistRepository(AppDbContext context) : base(context)
        {

        }
    }
}
