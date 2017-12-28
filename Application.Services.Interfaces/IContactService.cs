using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IContactService
    {
        Contact GetById(int id);
        IEnumerable<Contact> GetAll();
        Contact Add(Contact contact);
        void Delete(int id);
    }
}
