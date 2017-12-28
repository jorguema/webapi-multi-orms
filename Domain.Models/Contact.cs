using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Contact: EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }

        private ICollection<Address> _addresses;
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new Collection<Address>()); }
            set { _addresses = value; }
        }
    }
}
