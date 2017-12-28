using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Address: EntityBase
    {
        public int ContactId { get; set; }
        public string AddressType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string PostalCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}
