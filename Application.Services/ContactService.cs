using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private static IContactRepository _contactRepository;

        public ContactService  (IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Contact GetById(int id)
        {
            return _contactRepository.GetById(id);
        }
    }
}
