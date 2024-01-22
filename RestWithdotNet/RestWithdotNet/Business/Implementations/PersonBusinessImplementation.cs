using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithDotNet.Data.Converter.Implementations;
using RestWithDotNet.Data.VO;
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
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());

        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));

        }

        public PersonVO Create(PersonVO person) // primeiro converter VO para entidade
        {
            var personEntity = _converter.Parse(person); // Parseando para entidade
            personEntity = _repository.Create(personEntity); // Persistindo os dados
            return _converter.Parse(personEntity); // Convertendo p/ VO e retornando a resposta
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
