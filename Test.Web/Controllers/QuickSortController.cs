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
    public class QuickSortController : Controller
    {
        IDataService db;
        ISortStrategy sorting;

        public QuickSortController(IDataService dservice, QuickSorting sorting)
        {
            this.db = dservice;
            this.sorting = sorting;
        }

        // GET api/QuickSort?length=15&isascending=false
        [HttpGet]
        public ActionResult Get(int length, bool isAscending = true)
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
            {
                var ObjectRes = StatusCode(400, "length must be greater than zero");
                ObjectRes.StatusCode = 400;
                return ObjectRes;
            }
                //return Json("length must be greater than zero");
        }
    }
}
