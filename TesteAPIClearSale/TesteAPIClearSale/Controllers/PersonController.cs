using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteAPIClearSale.Models;
using TesteAPIClearSale.Services;

namespace TesteAPIClearSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<Person> Get()
        {

            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var p = _personService.Create(person);

            return CreatedAtAction("Get", new { id = person.Id }, person);
        }

        [HttpPut]
        public ActionResult Put([FromBody] Person person)
        {

            if (_personService == null) return BadRequest();
            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var existingItem = _personService.FindById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            _personService.Delete(id);
            return Ok();
        }

    }
}
