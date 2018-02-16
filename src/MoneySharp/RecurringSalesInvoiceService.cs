using System.Collections.Generic;
using System.Linq;
using MoneySharp.Contract;
using MoneySharp.Internal;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;

namespace MoneySharp
{
    public class RecurringSalesInvoiceService : IRecurringSalesInvoiceService
    {
        private readonly IRecurringSalesInvoiceConnector<RecurringSalesInvoiceGet, RecurringSalesInvoiceWrapper> _recurringSalesInvoiceConnector;
        private readonly IMapper<Contract.Model.RecurringSalesInvoice, RecurringSalesInvoiceGet, RecurringSalesInvoicePost> _recurringSalesInvoiceMapper;

        public RecurringSalesInvoiceService(
            IRecurringSalesInvoiceConnector<RecurringSalesInvoiceGet, RecurringSalesInvoiceWrapper> recurringSalesInvoiceConnector,
            IMapper<Contract.Model.RecurringSalesInvoice, RecurringSalesInvoiceGet, RecurringSalesInvoicePost>
                recurringSalesInvoiceMapper)
        {
            _recurringSalesInvoiceConnector = recurringSalesInvoiceConnector;
            _recurringSalesInvoiceMapper = recurringSalesInvoiceMapper;
        }

        public IList<Contract.Model.RecurringSalesInvoice> Get()
        {
            var salesInvoices = _recurringSalesInvoiceConnector.GetList();
            return salesInvoices.Select(_recurringSalesInvoiceMapper.MapToContract).ToList();
        }

        public Contract.Model.RecurringSalesInvoice GetById(long id)
        {
            var salesInvoice = _recurringSalesInvoiceConnector.GetById(id);
            return _recurringSalesInvoiceMapper.MapToContract(salesInvoice);
        }

        public IList<Contract.Model.RecurringSalesInvoice> GetByContactId(long id)
        {
            var salesInvoices = _recurringSalesInvoiceConnector.GetByContactId(id);
            return salesInvoices.Select(_recurringSalesInvoiceMapper.MapToContract).ToList();
        }

        public Contract.Model.RecurringSalesInvoice Create(Contract.Model.RecurringSalesInvoice salesInvoice)
        {
            var salesInvoicePost = _recurringSalesInvoiceMapper.MapToApi(salesInvoice, null);
            var wrappedSalesInvoice = new RecurringSalesInvoiceWrapper(salesInvoicePost);
            var createdSalesInvoice = _recurringSalesInvoiceConnector.Create(wrappedSalesInvoice);
            return _recurringSalesInvoiceMapper.MapToContract(createdSalesInvoice);
        }

        public Contract.Model.RecurringSalesInvoice Update(long id, Contract.Model.RecurringSalesInvoice salesInvoice)
        {
            var currentSalesInvoice = _recurringSalesInvoiceConnector.GetById(id);
            var salesInvoicePost = _recurringSalesInvoiceMapper.MapToApi(salesInvoice, currentSalesInvoice);
            var wrappedSalesInvoice = new RecurringSalesInvoiceWrapper(salesInvoicePost);
            var updatedSalesInvoice = _recurringSalesInvoiceConnector.Update(id, wrappedSalesInvoice);
            return _recurringSalesInvoiceMapper.MapToContract(updatedSalesInvoice);
        }

        public void Delete(long id)
        {
            _recurringSalesInvoiceConnector.Delete(id);
        }
    }
}
