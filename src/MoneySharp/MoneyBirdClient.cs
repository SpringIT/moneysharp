using MoneySharp.Contract;

namespace MoneySharp
{
    public class MoneyBirdClient : IMoneyBirdClient
    {
        private readonly IContactService _contactService;
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly IRecurringSalesInvoiceService _recurringSalesInvoiceService;

        public MoneyBirdClient(IContactService contactService, ISalesInvoiceService salesInvoiceService, IRecurringSalesInvoiceService recurringSalesInvoiceService)
        {
            _contactService = contactService;
            _salesInvoiceService = salesInvoiceService;
            _recurringSalesInvoiceService = recurringSalesInvoiceService;
        }

        public IContactService Contacts => _contactService;
        public ISalesInvoiceService SalesInvoices => _salesInvoiceService;
        public IRecurringSalesInvoiceService RecurringSalesInvoices => _recurringSalesInvoiceService;
    }
}
