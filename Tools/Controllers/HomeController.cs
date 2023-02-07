
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
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

        // maybe would like to read this
        // https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
        // https://appetere.com/blog/passing-include-statements-into-a-repository
        public IActionResult Index()
        {
            var x = _customerService.Include(i => i.city);

            // Expression<Func<TEntityDTO, object>>[] myObjArray = { i => i.city, i => i.age };
            var xxx = _customerService.getRowsWithIncludeMultiple(page: 0, i => i.cityDTO, i => i.ageDTO);
          
            //_customerService.getRowsWithIncludes(o => o.Customer, o => o.LineItems);
            var result = _customerService.InitGrid();
            return View("Index", result);
        }

        [HttpPost]
        public async Task<JsonResult> ManageAsync(CustomerDTO model, int activeBtn, int firstBtn)
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
                            rows = JsonConvert.DeserializeObject(_customerService.getRows(activeBtn)),
                            footer = _customerService.getFooter(firstBtn, activeBtn)
                        });
                        return Json(refresh);
                    }
                    else
                    {
                        return Json(_customerService.Update(model));
                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.InnerException.Message);
                }
            }
            return Json("notValed ModelState");
        }

        // https://stackoverflow.com/questions/42360139/asp-net-core-return-json-with-status-code
        [HttpPost]
        public IActionResult Paging(int firstBtn, int activeBtn)
        {
            if (firstBtn == 0)
            {
                return Json(_customerService.getRows(activeBtn));
            }
            string refresh = JsonConvert.SerializeObject(
               new
               {
                   rows = JsonConvert.DeserializeObject(_customerService.getRows(activeBtn)),
                   footer = _customerService.getFooter(firstBtn, activeBtn)
               }
            );
            return Json(refresh);
        }

        public IActionResult Delete(int id, int fotId, int firstBtn)
        {
            _customerService.Remove(id, fotId);
            string refresh = JsonConvert.SerializeObject(
            new
            {
                rows = JsonConvert.DeserializeObject(_customerService.getRows(fotId)),
                footer = _customerService.getFooter(firstBtn, fotId)
            });
            return Json(refresh);
        }
 

    }
}