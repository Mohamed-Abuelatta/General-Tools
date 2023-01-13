
using Microsoft.AspNetCore.Mvc;
using Tools.Models;
using Tools.Service.ServiceData;

namespace Tools.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var result = _customerService.InitGrid();
            return View("Index", result);
        }

        public async Task<IActionResult> ManageAsync(CustomerDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        return Json("_GridRow", await _customerService.AddAsync(model));
                        //return Json(await _customerService.AddAsync(model));
                    }
                    else
                    {
                        return Json("_GridRow", _customerService.Update(model));
                        //return Json(_customerService.Update(model));
                    }
                }
                catch 
                {
                    return Json("_GridRow");
                }
            }
            return Json("_GridRow");
        }
        
        public IActionResult Delete(int id)
        {
            _customerService.Remove(id);
            return View();
        }
 

    }
}