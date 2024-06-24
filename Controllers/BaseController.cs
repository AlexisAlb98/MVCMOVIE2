using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Controllers
{
    public class BaseController : Controller
    {
        protected List<SelectListItem> GetGenres()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "Celulares", Text = "Celulares" },
            new SelectListItem { Value = "Tablets", Text = "Tablets" },
            new SelectListItem { Value = "Notebooks", Text = "Notebooks" },
            new SelectListItem { Value = "Desktops", Text = "Desktops" },
            new SelectListItem { Value = "Smartwatches", Text = "Smartwatches" },
            new SelectListItem { Value = "Televisores", Text = "Televisores" },
            new SelectListItem { Value = "Smart TVs", Text = "Smart TVs" },
            new SelectListItem { Value = "Monitores", Text = "Monitores" },
            new SelectListItem { Value = "Auriculares", Text = "Auriculares" },
            new SelectListItem { Value = "Cámaras", Text = "Cámaras" },
            new SelectListItem { Value = "Impresoras", Text = "Impresoras" },
            new SelectListItem { Value = "Periféricos", Text = "Periféricos" },
            new SelectListItem { Value = "Consolas de Videojuegos", Text = "Consolas de Videojuegos" }
        };
        }
    }
}
