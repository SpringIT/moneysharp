using System.Collections.Generic;
using System.Linq;
using MoneySharp.Internal;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;

namespace MoneySharp
{
    public class SalesInvoiceService
    {
        private readonly IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper> _salesInvoiceConnector;
        private readonly IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost> _salesInvoiceMapper;

        public SalesInvoiceService(IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper> salesInvoiceConnector, IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost> salesInvoiceMapper)
        {
            _salesInvoiceConnector = salesInvoiceConnector;
            _salesInvoiceMapper = salesInvoiceMapper;
        }

        /// <summary>
        /// Get all sales invoices from moneybird. Limit is 100 invoices
        /// </summary>
        /// <returns></returns>
        public IList<Contract.Model.SalesInvoice> GetAllSalesInvoices()
        {
            var salesInvoices = _salesInvoiceConnector.GetList();
            return salesInvoices.Select(_salesInvoiceMapper.MapToContract).ToList();
        }

        /// <summary>
        /// Gets single sales invoice
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        /// <returns></returns>
        public Contract.Model.SalesInvoice GetSalesInvoiceById(long id)
        {
            var salesInvoice = _salesInvoiceConnector.GetById(id);
            return _salesInvoiceMapper.MapToContract(salesInvoice);
        }

        /// <summary>
        /// Create new sales invoice in moneybird
        /// </summary>
        /// <param name="salesInvoice">Invoice to create</param>
        /// <returns></returns>
        public Contract.Model.SalesInvoice CreateSalesInvoice(Contract.Model.SalesInvoice salesInvoice)
        {
            var salesInvoicePost = _salesInvoiceMapper.MapToApi(salesInvoice, null);
            var wrappedSalesInvoice = new SalesInvoiceWrapper(salesInvoicePost);
            var createdSalesInvoice = _salesInvoiceConnector.Create(wrappedSalesInvoice);
            return _salesInvoiceMapper.MapToContract(createdSalesInvoice);
        }

        /// <summary>
        /// Updates sales invoice in momenybird
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        /// <param name="salesInvoice">Sales invoice to update</param>
        /// <returns></returns>
        public Contract.Model.SalesInvoice UpdateSalesInvoice(long id, Contract.Model.SalesInvoice salesInvoice)
        {
            var currentSalesInvoice = _salesInvoiceConnector.GetById(id);
            var salesInvoicePost = _salesInvoiceMapper.MapToApi(salesInvoice, currentSalesInvoice);
            var wrappedSalesInvoice = new SalesInvoiceWrapper(salesInvoicePost);
            var updatedSalesInvoice = _salesInvoiceConnector.Update(id, wrappedSalesInvoice);
            return _salesInvoiceMapper.MapToContract(updatedSalesInvoice);
        }

        /// <summary>
        /// Delete invoice from moneybird
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        public void DeleteSalesInvoice(long id)
        {
            _salesInvoiceConnector.Delete(id);
        }
    }
}
