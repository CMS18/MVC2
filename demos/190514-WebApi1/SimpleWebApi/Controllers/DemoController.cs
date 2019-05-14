using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        List<string> _fruit = new List<string>
        {
            "Äpple",
            "Banan",
            "Ananas"
        };

        // GET: api/Demo
        [HttpGet()]
        public IEnumerable<string> Get()
        {
            return _fruit;
        }

        // GET: api/Demo/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<string> Get(int id)
        {
            if (id>=0 && id < _fruit.Count)
            {
                return Ok(_fruit[id]);
            }

            return NotFound();
        }

        // POST: api/Demo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Demo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
