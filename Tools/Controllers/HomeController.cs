﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public JsonResult RefreshGrid(int fotId)
        {
            string refresh = JsonConvert.SerializeObject(
               new
               {
                   rows = JsonConvert.DeserializeObject(_customerService.getRows(fotId)),
                   footer = _customerService.getFooter(fotId)
               }
            );
            return Json(refresh);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAsync(CustomerDTO model, int fotId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        await _customerService.AddAsync(model);
                        string refresh = JsonConvert.SerializeObject(
                        new
                        {
                            rows = JsonConvert.DeserializeObject(_customerService.getRows(fotId)),
                            footer = _customerService.getFooter(fotId)
                        });
                        return Json(refresh);
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
        [HttpPost]
        public IActionResult Paging(string nav,int fotId)
        {
            if (nav == "page")
            {
                return Json(_customerService.getRows(fotId));
            }
            string refresh = JsonConvert.SerializeObject(
               new
               {
                   rows = JsonConvert.DeserializeObject(_customerService.getRows(fotId)),
                   footer = _customerService.getFooter(fotId, nav)
               }
            );
            return Json(refresh);
        }
 
        public IActionResult Delete(int id, int page)
        {
            return Json(_customerService.Remove(id, page));
        }
 

    }
}