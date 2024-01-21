using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;
        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) 
        
        { 

            var Contact = new Contact()
            {
                Id= Guid.NewGuid(),
                Adderess = addContactRequest.Adderess,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone= addContactRequest.Phone,
             };

            await dbContext.Contacts.AddAsync(Contact);
            await dbContext.SaveChangesAsync();
        
            return Ok(Contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactsRequest updateContactsRequest)
        {


            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FullName = updateContactsRequest.FullName;
                contact.Adderess = updateContactsRequest.Adderess;
                contact.Email = updateContactsRequest.Email;
                contact.Phone = updateContactsRequest.Phone;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
       public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok("{contact} is deleted successfully");
            }

            return NotFound();

        }


    }
}
