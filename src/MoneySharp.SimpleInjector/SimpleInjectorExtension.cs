using MoneySharp.Contract;
using MoneySharp.Contract.Settings;
using MoneySharp.Internal;
using MoneySharp.Internal.Helper;
using MoneySharp.Internal.Mapping;
using MoneySharp.Internal.Model;
using MoneySharp.Internal.Model.Wrapper;
using SimpleInjector;

namespace MoneySharp.SimpleInjector
{
    public static class SimpleInjectorExtension
    {
        public static Container UseSimpleInjector(this Configurator configurator, Container container)
        {
            container.Register<ISettings>(() => configurator.Settings);

            container.Register<IDefaultConnector<Contact, ContactWrapper>>(
                () => new DefaultConnector<Contact, ContactWrapper>("contacts", container.GetInstance<IClientInitializer>(), container.GetInstance<IRequestHelper>()));
            container.Register<IDefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper>>(
              () => new DefaultConnector<SalesInvoiceGet, SalesInvoiceWrapper>("sales_invoices", container.GetInstance<IClientInitializer>(), container.GetInstance<IRequestHelper>()));
            container.Register<IDefaultConnector<RecurringSalesInvoiceGet, RecurringSalesInvoiceWrapper>>(
              () => new DefaultConnector<RecurringSalesInvoiceGet, RecurringSalesInvoiceWrapper>("recurring_sales_invoices", container.GetInstance<IClientInitializer>(), container.GetInstance<IRequestHelper>()));

            container.Register<IMapper<Contract.Model.Contact, Contact, Contact>, ContactMapper>();
            container.Register<IMapper<Contract.Model.SalesInvoice, SalesInvoiceGet, SalesInvoicePost>, SalesInvoiceMapper>();
            container.Register<IMapper<Contract.Model.SalesInvoiceDetail, SalesInvoiceDetail, SalesInvoiceDetail>, SalesInvoiceDetailMapper>();
            container.Register<IMapper<Contract.Model.CustomField, CustomField, CustomField>, CustomFieldMapper>();
            container.Register<IMapper<Contract.Model.RecurringSalesInvoice, RecurringSalesInvoiceGet, RecurringSalesInvoicePost>, RecurringSalesInvoiceMapper>();

            container.Register<IClientInitializer, ClientInitializer>();
            container.Register<IRequestHelper, RequestHelper>();
            container.Register<IMoneyBirdClient, MoneyBirdClient>();
            container.Register<IContactService, ContactService>();
            container.Register<ISalesInvoiceService, SalesInvoiceService>();
            container.Register<IRecurringSalesInvoiceService, RecurringSalesInvoiceService>();

            return container;
        }
    }
}
