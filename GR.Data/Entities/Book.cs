using System;

namespace GR.Data.Entities
{
    public class Book : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public virtual Author Author { get; set; }
    }
}
