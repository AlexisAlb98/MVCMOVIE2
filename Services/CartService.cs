using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Data; // Asegúrate de ajustar el namespace según tu estructura de proyecto
using MvcMovie.Models; // Ajusta según tu estructura de proyecto
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
                // Genera un nuevo CartId si no existe
                cartId = Guid.NewGuid().ToString();
                // Almacena el CartId en una cookie
                httpContext.Response.Cookies.Append("CartId", cartId);
            }
            return cartId;
        }

        public async Task AddToCart(int movieId, int quantity, HttpContext httpContext)
        {
            var cartId = GetCartId(httpContext);
            var cartItem = await _context.CartItems
                .SingleOrDefaultAsync(c => c.MovieId == movieId && c.CartId == cartId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    MovieId = movieId,
                    Quantity = quantity,
                    CartId = cartId
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
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
    }
}