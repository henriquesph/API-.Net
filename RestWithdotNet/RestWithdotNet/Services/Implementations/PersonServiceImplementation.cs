using RestWithDotNet.Model;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading;

namespace RestWithDotNet.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count; // mockando um ID - simulando uma consulta no banco

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {

        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for(int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person FindById(long Id)
        {
            return new Person 
            {
                Id = IncrementAndGet(),
                Name = "Pedro",
                LastName = "Silva",
                Adress = "Avenida Darcy Vargas",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                Name = "Person Name" + i,
                LastName = "Person LastName" + i,
                Adress = "Person Adress" + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
