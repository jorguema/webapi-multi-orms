using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Contact GetById(int id);
        IEnumerable<Contact> GetAll();
        Contact Add(Contact contact);
        void Delete(int id);
        Contact Save(Contact contact);
    }
}
