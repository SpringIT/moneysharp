using System.Collections;
using System.Collections.Generic;
using MoneySharp.Contract.Model;

namespace MoneySharp.Contract
{
    public interface IContactService
    {
        IList<Contact> Get();
        long Create(Contact contact);
        Contact Update(long id, Contact contact);
        /// <summary>
        /// Delete contact from moneybird
        /// </summary>
        /// <param name="id">Id of contact</param>
        void DeleteContact(long id);
    }
}
