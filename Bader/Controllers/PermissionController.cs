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
using System.Security;

namespace Bader.Controllers
{
    public class PermissionController : Controller
    {
        private readonly PermissionDomain _PermissionDomain;

        public PermissionController(PermissionDomain context)
        {
            _PermissionDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index( string successful = " ", string failed = " ")
        {
            if(successful != "")
                ViewData["success"] = successful;
            else if(failed !=" ")
                ViewData["Failed"] = failed;
            return View(await _PermissionDomain.GetPermissions());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Guid id)
        {
            try
            {
                    int check = await _PermissionDomain.DeletePermission(id);
                    if (check == 1)
                    {
                        ViewData["Successful"] = "تم الحذف الصلاحية بنجاح";
                    }
                    else
                    {
                        ViewData["Failed"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                    }
            }
            catch
            {
                ViewData["Failed"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }

            var DomainInfo = await _PermissionDomain.GetPermissions();
            return View(DomainInfo);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await _PermissionDomain.GetRoles();
            var colleges = await _PermissionDomain.GetColleges();
            ViewBag.CollegesList = new SelectList(colleges, "Id", "CollegeNameAr");
            ViewBag.RolesList = new SelectList(roles, "Id", "RoleNameAr");
            return View();
        }

        // GET: Users/Details/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PermissionViewModel permission)
        {
            if (ModelState.IsValid)
            {
                await _PermissionDomain.AddPermission(permission);
                return RedirectToAction(nameof(Index));
            }
            var roles = await _PermissionDomain.GetRoles();
            var colleges = await _PermissionDomain.GetColleges();
            ViewBag.CollegesList = new SelectList(colleges, "Id", "CollegeNameAr");
            ViewBag.RolesList = new SelectList(roles, "Id", "RoleNameAr");
            return View(permission);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var permission = await _PermissionDomain.GetPermissionbyId(id);
            if (permission == null)
            {
                return NotFound();
            }
            var roles = await _PermissionDomain.GetRoles();
            var colleges = await _PermissionDomain.GetColleges();
            ViewBag.CollegesList = new SelectList(colleges, "Id", "CollegeNameAr");
            ViewBag.RolesList = new SelectList(roles, "Id", "RoleNameAr");
            return View(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PermissionViewModel permission)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (await _PermissionDomain.PermissionIdExists(permission.GUID))
                    //{
                    //    ModelState.AddModelError("Id", "لا يمكن ان يوجد رقم معرف متكرر");
                    //    return View(permission);
                    //}

                    int check = await _PermissionDomain.UpdatePermission(permission);

                    if (check == 1)
                    {
                        ViewData["Successful"] = "تم التعديل بنجاح";

                    }
                    else
                    {
                        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                    }
                }

            }
            catch
            {
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            }





            return View(permission);

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async IActionResult Delete(Guid id)
        //{
        //    string successful = " ";
        //    string failed = " ";
        //    try
        //    {
        //        int check = await _PermissionDomain.DeletePermission(id);
        //        if (check == 1)
        //        {
        //            successful = "تم حذف الصلاحية بنجاح";
        //        }
        //        else
        //        {
        //            failed = "حدث خطأ";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        failed = " حدث خطأ";
        //    }

        //    return RedirectToAction(nameof(Index), new { successful = successful, failed = failed });
        //}
        

        // POST: Users/Edit/5
        //    [HttpGet]
        //    public async Task<IActionResult> Delete(Guid id)
        //    {
        //        var permission = await _PermissionDomain.GetPermissionByGUID(id);
        //        if (permission == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(permission);
        //    }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]

        //    public async Task<IActionResult> Delete(PermissionViewModel permission)
        //    {

        //        await _PermissionDomain.DeletePermission(permission);
        //        return RedirectToAction(nameof(Index));
        //    }


    }

}
