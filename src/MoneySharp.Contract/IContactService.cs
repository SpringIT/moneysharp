using System.Collections;
using System.Collections.Generic;
using MoneySharp.Contract.Model;

namespace MoneySharp.Contract
{
    public interface IContactService
    {
        IList<Contact> GetAllContacts();
        long CreateContact(Contact contact);
        Contact UpdateContact(long contactId, Contact contact);


    }
}
