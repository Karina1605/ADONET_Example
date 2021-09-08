using DataBaseConnector.Interfaces;
using DataBaseModels.DataBaseTables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example_ADO.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataBaseRep _rep;
        public CategoriesController(IDataBaseRep rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _rep.Categories.GetAll();
            if (res.Count() == 0)
                return NotFound();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _rep.Categories.GetById(id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddNew([FromBody] Category category)
        {
            var res = _rep.Categories.Insert(category);
            return Ok(res);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Category category)
        {
            var res = _rep.Categories.Update(category);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var res = _rep.Categories.Delete(Id);
            return Ok(res);
        }
    }
}
