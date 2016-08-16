using AutoMoq;
using MoneySharp.Contract.Model;
using MoneySharp.Contract.Settings;
using MoneySharp.Internal.Helper;
using Moq;
using NUnit.Framework;

namespace MoneySharp.Test.Internal.Helper
{
    [TestFixture]
    public class ClientInitializerTest
    {
        private AutoMoqer _mocker;

        private Mock<IAuthenticationSettings> _authenticationSettings;
        private Mock<IUrlSettings> _urlSettings;

        private ClientInitializer _configurator;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMoqer();

            _authenticationSettings = _mocker.GetMock<IAuthenticationSettings>();
            _urlSettings = _mocker.GetMock<IUrlSettings>();

            var settingsProvider = _mocker.GetMock<ISettingsProvider>();
            settingsProvider.Setup(c => c.GetAuthenticationSettings()).Returns(_authenticationSettings.Object);
            settingsProvider.Setup(c => c.GetUrlSettings()).Returns(_urlSettings.Object);

            _configurator = _mocker.Create<ClientInitializer>();
        }

        [Test]
        public void Get_Verify_Url()

        {
            _urlSettings.Setup(c => c.AdministrationId).Returns("AdminstrationId");
            _urlSettings.Setup(c => c.Url).Returns("http://test.nl");
            _urlSettings.Setup(c => c.Version).Returns("v2");
            _authenticationSettings.Setup(c => c.Token).Returns("Test1234");

            var client = _configurator.Get();
            
            Assert.AreEqual(client.BaseUrl, "http://test.nl/api/v2/AdminstrationId");
        }

        [Test]
        public void Get_Verify_DefaultHeaderAuthorization()
        {
            _authenticationSettings.Setup(c => c.Token).Returns("Test1234");
            _urlSettings.Setup(c => c.AdministrationId).Returns("AdminstrationId");
            _urlSettings.Setup(c => c.Url).Returns("http://test.nl");
            _urlSettings.Setup(c => c.Version).Returns("v2");

            _configurator.Get();
            //Can only check this way. There is no way to check the restharp client on default header.
            _authenticationSettings.Verify(c => c.Token, Times.Once);
        }
    }
}
