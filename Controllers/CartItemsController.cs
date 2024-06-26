﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Services;


namespace MvcMovie.Controllers
{
    public class CartController : BaseController
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _cartService.GetCartItems(HttpContext);
            ViewBag.Genres = GetGenres();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int movieId)
        {
            await _cartService.AddToCart(movieId, HttpContext);
            ViewBag.Genres = GetGenres();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            await _cartService.UpdateCartItemQuantity(cartItemId, quantity);
            ViewBag.Genres = GetGenres();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Increment(int cartItemId)
        {
            await _cartService.IncrementCartItemQuantity(cartItemId);
            ViewBag.Genres = GetGenres();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Decrement(int cartItemId)
        {
            await _cartService.DecrementCartItemQuantity(cartItemId);
            ViewBag.Genres = GetGenres();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartService.RemoveFromCart(cartItemId);
            ViewBag.Genres = GetGenres();
            return RedirectToAction("Index");
        }
    }

}


