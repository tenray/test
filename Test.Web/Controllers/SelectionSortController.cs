using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;
using Test.Core.SortLogic;


namespace Test.Web.Controllers
{
    [Route("api/[controller]")]
    public class SelectionSortController : Controller
    {
        IDataService db;
        ISortStrategy sorting;

        public SelectionSortController(IDataService dservice, SelectionSorting sorting)
        {
            this.db = dservice;
            this.sorting = sorting;
        }

        // GET api/SelectionSort?length=15&isascending=false
        [HttpGet]
        public JsonResult Get(int length, bool isAscending = true)
        {
            if (length > 0)
            {
                try
                {
                    return Json(sorting.Sort(new SortDTO { Length = length, IsAscending = isAscending }));
                }
                catch (ValidationException e)
                {
                    return Json(e.Message);
                }
            }
            else
                return Json("length must be greater than zero");
        }
    }
}
