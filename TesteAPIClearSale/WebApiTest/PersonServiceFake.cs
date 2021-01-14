using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TesteAPIClearSale.Models;
using TesteAPIClearSale.Services;

namespace WebApiTest
{
    public class PersonServiceFake : IPersonService
    {
        private readonly List<Person> people;
        private volatile int count;

        //Creating Fake Data
        public PersonServiceFake()
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

        public void Delete(long id)
        {
            var existing = people.First(a => a.Id == id);
            people.Remove(existing);
        }

        public List<Person> FindAll()
        {
            return people;
        }

        public Person FindById(long id)
        {
            return people.Where(a => a.Id == id).FirstOrDefault();
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

        public long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person Update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
