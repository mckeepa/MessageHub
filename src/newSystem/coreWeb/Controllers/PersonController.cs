using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
