using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.ContactUsDTO;

namespace courseProject.Services.ContactUs
{
    public class ContactServices : IContactServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ContactServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddNewCOntactMessage(CreateMessageContactDTO contact)
        {
            var contactMapper = mapper.Map <CreateMessageContactDTO , Contact> (contact);
            await unitOfWork.contactRepository.AddMessageContactasync(contactMapper);
            await unitOfWork.contactRepository.saveAsync();
        }

        public async Task<IReadOnlyList<Contact>> GetAllMessages( )
        {
            return await unitOfWork.contactRepository.GetAllContactsAsync();
        }

        public async Task<Contact> getContactById(Guid id)
        {
            return await unitOfWork.contactRepository.getContactByIdAsync(id);
        }
    }
}
