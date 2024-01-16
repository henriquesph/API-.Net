using RestWithDotNet.Model;
using System.Collections.Generic;

namespace RestWithDotNet.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindById(long Id);
        List<Person> FindAll();
        Person Update (Person person);
        void Delete(long id);
    }
}