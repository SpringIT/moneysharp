using FluentAssertions;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using Moq.AutoMock;
using NUnit.Framework;

namespace MoneySharp.Test.Internal.Mapping
{
    public class SalesInvoiceDetailMapperTest
    {
        private AutoMocker _mocker;

        private SalesInvoiceDetailMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _mapper = _mocker.CreateInstance<SalesInvoiceDetailMapper>();
        }

        [Test]
        public void MapToContract_AllFieldsMapped_Correctly()
        {
            var salesInvoiceDetail = new SalesInvoiceDetail()
            {
                row_order = 2,
                product_id= 3,
                amount = "100",
                description = "tests",
                ledger_account_id = 999,
                tax_rate_id = "5",
                price = 123,
                period = "day"
            };

            var result = _mapper.MapToContract(salesInvoiceDetail);
            var expectedResult = new Contract.Model.SalesInvoiceDetail()
            {
                Amount = salesInvoiceDetail.amount,
                Description = salesInvoiceDetail.description,
                LedgerAccountId = salesInvoiceDetail.ledger_account_id,
                Period = salesInvoiceDetail.period,
                Price = salesInvoiceDetail.price,
                ProductId = salesInvoiceDetail.product_id,
                RowOrder = salesInvoiceDetail.row_order,
                TaxRateId = salesInvoiceDetail.tax_rate_id
            };

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void MapToApi_AllFieldsMapped_Correctly()
        {
            var salesInvoiceDetail = new Contract.Model.SalesInvoiceDetail
            {
                RowOrder= 2,
                ProductId = 3,
                Amount = "100",
                Description = "tests",
                LedgerAccountId = 999,
                TaxRateId = "5",
                Price = 123,
                Period = "day"
            };

            var result = _mapper.MapToApi(salesInvoiceDetail, null);
            var expectedResult = new SalesInvoiceDetail
            {
                amount = salesInvoiceDetail.Amount,
                description = salesInvoiceDetail.Description,
                ledger_account_id= salesInvoiceDetail.LedgerAccountId,
                period = salesInvoiceDetail.Period,
                price = salesInvoiceDetail.Price,
                product_id = salesInvoiceDetail.ProductId,
                row_order = salesInvoiceDetail.RowOrder,
                tax_rate_id = salesInvoiceDetail.TaxRateId
            };

            result.ShouldBeEquivalentTo(expectedResult);
        }
    }
}
