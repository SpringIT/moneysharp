using System.Linq;
using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class SalesInvoiceMapper : IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost>
    {
        public Contract.Model.SalesInvoice MapToContract(SalesInvoiceGet salesInvoice)
        {
            var mapToContract = new Contract.Model.SalesInvoice();

            mapToContract.Url = salesInvoice.url;
            mapToContract.ContactId = salesInvoice.contact_id;
            mapToContract.CustomFields = salesInvoice.custom_fields.Select(GetCustomField).ToList();
            mapToContract.Details = salesInvoice.details.Select(GetDetail).ToList();

            return mapToContract;
        }

        public SalesInvoicePost MapToApi(Contract.Model.SalesInvoice  salesInvoice, SalesInvoiceGet current)
        {
            var returnValue = new SalesInvoicePost();
            if (current != null)
            {
                //Todo copy properties
            }


            return returnValue;
        }

        private Contract.Model.SalesInvoiceDetail GetDetail(SalesInvoiceDetail arg)
        {
            return new Contract.Model.SalesInvoiceDetail
            {
                Amount = arg.amount,
                Description = arg.description,
                LedgerId = arg.ledger_account_id,
                Period = arg.period,
                Price = arg.price,
                RowOrder = arg.row_order,
                Tax = arg.tax_rate_id
            };
        }

        private Contract.Model.CustomField GetCustomField(CustomField arg)
        {
            return new Contract.Model.CustomField() { Value = arg.value, Id = arg.id };
        }
    }


}