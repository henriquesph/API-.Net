using RestWithDotNet.Model;
using System.Collections.Generic;

namespace RestWithDotNet.Services.Implementations
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long Id);
        List<Person> FindAll();
        Person Update (Person person);
        void Delete(long id);
    }
}