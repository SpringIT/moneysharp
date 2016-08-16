using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class ContactMapper : IMapper<Contract.Model.Contact, Contact, Contact>
    {
        public Contract.Model.Contact MapToContract(Contact contact)
        {
            var map = new Contract.Model.Contact
            {
                Firstname = contact.firstname,
                Lastname = contact.lastname,
                Company = contact.company_name,
                Email = contact.email,
                Id = contact.id
            };

            if (!string.IsNullOrEmpty(contact.address1) || !string.IsNullOrEmpty(contact.city) ||
                !string.IsNullOrEmpty(contact.zipcode) || !string.IsNullOrEmpty(contact.country))
            {
                map.Address = new Contract.Model.Address
                {
                    AddressLine = contact.address1,
                    Country = contact.country,
                    Place = contact.city,
                    PostalCode = contact.zipcode
                };
            }

            if (contact.sepa_active)
            {
                map.Mandate = new Contract.Model.Mandate
                {
                    AccountName = contact.sepa_iban_account_name,
                    Bic = contact.sepa_bic,
                    Iban = contact.sepa_iban,
                    Mandatecode = contact.sepa_mandate_id,
                    SequenceType = contact.sepa_sequence_type,
                    SignedMandate = contact.sepa_mandate_date
                };
            }

            return map;
        }

        public Contact MapToApi(Contract.Model.Contact contact, Contact current)
        {
            if(current == null) current = new Contact();
            current.firstname = contact.Firstname;
            current.lastname = contact.Lastname;
            current.company_name = contact.Company;
            current.email = contact.Email;

            if (contact.Address != null)
            {
                current.address1 = contact.Address.AddressLine;
                current.country = contact.Address.Country;
                current.city = contact.Address.Place;
                current.zipcode = contact.Address.PostalCode;
            }
            else
            {
                current.address1 = null;
                current.country = null;
                current.city = null;
                current.zipcode = null;
            }

            if (contact.Mandate != null)
            {
                current.sepa_iban_account_name = contact.Mandate.AccountName;
                current.sepa_bic = contact.Mandate.Bic;
                current.sepa_iban = contact.Mandate.Iban;
                current.sepa_mandate_id = contact.Mandate.Mandatecode;
                current.sepa_sequence_type = contact.Mandate.SequenceType;
                current.sepa_mandate_date =contact.Mandate.SignedMandate;
            }
            else
            {
                current.sepa_iban_account_name = null;
                current.sepa_bic = null;
                current.sepa_iban = null;
                current.sepa_mandate_id = null;
                current.sepa_sequence_type = null;
                current.sepa_mandate_date = null;
            }
            return current;
        }
    }
}
