﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bader.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Bader.Domain;
using Bader.ViewModels;
using Newtonsoft.Json;
using System.Security.Principal;

namespace Bader.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserDomain _UserDomain;

        public AccessController(UserDomain context)
        {
            _UserDomain = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            ClaimsPrincipal principal = HttpContext.User;

            if (principal.Identity.IsAuthenticated)
            {
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "Admin" || role == "SuperAdmin" || role == "Editor")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
                return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel userlogin)
        {
            try
            {
                var user = await _UserDomain.ValidateUser(userlogin.Username, userlogin.Password);

                if (user == null)
                {
                    ViewData["ErrorMessage"] = "خطأ: اسم المستخدم أو كلمة المرور غير صحيحة";
                    return View(userlogin);
                }
                else
                {
                    List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("FullNameAr", user.FullNameAr),
                new Claim("FullNameEn", user.FullNameEn),
                new Claim("Email", user.Email),
                new Claim("Phone", user.Phone),
                new Claim("Usertype", user.Usertype),
                new Claim("CollegeNameAr", user.CollegeNameAr),
                new Claim("CollegeNameEn", user.CollegeNameEn),
                new Claim("CollegeCode", user.CollegeCode),
                new Claim("Role",  user.RoleNameEn),
                new Claim("Gender",  user.Gender? "انثى" : "ذكر"),
                new Claim(ClaimTypes.Role, user.RoleNameEn)

            };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                    if (user.RoleNameEn == "Admin" || user.RoleNameEn == "SuperAdmin" || user.RoleNameEn == "Editor")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else 
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            catch 
            {
                ViewData["ErrorMessage"] = "خطأ: اسم المستخدم أو كلمة المرور غير صحيحة";
                return View(userlogin);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

    }
}
