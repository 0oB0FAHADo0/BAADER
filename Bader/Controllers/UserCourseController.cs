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
using System.Linq.Expressions;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
namespace Bader.Controllers
{
    public class UserCourseController : Controller
    {

        private readonly CourseDomain _CourseDomain;

        public UserCourseController(CourseDomain context)
        {
            _CourseDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collagecode = User.FindFirst("CollegeCode").Value;

            return View(await _CourseDomain.GetSomeCourses(collagecode));
        }
    }
}
