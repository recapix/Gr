using System;

namespace GR.Web.Models
{
    public class AuthorListingViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalBooks { get; set; }
    }
}
