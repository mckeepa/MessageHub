using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace coreWeb.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly NewSystemContext _context;

        public PersonController(ILogger<PersonController> logger, NewSystemContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public Models.ReturnObject<IEnumerable<Models.Person>> Get()
        {
            var rng = new Random();
            var personList = Enumerable.Range(1, 5).Select(index => new Models.Person
            {
                GivenName1 = "GivenName1",
                GivenName2 = "GivenName2",
                // Identifiders = new Dictionary<string, string>(){
                //     {"LEGACY", "12345"},
                //     {"NEW", "PER-2345"},
                //     {"SUPPORT", "ABC2345"},
                // }
            })
            .ToArray();

            

            // using (var db = new NewSystemContext())
            // {
            //     // Create
            //     Console.WriteLine("Inserting a person");
            //     db.Add(new Person { 
            //         GivenName1 = "Test" + DateTimeOffset.Now.LocalDateTime.ToLongTimeString() });
            //     db.SaveChanges();

            //     // Read
            //     Console.WriteLine("Querying for a Person");
            //     personList = db.Persons
            //         .OrderBy(b => b.PersonId).ToArray();
            // }


            return new Models.ReturnObject<IEnumerable<Models.Person>>(){
                data =_context.Persons.ToList()
            };
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson(Person person)
    {
        var per = new Person
        {
            DateOfBirth = person.DateOfBirth,
            GivenName1 = person.GivenName1
        };

        _context.Persons.Add(per);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = per.PersonId }, person);
    }



        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Models.Person person )
        {

            Models.Person returnPerson;
            try
            {
                var dbPerson = _context.Persons.Where(per=> per.PersonId.Equals(id)).SingleOrDefault();
                dbPerson.DateOfBirth = person.DateOfBirth;
                dbPerson.GivenName1 = person.GivenName1;
                dbPerson.GivenName2 = person.GivenName2;
                dbPerson.GivenName3 = person.GivenName3;
                dbPerson.FamilyName = person.FamilyName;
                dbPerson.Alerts = person.Alerts;
                
                //Set the retrun object
                returnPerson = dbPerson;
            }            
            catch {  
                // _context.Entry(person).State = EntityState.Modified;
                _context.Persons.Add(person);

                //Set the retrun object
                returnPerson = person;
            };
         
            await _context.SaveChangesAsync();          
            return CreatedAtAction(nameof(Get), new { id = person.PersonId }, person);
        }

         private bool PersonExists(Guid id) => 
            _context.Persons.Any(e => e.PersonId == id);
    }
}
