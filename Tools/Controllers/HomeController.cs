
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using Tools.Models;
using Tools.Service.ServiceData;
using Tools.Tools.Grid;
using static Tools.Tools.CustomAttributes.AttrEnum;

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
            InitGrid grid = new InitGrid();
            grid.grid = _customerService.GetGrid();
            grid.columns = _customerService.getColumns(new CustomerDTO());
            grid.rows = JsonConvert.DeserializeObject(_customerService.getRowsWithIncludes());
            grid.footer = _customerService.getFooter();
            grid.enums = _customerService.getEnums(typeof(InputType), typeof(KeyType), typeof(HiddenClass));
            grid.ddls = _customerService.getDDLs(typeof(City));
            return View("Index", grid);
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
                            rows = JsonConvert.DeserializeObject(_customerService.getRowsWithIncludes(activeBtn).ToJson()),
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
                   rows = JsonConvert.DeserializeObject(_customerService.getRowsWithIncludes(activeBtn).ToJson()),
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
                rows = JsonConvert.DeserializeObject(_customerService.getRowsWithIncludes(fotId).ToJson()),
                footer = _customerService.getFooter(firstBtn, fotId)
            });
            return Json(refresh);
        }
 

    }
}