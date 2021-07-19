using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding_11083.Models
{
    public class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class Person : Human
    {
        public List<Person> Friends { get; set; }

    }
    
}
