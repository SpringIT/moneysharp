using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MoneySharp.Internal.Model;
using RestSharp;

namespace MoneySharp.Internal.Mapping
{
    public class SalesInvoiceMapper : IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost>
    {
        public Contract.Model.SalesInvoice MapToContract(SalesInvoiceGet salesInvoice)
        {
            var mapToContract = new Contract.Model.SalesInvoice
            {
                Url = salesInvoice.url,
                ContactId = salesInvoice.contact_id,
                CustomFields = salesInvoice.custom_fields.Select(GetCustomField).ToList(),
                Details = salesInvoice.details.Select(GetDetail).ToList(),
                InvoiceId = salesInvoice.invoice_id
            };

            mapToContract.ContactId = salesInvoice.contact_id;
            mapToContract.DocumentStyleId = salesInvoice.document_style_id;
            mapToContract.WorkflowId = salesInvoice.workflow_id;
            mapToContract.DueDate = DateTime.Parse(salesInvoice.due_date, CultureInfo.InvariantCulture);
            mapToContract.InvoiceDate = DateTime.Parse(salesInvoice.invoice_date, CultureInfo.InvariantCulture);
            mapToContract.PriceAreIncludedTax = salesInvoice.prices_are_incl_tax;
            mapToContract.TotalTax = salesInvoice.total_tax;
            mapToContract.TotalPriceIncludingTax = salesInvoice.total_price_incl_tax;
            mapToContract.TotalPriceExcludingTax = salesInvoice.total_price_excl_tax;
            mapToContract.Id = salesInvoice.id;
            mapToContract.Url = salesInvoice.url;
            mapToContract.State = GetContractState(salesInvoice.state);

            return mapToContract;
        }

        public SalesInvoicePost MapToApi(Contract.Model.SalesInvoice  salesInvoice, SalesInvoiceGet current)
        {
            var returnValue = new SalesInvoicePost();
            if (current != null)
            {
                SetCustomFields(current, returnValue);
                returnValue.details_attributes = current.details;
                returnValue.contact_id = current.contact_id;
                returnValue.document_style_id = current.document_style_id;
                returnValue.due_date = current.due_date;
                returnValue.id = current.id;
                returnValue.invoice_date = current.invoice_date;
                returnValue.invoice_id = current.invoice_id;
                returnValue.prices_are_incl_tax = current.prices_are_incl_tax;
                returnValue.total_price_excl_tax = current.total_price_excl_tax;
                returnValue.total_price_incl_tax = current.total_price_incl_tax;
                returnValue.state = current.state;
                returnValue.total_tax = current.total_tax;
                returnValue.workflow_id = current.workflow_id;
            }


            SetCustomFields(salesInvoice, returnValue);
            returnValue.details_attributes = salesInvoice.Details?.Select(GetApiDetail).ToList() ?? new List<SalesInvoiceDetail>();
            returnValue.invoice_id = salesInvoice.InvoiceId;
            returnValue.id = salesInvoice.Id;
            returnValue.state = GetApiState(salesInvoice.State);
            returnValue.total_price_excl_tax = salesInvoice.TotalPriceExcludingTax;
            returnValue.total_price_incl_tax = salesInvoice.TotalPriceIncludingTax;
            returnValue.total_tax = salesInvoice.TotalTax;
            returnValue.workflow_id = salesInvoice.WorkflowId;
            returnValue.contact_id = salesInvoice.ContactId;
            returnValue.document_style_id = salesInvoice.DocumentStyleId;
            returnValue.due_date = salesInvoice.DueDate?.ToString("yyyy-MM-dd");
            returnValue.invoice_date = salesInvoice.InvoiceDate?.ToString("yyyy-MM-dd");
            returnValue.prices_are_incl_tax = salesInvoice.PriceAreIncludedTax;
            return returnValue;
        }

        private static void SetCustomFields(SalesInvoiceGet current, SalesInvoicePost returnValue)
        {
            var jobject = new JsonObject();
            foreach (var customField in current.custom_fields)
            {
                jobject[current.custom_fields.IndexOf(customField).ToString()] = customField;
            }

            returnValue.custom_fields_attributes = jobject;
        }

        private static void SetCustomFields(Contract.Model.SalesInvoice current, SalesInvoicePost returnValue)
        {
            var jobject = new JsonObject();
            if (current.CustomFields != null)
            {
                foreach (var customField in current.CustomFields)
                {
                    jobject[current.CustomFields.IndexOf(customField).ToString()] = new CustomField
                    {
                        id = customField.Id,
                        value = customField.Value
                    };
                }
            }
            returnValue.custom_fields_attributes = jobject;
        }

        private SalesInvoiceDetail GetApiDetail(Contract.Model.SalesInvoiceDetail arg)
        {
            return new SalesInvoiceDetail
            {
                amount = arg.Amount,
                description = arg.Description,
                ledger_account_id = arg.LedgerAccountId,
                period = arg.Period,
                price = arg.Price,
                row_order = arg.RowOrder,
                tax_rate_id = arg.TaxRateId
            };
        }

        private Contract.Model.SalesInvoiceDetail GetDetail(SalesInvoiceDetail arg)
        {
            return new Contract.Model.SalesInvoiceDetail
            {
                Amount = arg.amount,
                Description = arg.description,
                LedgerAccountId = arg.ledger_account_id,
                Period = arg.period,
                Price = arg.price,
                RowOrder = arg.row_order,
                TaxRateId = arg.tax_rate_id
            };
        }

        private string GetApiState(Contract.Model.SalesInvoiceStatus state)
        {
            switch (state)
            {
                case Contract.Model.SalesInvoiceStatus.Paid:
                    return "paid";
                case Contract.Model.SalesInvoiceStatus.Draft:
                    return "draft";
                case Contract.Model.SalesInvoiceStatus.Late:
                    return "late";
                case Contract.Model.SalesInvoiceStatus.Open:
                    return "open";
                default:
                    throw new NotSupportedException($"State: {state} is not supported");
            }
        }

        private Contract.Model.SalesInvoiceStatus GetContractState(string state)
        {
            switch (state.ToUpper())
            {
                case "DRAFT":
                    return Contract.Model.SalesInvoiceStatus.Draft;
                case "LATE":
                    return Contract.Model.SalesInvoiceStatus.Late;
                case "OPEN":
                    return Contract.Model.SalesInvoiceStatus.Open;
                case "PAID":
                    return Contract.Model.SalesInvoiceStatus.Paid;
                default:
                    throw new NotSupportedException($"State: {state} is not supported");
            }
        }

        private Contract.Model.CustomField GetCustomField(CustomField arg)
        {
            return new Contract.Model.CustomField() { Value = arg.value, Id = arg.id };
        }
    }


}