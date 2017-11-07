using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MoneySharp.Internal.Model;
using SimpleJson;

namespace MoneySharp.Internal.Mapping
{
    public class SalesInvoiceMapper : IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost>
    {
        private readonly IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>
            _salesInvoiceDetailMapper;

        private readonly IMapper<Contract.Model.CustomField, CustomField, CustomField> _customFieldMapper;

        public SalesInvoiceMapper(IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail> salesInvoiceDetailMapper, IMapper<Contract.Model.CustomField, CustomField, CustomField> customFieldMapper)
        {
            _salesInvoiceDetailMapper = salesInvoiceDetailMapper;
            _customFieldMapper = customFieldMapper;
        }

        public Contract.Model.SalesInvoice MapToContract(SalesInvoiceGet input)
        {
            var mapToContract = new Contract.Model.SalesInvoice
            {
                Url = input.url,
                ContactId = input.contact_id,
                CustomFields =
                    input.custom_fields != null
                        ? input.custom_fields.Select(_customFieldMapper.MapToContract).ToList()
                        : new List<Contract.Model.CustomField>(),
                Details =
                    input.details != null
                        ? input.details.Select(_salesInvoiceDetailMapper.MapToContract).ToList()
                        : new List<Contract.Model.SalesInvoiceDetail>(),
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
            mapToContract.Url = input.url;
            mapToContract.State = GetContractState(input.state);

            return mapToContract;
        }

        public SalesInvoicePost MapToApi(Contract.Model.SalesInvoice data, SalesInvoiceGet current)
        {
            var returnValue = new SalesInvoicePost();

            SetCustomFields(data, returnValue);
            returnValue.details_attributes = data.Details?.Select(c => _salesInvoiceDetailMapper.MapToApi(c, null)).ToList() ?? new List<SalesInvoiceDetail>();
            returnValue.invoice_id = data.InvoiceId;
            returnValue.id = data.Id;
            returnValue.state = GetApiState(data.State);
            returnValue.total_price_excl_tax = data.TotalPriceExcludingTax;
            returnValue.total_price_incl_tax = data.TotalPriceIncludingTax;
            returnValue.total_tax = data.TotalTax;
            returnValue.workflow_id = data.WorkflowId;
            returnValue.contact_id = data.ContactId;
            returnValue.document_style_id = data.DocumentStyleId;
            returnValue.due_date = data.DueDate?.ToString("yyyy-MM-dd");
            returnValue.invoice_date = data.InvoiceDate?.ToString("yyyy-MM-dd");
            returnValue.prices_are_incl_tax = data.PriceAreIncludedTax;
            return returnValue;
        }

        private void SetCustomFields(SalesInvoiceGet current, SalesInvoicePost returnValue)
        {
            var jobject = new JsonObject();
            foreach (var customField in current.custom_fields)
            {
                jobject[current.custom_fields.IndexOf(customField).ToString()] = customField;
            }

            returnValue.custom_fields_attributes = jobject;
        }

        private void SetCustomFields(Contract.Model.SalesInvoice current, SalesInvoicePost returnValue)
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

    }


}