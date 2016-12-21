using System.Collections.Generic;
using FluentAssertions;
using MoneySharp.Internal;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace MoneySharp.Test
{
    [TestFixture]
    public class ContactServiceTest
    {
        private AutoMocker _mocker;

        private ContactService _contactService;

        private Mock<IDefaultConnector<Contact, ContactWrapper>> _defaultConnector;
        private Mock<IMapper<Contract.Model.Contact, Contact, Contact>> _mapper;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();

            _defaultConnector = _mocker.GetMock<IDefaultConnector<Contact, ContactWrapper>>();
            _mapper = _mocker.GetMock<IMapper<Contract.Model.Contact, Contact, Contact>>();

            _contactService = _mocker.CreateInstance<ContactService>();
        }

        [Test]
        public void Get_CallsGetList_ReturnsMappedObjects()
        {
            var contact = new Contact();
            _defaultConnector.Setup(c => c.GetList()).Returns(new List<Contact>() { contact});

            var mappedItem = new Contract.Model.Contact();
            _mapper.Setup(c => c.MapToContract(contact)).Returns(mappedItem);

            var items = _contactService.Get();

            items.Should().Contain(mappedItem);
        }


        [Test]
        public void GetById_CallsGetById_ReturnsMappedObjects()
        {
            var id = 1234;
            var contact = new Contact();
            _defaultConnector.Setup(c => c.GetById(id)).Returns(contact);

            var mappedItem = new Contract.Model.Contact();
            _mapper.Setup(c => c.MapToContract(contact)).Returns(mappedItem);

            var item = _contactService.GetById(id);

            item.ShouldBeEquivalentTo(mappedItem);
        }

        [Test]
        public void Create_MapsToApi_CallsConnectorWithWrapper()
        {
            var contact = new Contract.Model.Contact();
            var update = new Contact();
            var resultContact = new Contact() { id = 1234 };

            _mapper.Setup(c => c.MapToApi(contact, null)).Returns(update);
            _defaultConnector.Setup(c => c.Create(It.Is<ContactWrapper>(v => v.contact == update))).Returns(resultContact);

            var result = _contactService.Create(contact);

            result.Should().Be(resultContact.id);
            _defaultConnector.Verify(c => c.Create(It.Is<ContactWrapper>(v => v.contact == update)), Times.Once);
        }

        [Test]
        public void Update_GetAndUpdate_CallsConnectorWithUpdate()
        {
            var existing = new Contact() { id = 1234,};
            var data = new Contract.Model.Contact() { Id = existing.id };
            var existingMapped = new Contact();
            var saveResult = new Contact();
            var mappedResult = new Contract.Model.Contact() { Company = "I'm mapped" };

            _defaultConnector.Setup(c => c.GetById(existing.id)).Returns(existing);
            _mapper.Setup(c => c.MapToApi(data, existing)).Returns(existingMapped);
            _defaultConnector.Setup(c => c.Update(data.Id, It.Is<ContactWrapper>(v => v.contact == existingMapped))).Returns(saveResult);
            _mapper.Setup(c => c.MapToContract(saveResult)).Returns(mappedResult);

            var result = _contactService.Update(data.Id, data);

            result.ShouldBeEquivalentTo(mappedResult);
            _defaultConnector.Verify(c => c.Update(data.Id, It.Is<ContactWrapper>(v => v.contact == existingMapped)), Times.Once);
        }

        [Test]
        public void Delete_TryToDelete_CallsConnectorWithDelete()
        {
            var id = 1234;

            _contactService.Delete(id);

            _defaultConnector.Verify(c => c.Delete(id), Times.Once);
        }
    }
}
