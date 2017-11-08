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
    public class RecurringSalesInvoiceServiceTest
    {
        private RecurringSalesInvoiceService _invoiceService;

        private AutoMocker _mocker;

        private Mock<IDefaultConnector<RecurringSalesInvoiceGet, RecurringSalesInvoiceWrapper>> _defaultConnector;
        private Mock<IMapper<Contract.Model.RecurringSalesInvoice, RecurringSalesInvoiceGet, RecurringSalesInvoicePost>> _mapper;


        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _defaultConnector = _mocker.GetMock<IDefaultConnector<RecurringSalesInvoiceGet, RecurringSalesInvoiceWrapper>>();
            _mapper =
                _mocker.GetMock<IMapper<Contract.Model.RecurringSalesInvoice, RecurringSalesInvoiceGet, RecurringSalesInvoicePost>>();

            _invoiceService = _mocker.CreateInstance<RecurringSalesInvoiceService>();
        }

        [Test]
        public void Get_CallsGetList_ReturnsMappedObjects()
        {
            var salesInvoice = new RecurringSalesInvoiceGet();
            _defaultConnector.Setup(c => c.GetList()).Returns(new List<RecurringSalesInvoiceGet>() { salesInvoice });

            var mappedItem = new Contract.Model.RecurringSalesInvoice();
            _mapper.Setup(c => c.MapToContract(salesInvoice)).Returns(mappedItem);

            var items = _invoiceService.Get();

            items.Should().Contain(mappedItem);
        }


        [Test]
        public void GetById_CallsGetById_ReturnsMappedObjects()
        {
            var id = 1234;
            var salesInvoice = new RecurringSalesInvoiceGet();
            _defaultConnector.Setup(c => c.GetById(id)).Returns(salesInvoice);

            var mappedItem = new Contract.Model.RecurringSalesInvoice();
            _mapper.Setup(c => c.MapToContract(salesInvoice)).Returns(mappedItem);

            var item = _invoiceService.GetById(id);

            item.Should().BeEquivalentTo(mappedItem);
        }

        [Test]
        public void Create_MapsToApi_CallsConnectorWithWrapper()
        {
            var salesInvoice = new Contract.Model.RecurringSalesInvoice();
            var update = new RecurringSalesInvoicePost();
            var resultContact = new RecurringSalesInvoiceGet() { id = 1234 };

            _mapper.Setup(c => c.MapToApi(salesInvoice, null)).Returns(update);
            _defaultConnector.Setup(c => c.Create(It.Is<RecurringSalesInvoiceWrapper>(v => v.recurring_sales_invoice == update))).Returns(resultContact);
            _mapper.Setup(c => c.MapToContract(resultContact)).Returns(new Contract.Model.RecurringSalesInvoice() { Id = resultContact.id });

            var result = _invoiceService.Create(salesInvoice);

            result.Id.Should().Be(resultContact.id);
            _defaultConnector.Verify(c => c.Create(It.Is<RecurringSalesInvoiceWrapper>(v => v.recurring_sales_invoice == update)), Times.Once);
        }

        [Test]
        public void Update_GetAndUpdate_CallsConnectorWithUpdate()
        {
            var existing = new RecurringSalesInvoiceGet() { id = 1234, };
            var data = new Contract.Model.RecurringSalesInvoice() { Id = existing.id };
            var existingMapped = new RecurringSalesInvoicePost();
            var saveResult = new RecurringSalesInvoiceGet();
            var mappedResult = new Contract.Model.RecurringSalesInvoice() { ContactId = 1234 };

            _defaultConnector.Setup(c => c.GetById(existing.id)).Returns(existing);
            _mapper.Setup(c => c.MapToApi(data, existing)).Returns(existingMapped);
            _defaultConnector.Setup(c => c.Update(data.Id, It.Is<RecurringSalesInvoiceWrapper>(v => v.recurring_sales_invoice == existingMapped))).Returns(saveResult);
            _mapper.Setup(c => c.MapToContract(saveResult)).Returns(mappedResult);

            var result = _invoiceService.Update(data.Id, data);

            result.Should().BeEquivalentTo(mappedResult);
            _defaultConnector.Verify(c => c.Update(data.Id, It.Is<RecurringSalesInvoiceWrapper>(v => v.recurring_sales_invoice == existingMapped)), Times.Once);
        }

        [Test]
        public void Delete_TryToDelete_CallsConnectorWithDelete()
        {
            var id = 1234;

            _invoiceService.Delete(id);

            _defaultConnector.Verify(c => c.Delete(id), Times.Once);
        }
    }
}
