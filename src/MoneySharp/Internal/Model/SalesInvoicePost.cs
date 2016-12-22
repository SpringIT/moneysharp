﻿using System.Collections.Generic;
using RestSharp;

namespace MoneySharp.Internal.Model
{
    public class SalesInvoicePost : SalesInvoice
    {
        public IList<SalesInvoiceDetail> details_attributes { get; set; }
        public JsonObject custom_fields_attributes { get; set; }
    }
}