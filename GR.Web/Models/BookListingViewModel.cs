using System;

namespace GR.Web.Models
{
    public class BookListingViewModel
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
    }
}
