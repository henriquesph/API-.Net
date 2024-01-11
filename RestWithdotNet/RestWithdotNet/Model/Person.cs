using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithDotNet.Model
{
    [Table("person")] // para ficar igual o definido no banco
    public class Person
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("first_name")]
        public string Name { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("address")]
        public string Adress { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
    }
}