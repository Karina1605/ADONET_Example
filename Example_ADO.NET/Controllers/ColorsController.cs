using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector.Interfaces;

namespace Example_ADO.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IDataBaseRep _rep;
        public ColorsController(IDataBaseRep rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _rep.Colors.GetAll();
            if (res.Count() == 0)
                return NotFound();
            return Ok(res);
        }
    }
}
