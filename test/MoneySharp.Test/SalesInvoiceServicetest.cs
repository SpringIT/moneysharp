using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class SalesInvoiceServiceTest
    {
        private SalesInvoiceService _invoiceService;

        private AutoMocker _mocker;

        private Mock<IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper>> _defaultConnector;
        private Mock<IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost>> _mapper;


        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _defaultConnector = _mocker.GetMock<IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper>>();
            _mapper =
                _mocker.GetMock<IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost>>();

            _invoiceService = _mocker.CreateInstance<SalesInvoiceService>();
        }

        [Test]
        public void Get_CallsGetList_ReturnsMappedObjects()
        {
            var salesInvoice = new SalesInvoiceGet();
            _defaultConnector.Setup(c => c.GetList()).Returns(new List<SalesInvoiceGet>() { salesInvoice });

            var mappedItem = new Contract.Model.SalesInvoice();
            _mapper.Setup(c => c.MapToContract(salesInvoice)).Returns(mappedItem);

            var items = _invoiceService.Get();

            items.Should().Contain(mappedItem);
        }


        [Test]
        public void GetById_CallsGetById_ReturnsMappedObjects()
        {
            var id = 1234;
            var salesInvoice = new SalesInvoiceGet();
            _defaultConnector.Setup(c => c.GetById(id)).Returns(salesInvoice);

            var mappedItem = new Contract.Model.SalesInvoice();
            _mapper.Setup(c => c.MapToContract(salesInvoice)).Returns(mappedItem);

            var item = _invoiceService.GetById(id);

            item.ShouldBeEquivalentTo(mappedItem);
        }

        [Test]
        public void Create_MapsToApi_CallsConnectorWithWrapper()
        {
            var salesInvoice = new Contract.Model.SalesInvoice();
            var update = new SalesInvoicePost();
            var resultContact = new SalesInvoiceGet() { id = 1234 };

            _mapper.Setup(c => c.MapToApi(salesInvoice, null)).Returns(update);
            _defaultConnector.Setup(c => c.Create(It.Is<SalesInvoiceWrapper>(v => v.sales_invoice == update))).Returns(resultContact);
            _mapper.Setup(c => c.MapToContract(resultContact)).Returns(new Contract.Model.SalesInvoice() { Id = resultContact.id});

            var result = _invoiceService.Create(salesInvoice);

            result.Id.Should().Be(resultContact.id);
            _defaultConnector.Verify(c => c.Create(It.Is<SalesInvoiceWrapper>(v => v.sales_invoice == update)), Times.Once);
        }

        [Test]
        public void Update_GetAndUpdate_CallsConnectorWithUpdate()
        {
            var existing = new SalesInvoiceGet() { id = 1234, };
            var data = new Contract.Model.SalesInvoice() { Id = existing.id };
            var existingMapped = new SalesInvoicePost();
            var saveResult = new SalesInvoiceGet();
            var mappedResult = new Contract.Model.SalesInvoice() { ContactId = 1234 };

            _defaultConnector.Setup(c => c.GetById(existing.id)).Returns(existing);
            _mapper.Setup(c => c.MapToApi(data, existing)).Returns(existingMapped);
            _defaultConnector.Setup(c => c.Update(data.Id, It.Is<SalesInvoiceWrapper>(v => v.sales_invoice == existingMapped))).Returns(saveResult);
            _mapper.Setup(c => c.MapToContract(saveResult)).Returns(mappedResult);

            var result = _invoiceService.Update(data.Id, data);

            result.ShouldBeEquivalentTo(mappedResult);
            _defaultConnector.Verify(c => c.Update(data.Id, It.Is<SalesInvoiceWrapper>(v => v.sales_invoice == existingMapped)), Times.Once);
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
