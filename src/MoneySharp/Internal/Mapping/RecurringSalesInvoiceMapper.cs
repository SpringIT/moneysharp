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

        public Contract.Model.RecurringSalesInvoice MapToContract(RecurringSalesInvoiceGet input)
        {
            var mapToContract = new Contract.Model.RecurringSalesInvoice
            {
                ContactId = input.contact_id,
                CustomFields = input.custom_fields != null ? input.custom_fields.Select(_customFieldMapper.MapToContract).ToList() : new List<Contract.Model.CustomField>(),
                Details = input.details != null ? input.details.Select(_salesInvoiceDetailMapper.MapToContract).ToList(): new List<Contract.Model.SalesInvoiceDetail>(),
                InvoiceId = input.invoice_id
            };

            mapToContract.ContactId = input.contact_id;
            mapToContract.DocumentStyleId = input.document_style_id;
            mapToContract.WorkflowId = input.workflow_id;
            mapToContract.DueDate = !string.IsNullOrEmpty(input.due_date) ? (DateTime?)DateTime.Parse(input.due_date, CultureInfo.InvariantCulture) : null;
            mapToContract.InvoiceDate = !string.IsNullOrEmpty(input.invoice_date) ? (DateTime?)DateTime.Parse(input.invoice_date, CultureInfo.InvariantCulture) : null;
            mapToContract.PriceAreIncludedTax = input.prices_are_incl_tax;
            mapToContract.TotalTax = input.total_tax;
            mapToContract.TotalPriceIncludingTax = input.total_price_incl_tax;
            mapToContract.TotalPriceExcludingTax = input.total_price_excl_tax;
            mapToContract.Id = input.id;
            mapToContract.FrequencyType = GetContractFrequency(input.frequency_type);
            mapToContract.Frequency = input.frequency;
            mapToContract.StartDate = input.start_date;
            mapToContract.LastDate = input.last_date;
            return mapToContract;
        }

        public RecurringSalesInvoicePost MapToApi(Contract.Model.RecurringSalesInvoice data, RecurringSalesInvoiceGet current)
        {
            var returnValue = new RecurringSalesInvoicePost();
            if (current != null)
            {
                returnValue.state = current.state;
            }

            SetCustomFields(data, returnValue);
            returnValue.details_attributes = data.Details?.Select(c => _salesInvoiceDetailMapper.MapToApi(c, null)).ToList() ?? new List<SalesInvoiceDetail>();
            returnValue.invoice_id = data.InvoiceId;
            returnValue.id = data.Id;
            returnValue.total_price_excl_tax = data.TotalPriceExcludingTax;
            returnValue.total_price_incl_tax = data.TotalPriceIncludingTax;
            returnValue.total_tax = data.TotalTax;
            returnValue.workflow_id = data.WorkflowId;
            returnValue.contact_id = data.ContactId;
            returnValue.document_style_id = data.DocumentStyleId;
            returnValue.due_date = data.DueDate?.ToString("yyyy-MM-dd");
            returnValue.invoice_date = data.InvoiceDate?.ToString("yyyy-MM-dd");
            returnValue.prices_are_incl_tax = data.PriceAreIncludedTax;
            returnValue.start_date = data.StartDate;
            returnValue.last_date = data.LastDate;
            returnValue.frequency = data.Frequency;
            returnValue.frequency_type = GetApiFrequencyType(data.FrequencyType);
            return returnValue;
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
