using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency
{
    public partial class Accounts 
    {
        public string SName { get; set; }
        public string FName { get; set; }
        public string UserRoleName { get; set; }
        public string UserStatusName { get; set; }
        public string UserWhoCreate { get; set; }
        public byte[] Photo { get; set; }
    }
}
