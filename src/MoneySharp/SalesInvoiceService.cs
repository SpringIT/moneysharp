﻿using System.Collections.Generic;
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
        private readonly IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper> _salesInvoiceConnector;
        private readonly IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost> _salesInvoiceMapper;

        public SalesInvoiceService(IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper> salesInvoiceConnector, IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost> salesInvoiceMapper)
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
    }
}
