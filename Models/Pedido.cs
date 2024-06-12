using MvcMovie.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel;

namespace MvcMovie.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public List<CartItem>? CartItems { get; set; } // Agregar esta lista
        public string? Nombre { get; internal set; }
        public string? Detalles { get; internal set; }
        public decimal? Total { get; internal set; }
        public string? UserId { get; set; }
       
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

    }
}
