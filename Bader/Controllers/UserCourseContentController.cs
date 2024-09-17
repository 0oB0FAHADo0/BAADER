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
using System.Runtime.InteropServices;
namespace Bader.Controllers
{

    public class UserCourseContentController : Controller
    {

        private readonly ContentDomain _ContentDomain;

        public UserCourseContentController(ContentDomain context)
        {
            _ContentDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            return View(await _ContentDomain.GetSomeContents(id));
        }
        [HttpGet]
        public async Task<IActionResult> ViewContent(Guid id)
        {
            return View(await _ContentDomain.GetContentByGUID(id));
        }
    }
}
