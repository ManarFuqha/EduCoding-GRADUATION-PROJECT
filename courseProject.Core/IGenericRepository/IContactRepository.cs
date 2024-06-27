using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IContactRepository :IGenericRepository1<Contact>
    {

        public Task AddMessageContactasync(Contact contact);
        public Task<IReadOnlyList<Contact>> GetAllContactsAsync();
        public Task<Contact> getContactByIdAsync(Guid id);

    }
}
