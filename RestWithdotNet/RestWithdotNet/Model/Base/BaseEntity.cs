using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithDotNet.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}