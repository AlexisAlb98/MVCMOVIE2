using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Services;

namespace MvcMovie.Controllers
{
    public class PedidosController : Controller
    {
        private readonly MoviesContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PedidoService _pedidoService;
        private readonly MercadoPagoService _mercadoPagoService;
        private readonly IConfiguration _configuration;

        public PedidosController(MoviesContext context, UserManager<IdentityUser> userManager, PedidoService pedidoService, MercadoPagoService mercadoPagoService, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _pedidoService = pedidoService;
            _mercadoPagoService = mercadoPagoService;
            _configuration = configuration; // Aquí inyectamos la configuración
        }

        // GET: Pedidos/RealizarPedido
        [Authorize] // Solo usuarios autenticados pueden acceder a esta acción
        public async Task<IActionResult> RealizarPedido()
        {
            // Obtener el usuario actualmente autenticado
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Si el usuario no está autenticado, redirigirlo a la página de inicio de sesión
                return RedirectToAction("Login", "Account");
            }

            // Obtener los ítems del carrito de compras
            var cartItems = await _context.CartItems
                .Include(item => item.Movie)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                // No hay elementos en el carrito de compras, redirigir a alguna página de error o a la página principal
                return RedirectToAction("Index");
            }

            // Calcular el total del pedido
            decimal totalPedido = (decimal)cartItems.Sum(item => item.Movie.Price * item.Quantity);

            // Crear un nuevo pedido utilizando la información del carrito de compras
            var pedido = new Pedido
            {
                Nombre = user.Email, // Utilizar el correo electrónico del usuario como nombre del pedido
                Detalles = string.Join(", ", cartItems.Select(item => $"{item.Movie.Title} (Cantidad: {item.Quantity})")),
                Total = totalPedido,
                UserId = user.Id, // Establecer el UserId con el ID del usuario autenticado
                CartItems = cartItems // Asignar la lista de ítems del carrito de compras al pedido
            };

            // Agregar el nuevo pedido al contexto y guardarlo en la base de datos
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            // Limpiar el carrito de compras después de realizar el pedido
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MisPedidos));
        }

        // GET: Pedidos
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
            {
                return View(await _context.Pedido.ToListAsync());
            }
        // GET: Pedidos/MisPedidos
        [Authorize] // Cualquier usuario autenticado puede ver sus propios pedidos
        public async Task<IActionResult> MisPedidos()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var pedidos = await _context.Pedido
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Detalles,Total,UserId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(pedido).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedido == null)
            {
                return Problem("Entity set 'MoviesContext.Pedido'  is null.");
            }
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
