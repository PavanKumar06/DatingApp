using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]// Attribute based routing  when we specify a route inside our controller class. Then this is what's going to be mapped as our endpoint so that when we navigate or make a request to this particular controller then our application knows where to route that particular request
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext Context;

        public ValuesController(DataContext context)
        {
            Context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await Context.Values.ToListAsync();// Values here is our DbSet (it will get the values as a list)
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)// We get this parameter from the route
        {
            var value = await Context.Values.FirstOrDefaultAsync(x =>x.Id == id);// If it doesn't find the value it will return a type of Values
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
