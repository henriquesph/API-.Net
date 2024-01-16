using RestWithDotNet.Model;
using System.Collections.Generic;

namespace RestWithDotNet.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);
        Person FindById(long Id);
        List<Person> FindAll();
        Person Update (Person person);
        void Delete(long id);
        bool Exists(long id);
    }
}