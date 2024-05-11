using System.Collections.Generic;

namespace XamarinMobileApp.Models
{
    public class Account
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Phone { get; set; }
        public string Surname { get; set; }
        public List<int> Favourites { get; set; }
    }
}