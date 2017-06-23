using Microsoft.AspNetCore.Mvc;
using System;
using GR.Data.UnityOfWork;
using GR.Web.Models.ContactViewModel;

namespace GR.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnityOfWork _uow;

        public HomeController(IUnityOfWork uow)
        {
            _uow = uow;
        } 

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["ContactSucess"] = false;
            ViewData["Message"] = "Your contact page.";

            return View(new ContactViewModel());
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            ViewData["Message"] = "Your contact page.";
            if (ModelState.IsValid)
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                ViewData["ContactSucess"] = true;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
