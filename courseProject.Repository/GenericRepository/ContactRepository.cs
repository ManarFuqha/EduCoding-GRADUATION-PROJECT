using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace courseProject.Repository.GenericRepository
{
    public class ContactRepository : GenericRepository1<Contact> , IContactRepository
    {
        private readonly projectDbContext dbContext;

        public ContactRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }



        // add a new contact us message
        public async Task AddMessageContactasync(Contact contact)
        {
            await dbContext.Set<Contact>().AddAsync(contact);
        }


        // retrieve all contact us messages 
        public async Task<IReadOnlyList<Contact>> GetAllContactsAsync()
        {
           
           return await dbContext.contacts.OrderByDescending(x=>x.dateOfAdded).ToListAsync();
        }


        // get a contact us message by it's Id
        public async Task<Contact> getContactByIdAsync(Guid id)
        {
           return await dbContext.contacts.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
