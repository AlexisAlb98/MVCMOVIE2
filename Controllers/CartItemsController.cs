using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Services;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _cartService.GetCartItems(HttpContext);
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int movieId, int quantity)
        {
            await _cartService.AddToCart(movieId, quantity, HttpContext);
            return RedirectToAction("Index");
        }
    }
}