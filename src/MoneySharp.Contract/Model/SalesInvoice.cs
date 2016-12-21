using System;
using System.Collections.Generic;

namespace MoneySharp.Contract.Model
{
    public class SalesInvoice
    {
        public long Id { get; set; }
        public long ContactId { get; set; }
        public long DocumentStyleId { get; set; }
        public long WorkflowId { get; set; }
        public bool PriceAreIncludedTax { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalPriceExcludingTax { get; set; }
        public decimal TotalPriceIncludingTax { get; set; }
        public decimal TotalTax { get; set; }
        public string InvoiceId { get; set; }
        public string Url { get; set; }
        public SalesInvoiceStatus State { get; set; }
        public IList<SalesInvoiceDetail> Details { get; set; }
        public IList<CustomField> CustomFields { get; set; }
    }
}