using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bader.Models;
using Bader.Domain;

namespace Bader.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDomain _UserDomain;

        public UsersController(UserDomain context)
        {
            _UserDomain = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _UserDomain.GetUsers());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        // GET: Users/Details/5
    }
}
