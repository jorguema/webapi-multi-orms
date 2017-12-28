using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
            Id = default(int);
        }

        public EntityBase(bool initialize)
        {
            if (!initialize) return;

            Id = default(int);
        }        

        public int Id { get; set; }
        public bool IsNew
        {
            get
            {
                return this.Id == default(int);
            }
        }
    }
}
