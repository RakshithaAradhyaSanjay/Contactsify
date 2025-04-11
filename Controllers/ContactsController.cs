﻿using ContactsApp.Data;
using ContactsApp.Models;
using ContactsApp.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsDbContext dbContext;

        public ContactsController(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
           var contacts = dbContext.Contacts.ToList();
            return Ok(contacts);
        }
        [HttpPost]
        public IActionResult AddContact(List<AddContactRequestDto> request)
        {
            var domainModelContact = request.Select(request => new Contact
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favorite = request.Favorite
            });
            dbContext.Contacts.AddRange(domainModelContact);
           dbContext.SaveChanges();
            return Ok(domainModelContact);
        }
    }
}
