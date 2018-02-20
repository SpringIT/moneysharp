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
        private readonly IContactConnector<Contact, ContactWrapper> _connector;
        private readonly IMapper<Contract.Model.Contact, Contact, Contact> _contactMapper;

        public ContactService(IContactConnector<Contact, ContactWrapper> connector, IMapper<Contract.Model.Contact, Contact, Contact> contactMapper)
        {
            _connector = connector;
            _contactMapper = contactMapper;
        }

        public IList<Contract.Model.Contact> Get()
        {
            var allContacts = _connector.GetList();
            return allContacts.Select(_contactMapper.MapToContract).ToList();
        }

        public IList<Contract.Model.Contact> GetBySearch(string search)
        {
            var allContacts = _connector.GetBySearch(search);
            return allContacts.Select(_contactMapper.MapToContract).ToList();
        }

        public IList<Contract.Model.Contact> Get(string search)
        {
            var allContacts = _connector.GetList();
            return allContacts.Select(_contactMapper.MapToContract).ToList();
        }

        public Contract.Model.Contact GetById(long id)
        {
            var result = _connector.GetById(id);
            return _contactMapper.MapToContract(result);
        }

        public long Create(Contract.Model.Contact contact)
        {
            var mappedContact = _contactMapper.MapToApi(contact, null);
            var wrappedContact = new ContactWrapper(mappedContact);
            var result = _connector.Create(wrappedContact);
            return result.id.Value;
        }

        public Contract.Model.Contact Update(long id, Contract.Model.Contact contact)
        {
            var current = _connector.GetById(id);
            var mappedContact = _contactMapper.MapToApi(contact, current);
            var wrappedContact = new ContactWrapper(mappedContact);
            var result = _connector.Update(id, wrappedContact);
            return _contactMapper.MapToContract(result);
        }

        public void Delete(long id)
        {
            _connector.Delete(id);
        }
    }
}
