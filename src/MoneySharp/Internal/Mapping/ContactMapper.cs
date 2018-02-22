using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class ContactMapper : IMapper<Contract.Model.Contact, Contact, Contact>
    {
        public Contract.Model.Contact MapToContract(Contact input)
        {
            var map = new Contract.Model.Contact
            {
                Firstname = input.firstname,
                Lastname = input.lastname,
                Company = input.company_name,
                ChamberOfCommerce = input.chamber_of_commerce,
                TaxNumber = input.tax_number,
                SendInvoicesToEmail = input.send_invoices_to_email,
                SendEstimatesToEmail = input.send_estimates_to_email,
                Id = input.id
            };

            if (!string.IsNullOrEmpty(input.address1) || !string.IsNullOrEmpty(input.city) ||
                !string.IsNullOrEmpty(input.zipcode) || !string.IsNullOrEmpty(input.country))
            {
                map.Address = new Contract.Model.Address
                {
                    AddressLine = input.address1,
                    Country = input.country,
                    Place = input.city,
                    PostalCode = input.zipcode
                };
            }

            if (input.sepa_active)
            {
                map.Mandate = new Contract.Model.Mandate
                {
                    AccountName = input.sepa_iban_account_name,
                    Bic = input.sepa_bic,
                    Iban = input.sepa_iban,
                    Mandatecode = input.sepa_mandate_id,
                    SequenceType = input.sepa_sequence_type,
                    SignedMandate = input.sepa_mandate_date
                };
            }

            return map;
        }

        public Contact MapToApi(Contract.Model.Contact data, Contact current)
        {
            if(current == null) current = new Contact();
            current.id = data.Id;
            current.firstname = data.Firstname;
            current.lastname = data.Lastname;
            current.company_name = data.Company;
            current.chamber_of_commerce = data.ChamberOfCommerce;
            current.tax_number = data.TaxNumber;
            current.send_invoices_to_email = data.SendInvoicesToEmail;
            current.send_estimates_to_email = data.SendEstimatesToEmail;

            if (data.Address != null)
            {
                current.address1 = data.Address.AddressLine;
                current.country = data.Address.Country;
                current.city = data.Address.Place;
                current.zipcode = data.Address.PostalCode;
            }
            else
            {
                current.address1 = null;
                current.country = null;
                current.city = null;
                current.zipcode = null;
            }

            if (data.Mandate != null)
            {
                current.sepa_active = true;
                current.sepa_iban_account_name = data.Mandate.AccountName;
                current.sepa_bic = data.Mandate.Bic;
                current.sepa_iban = data.Mandate.Iban;
                current.sepa_mandate_id = data.Mandate.Mandatecode;
                current.sepa_sequence_type = data.Mandate.SequenceType;
                current.sepa_mandate_date =data.Mandate.SignedMandate;
            }
            else
            {
                current.sepa_active = false;
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
