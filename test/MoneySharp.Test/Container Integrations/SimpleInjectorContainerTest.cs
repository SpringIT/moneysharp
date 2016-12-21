using MoneySharp.SimpleInjector;
using NUnit.Framework;
using SimpleInjector;

namespace MoneySharp.Test.Container_Integrations
{
    [TestFixture]
    public class SimpleInjectorContainerTest
    {
        [Test]
        public void VerifyContainer()
        {
            var container = new Container();
            Configurator.With
                .ApplySettings(settings =>
                {
                    settings.AdministrationId = "<ADMINISTRATIONID>";
                    settings.Token = "<TOKEN>";
                    settings.Url = "<URL>";
                    settings.Version = "<VERSION>";
                })
                .UseSimpleInjector(container);

            container.Verify();
        }
    }
}
