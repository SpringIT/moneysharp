using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class SalesInvoiceDetailMapper : IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>
    {
        public Contract.Model.SalesInvoiceDetail MapToContract(SalesInvoiceDetail input)
        {
            return new Contract.Model.SalesInvoiceDetail
            {
                Amount = input.amount,
                Description = input.description,
                LedgerAccountId = input.ledger_account_id,
                Period = input.period,
                Price = input.price,
                RowOrder = input.row_order,
                TaxRateId = input.tax_rate_id,
                ProductId = input.product_id
            };
        }

        public SalesInvoiceDetail MapToApi(Contract.Model.SalesInvoiceDetail data, SalesInvoiceDetail apiGet)
        {
            return new SalesInvoiceDetail
            {
                amount = data.Amount,
                description = data.Description,
                ledger_account_id = data.LedgerAccountId,
                period = data.Period,
                price = data.Price,
                row_order = data.RowOrder,
                tax_rate_id = data.TaxRateId,
                product_id = data.ProductId
            };
        }
    }
}
