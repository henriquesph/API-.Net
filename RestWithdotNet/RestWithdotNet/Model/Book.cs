using RestWithDotNet.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithDotNet.Model
{
    [Table("books")] // para ficar igual o definido no banco
    public class Book : BaseEntity
    {
        [Column("author")]
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime LauchDate { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("title")]
        public string Title { get; set; }
    }
}