using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Diagnostics;
using Tools.Models;
using Tools.Service;
using Tools.Service.ServiceData;
using Tools.Tools.Grid;
using static Tools.Service.ResponseResult;

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
            var result = _customerService.GetGrid();
            return View(result);
        }

        public async Task<IActionResult> ManageAsync(CustomerDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        return PartialView("_GridRow", await _customerService.AddAsync(model));
                        //return Json(await _customerService.AddAsync(model));
                    }
                    else
                    {
                        return PartialView("_GridRow", _customerService.Update(model));
                        //return Json(_customerService.Update(model));
                    }
                }
                catch (Exception ex)
                {
                    return PartialView("_GridRow", new ResponseResult(err: ex.Message));
                }
            }
            return PartialView("_GridRow", new ResponseResult(err: "فشلت العملية"));
        }
        
        public IActionResult Delete(int id)
        {
            _customerService.Remove(id);
            return View();
        }

        public IActionResult Paging(int pagerStart, string pageAction)
        {
            if (string.IsNullOrEmpty(pageAction) || pageAction == "undefined")
            {
                Grid result =  _customerService.GetGrid(pagerStart);
                return PartialView("_GridRowList", result);
            }
            else
            {
                List<PagerButton> result = _customerService.GetPager(pagerStart, pageAction);
                return PartialView("_GridPagination", result);
            }
        }
 

    }
}