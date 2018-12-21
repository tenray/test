using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;


namespace Test.Web.Controllers
{
    [Route("api/[controller]")]    
    public class SetDataController : Controller
    {
        IDataService service;

        public SetDataController(IDataService ds)
        {
            service = ds;
        }
                
        // POST api/SetData
        [HttpPost]
        public JsonResult Post([FromBody]int[] values)
        {
            try
            {
                if (values != null)
                {
                    var storageDto = new StorageDTO { Values = values.ToList()};
                    service.SetData(storageDto);
                    return Json(storageDto);
                }
                else
                    return Json("empty sequence");
            }
            catch (ValidationException ex)
            {                
                return Json(ex.Message);
            }
        }        
    }
}
