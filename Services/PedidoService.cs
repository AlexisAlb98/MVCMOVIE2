using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Services
{
    public class PedidoService
    {
        private readonly MoviesContext _context;

        public PedidoService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _context.Pedido
                .Include(p => p.CartItems)
                .ThenInclude(ci => ci.Movie)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
