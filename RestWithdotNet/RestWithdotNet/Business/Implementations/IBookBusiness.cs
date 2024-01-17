using RestWithDotNet.Model;
using System.Collections.Generic;

namespace RestWithDotNet.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book FindById(long Id);
        List<Book> FindAll();
        Book Update (Book book);
        void Delete(long id);
    }
}