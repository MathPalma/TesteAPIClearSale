using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TesteAPIClearSale.Models;

namespace TesteAPIClearSale.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly List<Person> people;
        private volatile int count;

        public PersonServiceImplementation()
        {
            people = new List<Person>();

            for (int i = 1; i < 9; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }

        }
        public Person Create(Person person)
        {
            person.Id = IncrementAndGet();
            people.Add(person);
            return person;
        }

        public List<Person> FindAll()
        {
            return people;
        }

        public Person FindById(long id)
        {
            return people.Where(a => a.Id == id).FirstOrDefault();
        }

        public Person Update(Person person)
        {
            return person;
        }
        public void Delete(long id)
        {
            var existing = people.First(a => a.Id == id);
            people.Remove(existing);
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                Name = "Name " + i,
                Age = i + 5,
                Address = "Address " + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
