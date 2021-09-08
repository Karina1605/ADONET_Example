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
    public class ProductsController : ControllerBase
    {
        private readonly IDataBaseRep _rep;
        public ProductsController(IDataBaseRep rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _rep.Products.GetAll();
            if (res.Count() == 0)
                return NotFound();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _rep.Products.GetById(id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddNew([FromBody] Product product)
        {
            var res = _rep.Products.Insert(product);
            return Ok(res);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            var res = _rep.Products.Update(product);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var res = _rep.Products.Delete(Id);
            return Ok(res);
        }

    }
}
