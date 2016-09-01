using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class SalesInvoiceDetailMapper : IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>
    {
        public Contract.Model.SalesInvoiceDetail MapToContract(SalesInvoiceDetail arg)
        {
            return new Contract.Model.SalesInvoiceDetail
            {
                Amount = arg.amount,
                Description = arg.description,
                LedgerAccountId = arg.ledger_account_id,
                Period = arg.period,
                Price = arg.price,
                RowOrder = arg.row_order,
                TaxRateId = arg.tax_rate_id,
                ProductId = arg.product_id
            };
        }

        public SalesInvoiceDetail MapToApi(Contract.Model.SalesInvoiceDetail arg, SalesInvoiceDetail apiGet)
        {
            return new SalesInvoiceDetail
            {
                amount = arg.Amount,
                description = arg.Description,
                ledger_account_id = arg.LedgerAccountId,
                period = arg.Period,
                price = arg.Price,
                row_order = arg.RowOrder,
                tax_rate_id = arg.TaxRateId,
                product_id = arg.ProductId
            };
        }
    }
}
