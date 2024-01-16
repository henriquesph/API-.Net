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
    public class PersonBusinessImplementation : IPersonBusiness
    {
        //private volatile int count; // mockando um ID - simulando uma consulta no banco

        //private MySQLContext _context; // injeção de dependencia
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();

        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);

        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
