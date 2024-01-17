using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithDotNet.Model;
using RestWithDotNet.Model.Context;
using RestWithDotNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading;

namespace RestWithDotNet.Business.Implementations // Regras de negócio separadas do acesso ao banco (Repository) - persistindo ou recuperando informações
{
    public class BookBusinessImplementation : IBookBusiness
    {
        //private volatile int count; // mockando um ID - simulando uma consulta no banco

        //private MySQLContext _context; // injeção de dependencia
        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();

        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);

        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}