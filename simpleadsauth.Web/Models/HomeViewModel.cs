using SimpleAdsAuth.Data;

namespace simpleadsauth.Web.Models
{
    public class HomeViewModel
    {
        public List<Ad> Ads { get; set; }
        public User CurrentUser { get; set; }
    }
}
