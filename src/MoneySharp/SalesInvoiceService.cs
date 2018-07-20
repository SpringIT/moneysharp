using System.Collections.Generic;
using System.Linq;
using MoneySharp.Contract;
using MoneySharp.Internal;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;

namespace MoneySharp
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceConnector<SalesInvoiceGet, SalesInvoiceWrapper> _salesInvoiceConnector;
        private readonly IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost> _salesInvoiceMapper;

        public SalesInvoiceService(ISalesInvoiceConnector<SalesInvoiceGet, SalesInvoiceWrapper> salesInvoiceConnector, IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost> salesInvoiceMapper)
        {
            _salesInvoiceConnector = salesInvoiceConnector;
            _salesInvoiceMapper = salesInvoiceMapper;
        }

        public IList<Contract.Model.SalesInvoice> Get()
        {
            var salesInvoices = _salesInvoiceConnector.GetList();
            return salesInvoices.Select(_salesInvoiceMapper.MapToContract).ToList();
        }

        public Contract.Model.SalesInvoice GetById(long id)
        {
            var salesInvoice = _salesInvoiceConnector.GetById(id);
            return _salesInvoiceMapper.MapToContract(salesInvoice);
        }

        public Contract.Model.SalesInvoice Create(Contract.Model.SalesInvoice salesInvoice)
        {
            var salesInvoicePost = _salesInvoiceMapper.MapToApi(salesInvoice, null);
            var wrappedSalesInvoice = new SalesInvoiceWrapper(salesInvoicePost);
            var createdSalesInvoice = _salesInvoiceConnector.Create(wrappedSalesInvoice);
            return _salesInvoiceMapper.MapToContract(createdSalesInvoice);
        }

        public Contract.Model.SalesInvoice Update(long id, Contract.Model.SalesInvoice salesInvoice)
        {
            var currentSalesInvoice = _salesInvoiceConnector.GetById(id);
            var salesInvoicePost = _salesInvoiceMapper.MapToApi(salesInvoice, currentSalesInvoice);
            var wrappedSalesInvoice = new SalesInvoiceWrapper(salesInvoicePost);
            var updatedSalesInvoice = _salesInvoiceConnector.Update(id, wrappedSalesInvoice);
            return _salesInvoiceMapper.MapToContract(updatedSalesInvoice);
        }

        public void Delete(long id)
        {
            _salesInvoiceConnector.Delete(id);
        }

        public void Send(long id, Contract.Model.SendInvoice invoice)
        {
            var sendInvoice = new SendInvoice
            {
                delivery_method = invoice.DeliveryMethod,
                delivery_ubl = invoice.DeliveryUbl,
                email_address = invoice.EmailAddress,
                email_message = invoice.EmailMessage,
                invoice_date = invoice.InvoiceDate,
                mergeable = invoice.Mergeable,
                sending_scheduled = invoice.SendingScheduled
            };
            _salesInvoiceConnector.Send(id, sendInvoice);
        }

        public void CreatePayment(long id, Contract.Model.Payment payment)
        {
            var sendInvoice = new Payment
            {
                price = payment.Price,
                payment_date = payment.PaymentDate.ToString("yyyy-MM-dd"),
                price_base = payment.PriceBase,
                financial_account_id = payment.FinancialAccountId,
                financial_mutation_id = payment.FinancialMutationId
            };
            _salesInvoiceConnector.CreatePayment(id, sendInvoice);
        } 
        
        public void DeletePayment(long id, long paymentId)
        {
            _salesInvoiceConnector.DeletePayment(id, paymentId);
        }
    }
}
