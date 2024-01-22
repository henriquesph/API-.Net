using RestWithDotNet.Data.VO;
using System.Collections.Generic;

namespace RestWithDotNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long Id);
        List<PersonVO> FindAll();
        PersonVO Update (PersonVO person);
        void Delete(long id);
    }
}