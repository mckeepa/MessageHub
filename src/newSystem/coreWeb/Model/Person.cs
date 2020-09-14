using System;
using System.Collections.Generic;

namespace coreWeb.Model
{
    public class Person
    {
        public Person(){
            this.Alerts = new List<Alert>();
            this.Identifiders = new Dictionary<string, string>();
        }
        public String GivenName1  { get; set; }

        public String GivenName2  { get; set; }
        public String GivenName3  { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Dictionary<string, string> Identifiders {get; set;}  
        public IEnumerable<Alert> Alerts {get; set;}
    }
    public class Alert{
        public int Id {get; set;}
        public string Title {get; set;}
        public DateTimeOffset? StartDate {get; set;}
        public  DateTimeOffset? EndDate {get; set;}
    } 
}
