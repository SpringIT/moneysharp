using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MoneySharp.Internal.Model;
using RestSharp;

namespace MoneySharp.Internal.Mapping
{
    public class RecurringSalesInvoiceMapper : IMapper<Contract.Model.RecurringSalesInvoice, RecurringSalesInvoiceGet, RecurringSalesInvoicePost>
    {
        private readonly IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>
              _salesInvoiceDetailMapper;

        private readonly IMapper<Contract.Model.CustomField, CustomField, CustomField> _customFieldMapper;

        public RecurringSalesInvoiceMapper(IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail> salesInvoiceDetailMapper, IMapper<Contract.Model.CustomField, CustomField, CustomField> customFieldMapper)
        {
            _salesInvoiceDetailMapper = salesInvoiceDetailMapper;
            _customFieldMapper = customFieldMapper;
        }

        public Contract.Model.RecurringSalesInvoice MapToContract(RecurringSalesInvoiceGet salesInvoice)
        {
            var mapToContract = new Contract.Model.RecurringSalesInvoice
            {
                ContactId = salesInvoice.contact_id,
                CustomFields = salesInvoice.custom_fields.Select(_customFieldMapper.MapToContract).ToList(),
                Details = salesInvoice.details.Select(_salesInvoiceDetailMapper.MapToContract).ToList(),
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
            mapToContract.FrequencyType = GetContractFrequency(salesInvoice.frequency_type);
            mapToContract.Frequency = salesInvoice.frequency;
            mapToContract.StartDate = salesInvoice.start_date;
            mapToContract.LastDate = salesInvoice.last_date;
            return mapToContract;
        }

        public RecurringSalesInvoicePost MapToApi(Contract.Model.RecurringSalesInvoice salesInvoice, RecurringSalesInvoiceGet current)
        {
            var returnValue = new RecurringSalesInvoicePost();
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
            returnValue.details_attributes = salesInvoice.Details?.Select(c => _salesInvoiceDetailMapper.MapToApi(c, null)).ToList() ?? new List<SalesInvoiceDetail>();
            returnValue.invoice_id = salesInvoice.InvoiceId;
            returnValue.id = salesInvoice.Id;
            returnValue.total_price_excl_tax = salesInvoice.TotalPriceExcludingTax;
            returnValue.total_price_incl_tax = salesInvoice.TotalPriceIncludingTax;
            returnValue.total_tax = salesInvoice.TotalTax;
            returnValue.workflow_id = salesInvoice.WorkflowId;
            returnValue.contact_id = salesInvoice.ContactId;
            returnValue.document_style_id = salesInvoice.DocumentStyleId;
            returnValue.due_date = salesInvoice.DueDate?.ToString("yyyy-MM-dd");
            returnValue.invoice_date = salesInvoice.InvoiceDate?.ToString("yyyy-MM-dd");
            returnValue.prices_are_incl_tax = salesInvoice.PriceAreIncludedTax;
            returnValue.start_date = salesInvoice.StartDate;
            returnValue.last_date = salesInvoice.LastDate;
            returnValue.frequency = salesInvoice.Frequency;
            returnValue.frequency_type = GetApiFrequencyType(salesInvoice.FrequencyType);
            return returnValue;
        }

        private void SetCustomFields(RecurringSalesInvoiceGet current, SalesInvoicePost returnValue)
        {
            var jobject = new JsonObject();
            foreach (var customField in current.custom_fields)
            {
                jobject[current.custom_fields.IndexOf(customField).ToString()] = customField;
            }

            returnValue.custom_fields_attributes = jobject;
        }

        private void SetCustomFields(Contract.Model.RecurringSalesInvoice current, SalesInvoicePost returnValue)
        {
            var jobject = new JsonObject();
            if (current.CustomFields != null)
            {
                foreach (var customField in current.CustomFields)
                {
                    jobject[current.CustomFields.IndexOf(customField).ToString()] = _customFieldMapper.MapToApi(customField, null);
                }
            }
            returnValue.custom_fields_attributes = jobject;
        }

        private string GetApiFrequencyType(Contract.Model.FrequencyType type)
        {
            switch (type)
            {
                case Contract.Model.FrequencyType.Day:
                    return "day";
                case Contract.Model.FrequencyType.Quarter:
                    return "quarter";
                case Contract.Model.FrequencyType.Month:
                    return "month";
                case Contract.Model.FrequencyType.Week:
                    return "week";
                case Contract.Model.FrequencyType.Year:
                    return "year";
                default:
                    throw new NotSupportedException($"FrequencyType: {type} is not supported");
            }
        }

        private Contract.Model.FrequencyType GetContractFrequency(string type)
        {
            switch (type.ToLower())
            {
                case "day":
                    return Contract.Model.FrequencyType.Day;
                case "quarter":
                    return Contract.Model.FrequencyType.Quarter;
                case "month":
                    return Contract.Model.FrequencyType.Month;
                case "year":
                    return Contract.Model.FrequencyType.Year;
                case "week":
                    return Contract.Model.FrequencyType.Week;
                default:
                    throw new NotSupportedException($"FrequencyType: {type} is not supported");
            }
        }
    }
}
