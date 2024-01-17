using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithDotNet.Model;
using RestWithDotNet.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading;

namespace RestWithDotNet.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        //private volatile int count; // mockando um ID - simulando uma consulta no banco

        private MySQLContext _context; // injeção de dependencia

        public PersonRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();

            //List<Person> persons = new List<Person>();
            //for(int i = 0; i < 8; i++)
            //{
            //    Person person = MockPerson(i);
            //    persons.Add(person);
            //}
            //return persons;
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            //return new Person 
            //{
            //    //Id = IncrementAndGet(),
            //    Id = 1,
            //    Name = "Pedro",
            //    LastName = "Silva",
            //    Adress = "Avenida Darcy Vargas",
            //    Gender = "Male"
            //};
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public Person Update(Person person)
        {
            //if (!Exists(person.Id)) return new Person();
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

        //private Person MockPerson(int i)
        //{
        //    return new Person
        //    {
        //        //Id = IncrementAndGet(),
        //        Id = 1,
        //        Name = "Person Name" + i,
        //        LastName = "Person LastName" + i,
        //        Adress = "Person Adress" + i,
        //        Gender = "Male"
        //    };
        //}

        //private long IncrementAndGet()
        //{
        //    return Interlocked.Increment(ref count);
        //}
    }
}
