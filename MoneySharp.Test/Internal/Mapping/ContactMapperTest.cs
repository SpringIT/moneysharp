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

        public void MapToContract_FullMapping()
        {
            var contact = new Contact()
            {
                address1 = "test",
                address2 = "test2",
                zipcode = "zipcode",
                country = "nl",
                city = "city",
                id = 1234,
                sepa_active = true,
                firstname = "first",
                lastname = "second",
                email = "test@test.nl",
                company_name = "test",
                sepa_mandate_date = DateTime.Now,
                sepa_bic = "Bic",
                sepa_iban_account_name = "sepName",
                sepa_mandate_id = "12345",
                sepa_sequence_type = "sequence",
                sepa_iban = "iban",
                attention = "attention",
                chamber_of_commerce = "12344",
                created_at = DateTime.Now,
                phone = "phone",
                customer_id = "123",
                tax_number = "Tax",
            };

            var expectedResult = new Contract.Model.Contact
            {
                Address = new Address
                {
                    Country = "nl",
                    AddressLine = "test",
                    Place = "city",
                    PostalCode = "zipcode"
                },
                Mandate =
                    new Mandate
                    {
                        Iban = "iban",
                        AccountName = "sepName",
                        Bic = "Bic",
                        SignedMandate = contact.sepa_mandate_date,
                        SequenceType = "sequence",
                        Mandatecode = "12345"
                    },
                Id = 1234,
                Firstname = "first",
                Lastname = "second",
                Email = "test@test.nl",
                Company = "test"
            };

            var result = _mapper.MapToContract(contact);

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void MapToContract_SepaEnabledFalse_NoMandateObject()
        {
            var contact = new Contact
            {
                id = 1234,
                sepa_active = false,
                sepa_mandate_date = DateTime.Now,
                sepa_bic = "Bic",
                sepa_iban_account_name = "sepName",
                sepa_mandate_id = "12345",
                sepa_sequence_type = "sequence",
                sepa_iban = "iban",
            };

            var expectedResult = new Contract.Model.Contact
            {
                Address = null,
                Mandate = null,
                Id = 1234,
            };

            var result = _mapper.MapToContract(contact);

            result.ShouldBeEquivalentTo(expectedResult);
        }


        [Test]
        public void MapToContract_NoAddressInfo_AddressIsNull()
        {
            var contact = new Contact
            {
                id = 1234,
            };

            var expectedResult = new Contract.Model.Contact
            {
                Address = null,
                Id = 1234,
            };

            var result = _mapper.MapToContract(contact);

            result.ShouldBeEquivalentTo(expectedResult);

        }
    }
}
