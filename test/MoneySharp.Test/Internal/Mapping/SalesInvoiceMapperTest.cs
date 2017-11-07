using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using CustomField = MoneySharp.Internal.Model.CustomField;
using SalesInvoiceDetail = MoneySharp.Internal.Model.SalesInvoiceDetail;

namespace MoneySharp.Test.Internal.Mapping
{
    public class SalesInvoiceMapperTest
    {
        private AutoMocker _mocker;

        private SalesInvoiceMapper _mapper;

        private Mock<IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>> _salesInvoiceDetailMapper;
        private Mock<IMapper<Contract.Model.CustomField, CustomField, CustomField>> _customFieldMapper;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _salesInvoiceDetailMapper =
                _mocker.GetMock<IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>>();
            _customFieldMapper = _mocker.GetMock<IMapper<Contract.Model.CustomField, CustomField, CustomField>>();

            _mapper = _mocker.CreateInstance<SalesInvoiceMapper>();
        }

        [Test]
        public void MapToContract_MapCustomFields_CallsMapper()
        {
            var customField1 = new CustomField() { id = 123456, value = "test1" };
            var customField2 = new CustomField() { id = 123457, value = "test2" };
            var salesInvoice = new SalesInvoiceGet()
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
            var salesInvoice = new SalesInvoiceGet()
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
            var current = new SalesInvoiceGet();

            var salesInvoice = new SalesInvoiceGet()
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
                total_price_excl_tax = 132,
                total_price_incl_tax = 190,
                total_tax = 58
            };

            var result = _mapper.MapToContract(salesInvoice);

            var expectedResult = new Contract.Model.SalesInvoice()
            {
                State = Contract.Model.SalesInvoiceStatus.Draft,
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

            result.Should().BeEquivalentTo(expectedResult,
                opt => opt
                    .Excluding(p => p.Details)
                    .Excluding(p => p.CustomFields));
        }

        [TestCase(Contract.Model.SalesInvoiceStatus.Draft, "draft")]
        [TestCase(Contract.Model.SalesInvoiceStatus.Late, "late")]
        [TestCase(Contract.Model.SalesInvoiceStatus.Open, "open")]
        [TestCase(Contract.Model.SalesInvoiceStatus.Paid, "paid")]
        public void MapToContract_MapStateEnum_Correctly(Contract.Model.SalesInvoiceStatus output, string input)
        {
            var salesInvoice = new SalesInvoiceGet() { state = input };
            var result = _mapper.MapToContract(salesInvoice);

            result.State.Should().BeEquivalentTo(output);
        }

        [Test]
        public void MapToContract_StateEnumNotExist_ThrowsException()
        {
            var salesInvoice = new SalesInvoiceGet() { state = "test" };
            Action a = () => _mapper.MapToContract(salesInvoice);

            a.Should().Throw<Exception>();
        }

        [Test]
        public void MapToApi_MapCustomFields_CallsMapper()
        {
            var current = new SalesInvoiceGet();
            var customField1 = new Contract.Model.CustomField() { Id = 123456, Value = "test1" };
            var customField2 = new Contract.Model.CustomField() { Id = 123457, Value = "test2" };
            var salesInvoice = new Contract.Model.SalesInvoice()
            {
                State = Contract.Model.SalesInvoiceStatus.Draft,
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
            var current = new SalesInvoiceGet();
            var salesInvoiceDetail1 = new Contract.Model.SalesInvoiceDetail() { };
            var salesInvoiceDetail2 = new Contract.Model.SalesInvoiceDetail() { };
            var salesInvoice = new Contract.Model.SalesInvoice()
            {
                State = Contract.Model.SalesInvoiceStatus.Draft,
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
            var current = new SalesInvoiceGet();
            var salesInvoice = new Contract.Model.SalesInvoice()
            {
                State = Contract.Model.SalesInvoiceStatus.Draft,
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

            var expectedResult = new SalesInvoicePost()
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

            result.Should().BeEquivalentTo(expectedResult,
                opt => opt
                    .Excluding(p => p.details_attributes)
                    .Excluding(p => p.custom_fields_attributes));
        }

        [TestCase(Contract.Model.SalesInvoiceStatus.Draft, "draft")]
        [TestCase(Contract.Model.SalesInvoiceStatus.Late, "late")]
        [TestCase(Contract.Model.SalesInvoiceStatus.Open, "open")]
        [TestCase(Contract.Model.SalesInvoiceStatus.Paid, "paid")]
        public void MapToApi_MapStateEnum_Correctly(Contract.Model.SalesInvoiceStatus input, string output)
        {
            var current = new SalesInvoiceGet();
            var salesInvoice = new Contract.Model.SalesInvoice() { State = input };
            var result = _mapper.MapToApi(salesInvoice, current);

            result.state.Should().BeEquivalentTo(output);
        }

        [Test]
        public void MapToApi_StateEnumNotExist_ThrowsException()
        {
            var current = new SalesInvoiceGet();
            var salesInvoice = new Contract.Model.SalesInvoice() { State = (Contract.Model.SalesInvoiceStatus)1234 };
            Action a = () => _mapper.MapToApi(salesInvoice, current);

            a.Should().Throw<Exception>();
        }
    }
}
