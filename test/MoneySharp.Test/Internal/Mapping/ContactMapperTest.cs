using System;
using FluentAssertions;
using MoneySharp.Contract.Model;
using MoneySharp.Internal.Mapping;
using Moq.AutoMock;
using NUnit.Framework;
using Contact = MoneySharp.Internal.Model.Contact;

namespace MoneySharp.Test.Internal.Mapping
{
    public class ContactMapperTest
    {
        private AutoMocker _mocker;

        private ContactMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _mapper = _mocker.CreateInstance<ContactMapper>();
        }

        [Test]
        public void MapToContract_AddressEmpty_AddressIsNull()
        {
            var contact = new Contact
            {
                send_invoices_to_email = "test@test.nl",
                customer_id = "abc",
                lastname = "lastname"
            };

            var mapped = _mapper.MapToContract(contact);

            mapped.Address.Should().Be(null);
        }

        [Test]
        public void MapToContract_MandateNotActivated_MandateEmpty()
        {
            var contact = new Contact()
            {
                sepa_active = false,
                sepa_mandate_id = "abcd"
            };

            var mapped = _mapper.MapToContract(contact);

            mapped.Mandate.Should().Be(null);
        }

        [Test]
        public void MapToContract_MapAllProperties_Correctly()
        {
            var contact = new Contact()
            {
                id = 123,
                address1 = "street",
                country = "NL",
                city = "city",
                zipcode = "zipcode",
                firstname = "first",
                lastname = "last",
                company_name = "company",
                sepa_bic = "bic",
                sepa_iban = "iban",
                sepa_mandate_date = new DateTime(2016,1,9),
                phone = "0123456789",
                tax_number = "tax",
                chamber_of_commerce = "12345",
                sepa_sequence_type = "first",
                sepa_active = true,
                sepa_mandate_id = "abcd"
            };

            var mapped = _mapper.MapToContract(contact);
            var expectedResult = new Contract.Model.Contact
            {
                Id = contact.id,
                Address = new Address
                {
                    AddressLine = contact.address1,
                    Country = contact.country,
                    Place = contact.city,
                    PostalCode = contact.zipcode,
                },
                Company = contact.company_name,
                ChamberOfCommerce = contact.chamber_of_commerce,
                TaxNumber = contact.tax_number,
                SendInvoicesToEmail = contact.send_invoices_to_email,
                Firstname = contact.firstname,
                Lastname = contact.lastname,
                Mandate =  new Mandate
                {
                    AccountName = contact.sepa_iban_account_name,
                    Bic = contact.sepa_bic,
                    Iban = contact.sepa_iban,
                    Mandatecode = contact.sepa_mandate_id,
                    SequenceType = contact.sepa_sequence_type,
                    SignedMandate = contact.sepa_mandate_date
                }
            };
            mapped.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void MapToApi_AddressEmpty_AddressFieldsEmpty()
        {
            var contact = new Contract.Model.Contact();

            var c =  _mapper.MapToApi(contact, null);

            c.address1.Should().BeNull();
            c.zipcode.Should().BeNull();
            c.city.Should().BeNull();
            c.country.Should().BeNull();
        }

        [Test]
        public void MapToApi_MandateEmpty_MandateFieldsEmpty()
        {
            var contact = new Contract.Model.Contact();

            var c = _mapper.MapToApi(contact, null);

            c.sepa_iban_account_name.Should().BeNull();
            c.sepa_bic.Should().BeNull();
            c.sepa_iban.Should().BeNull();
            c.sepa_mandate_id.Should().BeNull();
            c.sepa_sequence_type.Should().BeNull();
            c.sepa_mandate_date.Should().Be(null);
        }

        [Test]
        public void MapToApi_MapAllProperties_Corretly()
        {
            var contact = new Contract.Model.Contact()
            {
                Id = 1234,
                Company = "company",
                Mandate = new Mandate()
                {
                    Iban = "iban",
                    SignedMandate = new DateTime(2016, 01, 1),
                    Bic = "bic",
                    SequenceType = "first",
                    Mandatecode = "mandate",
                    AccountName = "accountname",
                },
                ChamberOfCommerce = "COC",
                TaxNumber = "TAX",
                SendInvoicesToEmail = "email",
                Firstname = "firstname",
                Lastname = "lastname",
                Address = new Address()
                {
                    PostalCode = "postalcode",
                    Place = "place",
                    AddressLine = "addressLine",
                    Country = "country"
                }
            };

            var mapped = _mapper.MapToApi(contact, null);

            var expected = new Contact()
            {
                id = contact.Id,
                company_name = contact.Company,
                chamber_of_commerce = contact.ChamberOfCommerce,
                tax_number = contact.TaxNumber,
                send_invoices_to_email = contact.SendInvoicesToEmail,
                lastname = contact.Lastname,
                firstname = contact.Firstname,
                zipcode= contact.Address.PostalCode,
                country = contact.Address.Country,
                address1 = contact.Address.AddressLine,
                city = contact.Address.Place,
                sepa_active = true,
                sepa_bic = contact.Mandate.Bic,
                sepa_iban_account_name = contact.Mandate.AccountName,
                sepa_iban = contact.Mandate.Iban,
                sepa_mandate_id = contact.Mandate.Mandatecode,
                sepa_sequence_type= contact.Mandate.SequenceType,
                sepa_mandate_date = contact.Mandate.SignedMandate
            };

            mapped.Should().BeEquivalentTo(expected);
        }
    }
}
