using Microsoft.AspNetCore.Mvc;
namespace Bader.Controllers;
using Bader.Domain;
using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Scripting.Hosting;

public class CollegesController : Controller
{
    private readonly CollegeDomain _CollegeDomain;

    public CollegesController(CollegeDomain collegeDomain)
    {
        _CollegeDomain = collegeDomain;
    }

    [HttpGet]
    public async Task<IActionResult> Details()
    {
        var DomainInfo = await _CollegeDomain.GetAllColleges();



        return View(DomainInfo);
    }

    [HttpPost]
    public async Task<IActionResult> Details(Guid id)
    {
      
        try
        {
            CollegeViewModel College = await _CollegeDomain.GetCollegebyId(id);

            College.IsDeleted = true;
            int check = await _CollegeDomain.UpdateCollege(College);
            
            if (check == 1)
            {
                ViewData["Successful"] = "تم الحذف بنجاح";

            }
            else
            {

                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            }

        }
        catch
        {
            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

        }



        var DomainInfo = await _CollegeDomain.GetAllColleges();
        return View(DomainInfo);
    }




    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    
    public async Task<IActionResult> Add(CollegeViewModel college)
    {

        try
        {
            if (ModelState.IsValid)
            {
                if (await _CollegeDomain.BuildingNumExists(college.BuildingNum, college.GUID))
                {
                    ModelState.AddModelError("BuildingNum", "لا يمكن ان يوجد رقم المبنى لاكثر من كلية");
                    return View(college);
                }
                int check=await _CollegeDomain.AddCollege(college);
                if(check ==1)
                {
                    ViewData["Successful"] = "تمت الإضافة بنجاح";

                }
                else
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                }


               // return RedirectToAction("Details");
                
            }
        }
        catch
        {
            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

        }


        return View(college);

       

    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {

        try
        {
            CollegeViewModel College = await _CollegeDomain.GetCollegebyId(id);
            return View(College);
        }
        catch
        {

        }
        

        

        return View();
    }

  
   

    public async Task<IActionResult> Edit(CollegeViewModel college)
    {
        try {
            if (ModelState.IsValid)
            {
                if (await _CollegeDomain.BuildingNumExists(college.BuildingNum, college.GUID))
                {
                    ModelState.AddModelError("BuildingNum", "لا يمكن ان يوجد رقم المبنى لاكثر من كلية");
                    return View(college);
                }

               int check= await _CollegeDomain.UpdateCollege(college);
                //return RedirectToAction("Details");

                if(check == 1)
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





        return View(college);

    }


 

}


