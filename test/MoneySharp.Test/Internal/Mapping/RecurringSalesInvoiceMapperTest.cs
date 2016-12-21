using System;
using System.Collections.Generic;
using FluentAssertions;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace MoneySharp.Test.Internal.Mapping
{
    [TestFixture]
    public class RecurringSalesInvoiceMapperTest
    {
        private AutoMocker _mocker;

        private RecurringSalesInvoiceMapper _mapper;

        private Mock<IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>> _salesInvoiceDetailMapper;
        private Mock<IMapper<Contract.Model.CustomField, CustomField, CustomField>> _customFieldMapper;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _salesInvoiceDetailMapper =
                _mocker.GetMock<IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>>();
            _customFieldMapper = _mocker.GetMock<IMapper<Contract.Model.CustomField, CustomField, CustomField>>();

            _mapper = _mocker.CreateInstance<RecurringSalesInvoiceMapper>();
        }

        [Test]
        public void MapToContract_MapCustomFields_CallsMapper()
        {
            var customField1 = new CustomField() { id = 123456, value = "test1" };
            var customField2 = new CustomField() { id = 123457, value = "test2" };
            var salesInvoice = new RecurringSalesInvoiceGet()
            {
                state = "draft",
                custom_fields = new List<CustomField>()
                {
                    customField1,
                    customField2
                }
            };

            _customFieldMapper.Setup(c => c.MapToContract(customField1)).Returns(new Contract.Model.CustomField());
            _customFieldMapper.Setup(c => c.MapToContract(customField2)).Returns(new Contract.Model.CustomField());

            _mapper.MapToContract(salesInvoice);

            _customFieldMapper.VerifyAll();
        }


        [Test]
        public void MapToContract_MapDetails_CallsMapper()
        {
            var salesInvoiceDetail1 = new SalesInvoiceDetail() { };
            var salesInvoiceDetail2 = new SalesInvoiceDetail() { };
            var salesInvoice = new RecurringSalesInvoiceGet()
            {
                state = "draft",
                details = new List<SalesInvoiceDetail>()
                {
                    salesInvoiceDetail1,
                    salesInvoiceDetail2
                }
            };

            var mappedDetail1 = new Contract.Model.SalesInvoiceDetail();
            var mappedDetail2 = new Contract.Model.SalesInvoiceDetail();

            _salesInvoiceDetailMapper.Setup(c => c.MapToContract(salesInvoiceDetail1)).Returns(mappedDetail1);
            _salesInvoiceDetailMapper.Setup(c => c.MapToContract(salesInvoiceDetail2)).Returns(mappedDetail2);

            var result = _mapper.MapToContract(salesInvoice);

            result.Details.Should().Contain(mappedDetail1);
            result.Details.Should().Contain(mappedDetail2);
            _salesInvoiceDetailMapper.VerifyAll();
        }
        [Test]
        public void MapToContract_MapObject_Correctly()
        {
            var current = new RecurringSalesInvoiceGet();

            var salesInvoice = new RecurringSalesInvoiceGet()
            {
                state = "draft",
                contact_id = 123,
                document_style_id = 12345,
                workflow_id = 12346,
                invoice_id = "AbC",
                due_date = "2016-10-20",
                invoice_date = "2016-11-20",
                id = 1,
                prices_are_incl_tax = true,
                total_price_excl_tax =132,
                total_price_incl_tax = 190,
                total_tax = 58
            };

            var result = _mapper.MapToContract(salesInvoice);

            var expectedResult = new Contract.Model.RecurringSalesInvoice()
            {
                Id = salesInvoice.id,
                DocumentStyleId = salesInvoice.document_style_id,
                WorkflowId = salesInvoice.workflow_id,
                InvoiceId = salesInvoice.invoice_id,
                PriceAreIncludedTax = salesInvoice.prices_are_incl_tax,
                DueDate = new DateTime(2016, 10, 20),
                InvoiceDate = new DateTime(2016, 11, 20),
                ContactId = salesInvoice.contact_id,
                TotalPriceExcludingTax = salesInvoice.total_price_excl_tax,
                TotalPriceIncludingTax = salesInvoice.total_price_incl_tax,
                TotalTax = salesInvoice.total_tax,
            };

            result.ShouldBeEquivalentTo(expectedResult,
                opt => opt
                    .Excluding(p => p.Details)
                    .Excluding(p => p.CustomFields));
        }


        [Test]
        public void MapToApi_MapCustomFields_CallsMapper()
        {
            var current = new RecurringSalesInvoiceGet();
            var customField1 = new Contract.Model.CustomField() { Id = 123456, Value = "test1" };
            var customField2 = new Contract.Model.CustomField() { Id = 123457, Value = "test2" };
            var salesInvoice = new Contract.Model.RecurringSalesInvoice()
            {
                CustomFields = new List<Contract.Model.CustomField>()
                {
                    customField1,
                    customField2
                }
            };

            _customFieldMapper.Setup(c => c.MapToApi(customField1, null)).Returns(new CustomField());
            _customFieldMapper.Setup(c => c.MapToApi(customField2, null)).Returns(new CustomField());

            _mapper.MapToApi(salesInvoice, current);

            _customFieldMapper.VerifyAll();
        }

        [Test]
        public void MapToApi_MapSalesInvoiceDetails_CallsMapper()
        {
            var current = new RecurringSalesInvoiceGet();
            var salesInvoiceDetail1 = new Contract.Model.SalesInvoiceDetail() { };
            var salesInvoiceDetail2 = new Contract.Model.SalesInvoiceDetail() { };
            var salesInvoice = new Contract.Model.RecurringSalesInvoice()
            {
                Details = new List<Contract.Model.SalesInvoiceDetail>()
                {
                    salesInvoiceDetail1,
                    salesInvoiceDetail2
                }
            };

            var salesInvoiceDetail = new SalesInvoiceDetail();
            var salesInvoiceDetai2 = new SalesInvoiceDetail();

            _salesInvoiceDetailMapper.Setup(c => c.MapToApi(salesInvoiceDetail1, null)).Returns(salesInvoiceDetail);
            _salesInvoiceDetailMapper.Setup(c => c.MapToApi(salesInvoiceDetail2, null)).Returns(salesInvoiceDetai2);

            var result = _mapper.MapToApi(salesInvoice, current);

            result.details_attributes.Should().Contain(salesInvoiceDetail);
            result.details_attributes.Should().Contain(salesInvoiceDetai2);
            _salesInvoiceDetailMapper.VerifyAll();
        }

        [Test]
        public void MapToApi_MapObject_Correctly()
        {
            var current = new RecurringSalesInvoiceGet();
            var salesInvoice = new Contract.Model.RecurringSalesInvoice()
            {
                Id = 1,
                DocumentStyleId = 12345,
                WorkflowId = 12346,
                InvoiceId = "ABC",
                PriceAreIncludedTax = true,
                DueDate = new DateTime(2016, 10, 20),
                InvoiceDate = new DateTime(2016, 11, 20),
                ContactId = 123,
                TotalPriceExcludingTax = 123,
                TotalPriceIncludingTax = 150,
                TotalTax = 27,
            };

            var result = _mapper.MapToApi(salesInvoice, current);

            var expectedResult = new RecurringSalesInvoicePost()
            {
                state = "draft",
                contact_id = salesInvoice.ContactId,
                document_style_id = salesInvoice.DocumentStyleId,
                workflow_id = salesInvoice.WorkflowId,
                invoice_id = salesInvoice.InvoiceId,
                due_date = "2016-10-20",
                invoice_date = "2016-11-20",
                id = salesInvoice.Id,
                prices_are_incl_tax = salesInvoice.PriceAreIncludedTax,
                total_price_excl_tax = salesInvoice.TotalPriceExcludingTax,
                total_price_incl_tax = salesInvoice.TotalPriceIncludingTax,
                total_tax = salesInvoice.TotalTax

            };

            result.ShouldBeEquivalentTo(expectedResult,
                opt => opt
                    .Excluding(p => p.details_attributes)
                    .Excluding(p => p.custom_fields_attributes));
        }
    }
}
