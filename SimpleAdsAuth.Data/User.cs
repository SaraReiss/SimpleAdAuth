using System.ComponentModel;

namespace SimpleAdsAuth.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        //public List<Ad> ads { get; set; }
    }
}