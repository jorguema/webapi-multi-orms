using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Micro_ORMS.Controllers
{
    [RoutePrefix("api/contacts")]
    public class ContactsController : ApiController
    {
        private static IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null) return NotFound();

            return Ok(contact);
        }
    }
}