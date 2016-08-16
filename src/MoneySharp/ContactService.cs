using System.Collections.Generic;
using System.Linq;
using MoneySharp.Contract;
using MoneySharp.Internal;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;

namespace MoneySharp
{
    public class ContactService : IContactService
    {
        private readonly IDefaultConnector<Contact, ContactWrapper> _connector;
        private readonly IMapper<Contract.Model.Contact, Contact, Contact> _contactMapper;

        public ContactService(IDefaultConnector<Contact, ContactWrapper> connector, IMapper<Contract.Model.Contact, Contact, Contact> contactMapper)
        {
            _connector = connector;
            _contactMapper = contactMapper;
        }

        /// <summary>
        /// Get all contacts from moneybird
        /// </summary>
        /// <returns>List of all contacts</returns>
        public IList<Contract.Model.Contact> GetAllContacts()
        {
            var allContacts = _connector.GetList();
            return allContacts.Select(_contactMapper.MapToContract).ToList();
        }

        /// <summary>
        /// Create contact in moneybird
        /// </summary>
        /// <param name="contact">Contact to create</param>
        /// <returns>Id of created contact</returns>
        public long CreateContact(Contract.Model.Contact contact)
        {
            var mappedContact = _contactMapper.MapToApi(contact, null);
            var wrappedContact = new ContactWrapper(mappedContact);
            var result = _connector.Create(wrappedContact);

            return result.id;
        }

        /// <summary>
        /// Update contact in moneybird
        /// </summary>
        /// <param name="contactId">Id of contact</param>
        /// <param name="contact">Contact to update</param>
        /// <returns>Contact with mapped properties</returns>
        public Contract.Model.Contact UpdateContact(long contactId, Contract.Model.Contact contact)
        {
            var current = _connector.GetById(contactId);
            var mappedContact = _contactMapper.MapToApi(contact, current);
            var wrappedContact = new ContactWrapper(mappedContact);
            var result = _connector.Update(contactId, wrappedContact);
            return _contactMapper.MapToContract(result);
        }

        /// <summary>
        /// Delete contact from moneybird
        /// </summary>
        /// <param name="id">Id of contact</param>
        public void DeleteContact(long id)
        {
            _connector.Delete(id);
        }
    }
}
