using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace coreWeb.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
      
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Model.ReturnObject<IEnumerable<Model.Person>> Get()
        {
            var rng = new Random();
            var personList = Enumerable.Range(1, 5).Select(index => new Model.Person
            {
                GivenName1 = "GivenName1",
                GivenName2 = "GivenName2",
                Identifiders = new Dictionary<string, string>(){
                    {"LEGACY", "12345"},
                    {"NEW", "PER-2345"},
                    {"SUPPORT", "ABC2345"},
                }
            })
            .ToArray();

            return new Model.ReturnObject<IEnumerable<Model.Person>>(){data =personList};
        }
    }
}
