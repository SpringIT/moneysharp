using FluentAssertions;
using MoneySharp.Contract.Model;
using MoneySharp.Contract.Settings;
using MoneySharp.Internal.Helper;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace MoneySharp.Test.Internal.Helper
{
    [TestFixture]
    public class ClientInitializerTest
    {
        private AutoMocker _mocker;

        private Mock<ISettings> _settings;

        private ClientInitializer _configurator;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _settings = _mocker.GetMock<ISettings>();

            var settingsProvider = _mocker.GetMock<ISettingsProvider>();
            settingsProvider.Setup(c => c.GetSettings()).Returns(_settings.Object);

            _configurator = _mocker.CreateInstance<ClientInitializer>();
        }

        [Test]
        public void Get_Verify_Url()

        {
            _settings.Setup(c => c.AdministrationId).Returns("AdminstrationId");
            _settings.Setup(c => c.Url).Returns("http://test.nl");
            _settings.Setup(c => c.Version).Returns("v2");
            _settings.Setup(c => c.Token).Returns("Test1234");

            var client = _configurator.Get();
            
            client.BaseUrl.ShouldBeEquivalentTo("http://test.nl/api/v2/AdminstrationId/");
        }

        [Test]
        public void Get_Verify_DefaultHeaderAuthorization()
        {
            _settings.Setup(c => c.Token).Returns("Test1234");
            _settings.Setup(c => c.AdministrationId).Returns("AdminstrationId");
            _settings.Setup(c => c.Url).Returns("http://test.nl");
            _settings.Setup(c => c.Version).Returns("v2");

            _configurator.Get();
            //Can only check this way. There is no way to check the restharp client on default header.
            _settings.Verify(c => c.Token, Times.Once);
        }
    }
}
