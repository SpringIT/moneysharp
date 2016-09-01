using MoneySharp.Contract;
using MoneySharp.Contract.Settings;
using SimpleInjector;

namespace MoneySharp
{
    public static class SimpleInjectorExtension
    {
        public static Container UseSimpleInjector(this Configurator configurator,Container container)
        {
            container.Register<ISettings>(() => configurator.Settings);

            container.Register<IMoneyBirdClient, MoneyBirdClient>();
            container.Register<IContactService, ContactService>();
            container.Register<ISalesInvoiceService, SalesInvoiceService>();

            return container;
        }
    }
}
