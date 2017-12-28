using Application.Services.Interfaces;
using Domain.Models;
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
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null) return NotFound();

            return Ok(contact);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var contacts = _contactService.GetAll();

            return Ok(contacts);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] Contact contactDto)
        {
            if (contactDto == null) return BadRequest();

            var contact = _contactService.Add(contactDto);

            return Ok(contact);
        }


        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
           _contactService.Delete(id);

            return Ok();
        }
    }
}