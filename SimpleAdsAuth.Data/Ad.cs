using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdsAuth.Data
{
    public  class Ad
    {
        public string UserName { get; set; }
        public string UserNumber { get; set; }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Details { get; set; }
        public int UserId { get; set; }
    }
}
