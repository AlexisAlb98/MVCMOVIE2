using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Data;
using MvcMovie.Models;
using Microsoft.AspNetCore.Http;

namespace MvcMovie.Services
{
    public class CartService
    {
        private readonly MoviesContext _context;

        public CartService(MoviesContext context)
        {
            _context = context;
        }

        private string GetCartId(HttpContext httpContext)
        {
            if (!httpContext.Request.Cookies.TryGetValue("CartId", out var cartId))
            {
                cartId = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("CartId", cartId);
            }
            return cartId;
        }

        public async Task AddToCart(int movieId, HttpContext httpContext)
        {
            var cartId = GetCartId(httpContext);
            var cartItem = await _context.CartItems
                .SingleOrDefaultAsync(c => c.MovieId == movieId && c.CartId == cartId);

            if (cartItem == null)
            {
                var movie = await _context.Movies.FindAsync(movieId);
                if (movie == null)
                {
                    throw new KeyNotFoundException("Movie not found");
                }

                cartItem = new CartItem
                {
                    MovieId = movieId,
                    Quantity = 1,
                    CartId = cartId,
                    Movie = movie
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(HttpContext httpContext)
        {
            var cartId = GetCartId(httpContext);
            return await _context.CartItems
                .Where(c => c.CartId == cartId)
                .Include(c => c.Movie)
                .ToListAsync();
        }
        public async Task UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item not found");
            }

            if (quantity >= 1)
            {
                cartItem.Quantity = quantity; // Actualiza la cantidad a la nueva cantidad especificada
            }
            else
            {
                throw new ArgumentException("Quantity must be at least 1");
            }

            await _context.SaveChangesAsync();
        }

        public async Task IncrementCartItemQuantity(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item not found");
            }

            cartItem.Quantity += 1;

            await _context.SaveChangesAsync();
        }

        public async Task DecrementCartItemQuantity(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item not found");
            }

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else
            {
                throw new ArgumentException("Quantity must be at least 1");
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
