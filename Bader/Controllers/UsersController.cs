using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bader.Models;
using Bader.Domain;
using Bader.ViewModels;

namespace Bader.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDomain _UserDomain;

        public UsersController(UserDomain context)
        {
            _UserDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _UserDomain.GetUsers());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // GET: Users/Details/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (await _UserDomain.EmailExists(user.Id, user.Email))
                {
                    ModelState.AddModelError("Email", "البريد الإلكتروني أستخدم بالفعل.");
                    return View(user);
                }
                await _UserDomain.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _UserDomain.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(UserViewModel user)
        {
               if (ModelState.IsValid)
                {
                if (await _UserDomain.EmailExists(user.Id,user.Email))
                {
                    ModelState.AddModelError("Email", "البريد الإلكتروني نستخدم بالفعل.");
                    return View(user);
                }
                await _UserDomain.UpdateUser(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
        }


    }
    
}

