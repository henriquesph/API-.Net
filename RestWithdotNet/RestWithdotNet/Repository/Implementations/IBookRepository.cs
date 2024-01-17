using RestWithDotNet.Model;
using System.Collections.Generic;

namespace RestWithDotNet.Repository
{
    public interface IBookRepository
    {
        Book Create(Book person);
        Book FindById(long Id);
        List<Book> FindAll();
        Book Update (Book person);
        void Delete(long id);
        bool Exists(long id);
    }
}