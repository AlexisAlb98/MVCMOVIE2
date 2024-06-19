using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de lanzamiento")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Categoria")]
        public string? Genre { get; set; }

        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

        [Display(Name = "Imagen Descriptiva")]
        public string? Image { get; set; }

    }
}
