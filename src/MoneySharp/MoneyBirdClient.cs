using MoneySharp.Contract;
using MoneySharp.Contract.Settings;
using MoneySharp.Internal;
using MoneySharp.Internal.Helper;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;

namespace MoneySharp
{
    public class MoneyBirdClient : IMoneyBirdClient
    {
        private readonly IContactService _contactService;
        private readonly ISalesInvoiceService _salesInvoiceService;

        public MoneyBirdClient(ISettingsProvider settingsProvider)
        {
            var requestHelper = new RequestHelper();
            var clientInitializer = new ClientInitializer(settingsProvider);

            _contactService =
                new ContactService(
                    new DefaultConnector<Contact, ContactWrapper>("contacts", clientInitializer, requestHelper),
                    new ContactMapper());
            _salesInvoiceService =
                new SalesInvoiceService(
                    new DefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper>("sales_invoices", clientInitializer,
                        requestHelper), new SalesInvoiceMapper());
        }

        public IContactService Contacts => _contactService;
        public ISalesInvoiceService SalesInvoices => _salesInvoiceService;
    }
}
