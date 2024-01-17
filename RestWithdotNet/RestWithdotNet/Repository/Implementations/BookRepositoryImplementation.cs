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
    public class BookRepositoryImplementation : IBookRepository
    {
        //private volatile int count; // mockando um ID - simulando uma consulta no banco

        private MySQLContext _context; // injeção de dependencia

        public BookRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();

            //List<Person> persons = new List<Person>();
            //for(int i = 0; i < 8; i++)
            //{
            //    Person person = MockPerson(i);
            //    persons.Add(person);
            //}
            //return persons;
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));

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

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }

        public Book Update(Book book)
        {
            //if (!Exists(person.Id)) return new Person();
            if (!Exists(book.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
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
            return _context.Books.Any(p => p.Id.Equals(id));
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
