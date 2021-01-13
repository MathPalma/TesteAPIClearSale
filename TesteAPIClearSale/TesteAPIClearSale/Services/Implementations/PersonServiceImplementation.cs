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
        private volatile int count;
        public Person Create(Person person)
        {
            return person;
        }

        public List<Person> FindAll()
        {
            List<Person> people = new List<Person>();
            for (int i = 1; i < 9; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }
            return people;
        }

        public Person FindById(long id)
        {
            List<Person> people = FindAll();
            Person person;

            foreach (var p in people)
            {
                if (p.Id == id)
                {
                    person = p;
                    return person;
                }
            }
            return new Person
            {
                Id = IncrementAndGet(),
                Name = "Matheus",
                Address = "Santos - São Paulo - Brasil",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
        public void Delete(long id)
        {
            //doesn't need to return or any logics
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
