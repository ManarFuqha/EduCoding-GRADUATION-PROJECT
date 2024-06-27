using courseProject.Core.Models;
using courseProject.Core.Models.DTO.ContactUsDTO;

namespace courseProject.Services.ContactUs
{
    public interface IContactServices
    {

        public Task AddNewCOntactMessage(CreateMessageContactDTO contact);
        public Task<IReadOnlyList<Contact>> GetAllMessages();
        public Task<Contact> getContactById(Guid id);

    }
}
