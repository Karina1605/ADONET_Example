﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector.Interfaces;
using DataBaseModels.DataBaseTables;

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
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var res = _rep.Colors.GetById(id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }
        [HttpPost]
        public IActionResult AddNew([FromBody] Color color)
        {
            var res = _rep.Colors.Insert(color);
            return Ok(res);
        }
        [HttpPut]
        public IActionResult Update([FromBody] Color color)
        {
            var res = _rep.Colors.Update(color);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete (string Id)
        {
            var res = _rep.Colors.Delete(Id);
            return Ok(res);
        }
    }
}
