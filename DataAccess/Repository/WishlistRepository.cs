﻿using DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class WishlistRepository : Repository<Wishlist>
    {
        private readonly AppDbContext _context;
        public WishlistRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Wishlist> CreateWishlist(string userId)
        {
            var wishlist = new Wishlist
            {
                UserId = userId
            };
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();

            return wishlist;
        }
        public async Task<Wishlist> GetWishlistByUserId(string userId)
        {
            var list = await _context.Wishlists.Include(x => x.Products).ThenInclude(pw => pw.Product).FirstOrDefaultAsync(w => w.UserId == userId);

            if (list is null)
            {
                return await CreateWishlist(userId);
            }
            return list;
        }

        public async Task<bool> AddProductToWishlist(int productId, string userId)
        {
            var wishlist = await GetWishlistByUserId(userId);
            if (wishlist is null)
            {
                await CreateWishlist(userId);
            }

            bool isExist = wishlist.Products.Any(x => x.ProductId == productId);

            if (!isExist)
            {
                var productWishlist = new ProductWishlist
                {
                    ProductId = productId,
                    WishlistId = wishlist.Id
                };
                await _context.ProductWishlists.AddAsync(productWishlist);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveFromWishlist(int productId, string userId)
        {
            var wishlist = await GetWishlistByUserId(userId);
            if (wishlist is null)
            {
                return false;
            }
            var item = wishlist.Products.FirstOrDefault(wi => wi.ProductId == productId);
            if (item != null)
            {
                _context.ProductWishlists.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
