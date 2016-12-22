using System.Collections;
using System.Collections.Generic;
using MoneySharp.Contract.Model;

namespace MoneySharp.Contract
{
    public interface IContactService
    {
        /// <summary>
        /// Get all contacts from moneybird
        /// </summary>
        /// <returns>List of all contacts</returns>
        IList<Contact> Get();

        /// <summary>
        /// Gets single contact
        /// </summary>
        /// <param name="id">Id of contact</param>
        /// <returns>Contact or else KeyNotFoundException</returns>
        Contact GetById(long id);

        /// <summary>
        /// Create contact in moneybird
        /// </summary>
        /// <param name="contact">Contact to create</param>
        /// <returns>Id of created contact</returns>
        long Create(Contact contact);

        /// <summary>
        /// Update contact in moneybird
        /// </summary>
        /// <param name="id">Id of contact</param>
        /// <param name="contact">Contact to update</param>
        /// <returns>Contact with mapped properties</returns>
        Contact Update(long id, Contact contact);

        /// <summary>
        /// Delete contact from moneybird
        /// </summary>
        /// <param name="id">Id of contact</param>
        void Delete(long id);
    }
}
