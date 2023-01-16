
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                    }
                }
                catch 
                {
                    return Json("_GridRow");
                }
            }
            return Json("_GridRow");
        }

        // https://stackoverflow.com/questions/42360139/asp-net-core-return-json-with-status-code
        public JsonResult Paging(string nav, int fotId)
        {
            if (nav == "page")
            {
                return Json( _customerService.getRows(fotId));
            }
            string JSONresult = JsonConvert.SerializeObject(new { body = _customerService.getRows(fotId), footer = _customerService.getFooter(fotId, nav) }, Formatting.Indented);
            return Json(JSONresult);
        }
 
        public IActionResult Delete(int id)
        {
            _customerService.Remove(id);
            return View();
        }
 

    }
}