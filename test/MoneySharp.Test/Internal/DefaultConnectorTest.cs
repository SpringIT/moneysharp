using System.Collections.Generic;
using FluentAssertions;
using MoneySharp.Internal;
using MoneySharp.Internal.Helper;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using RestSharp;

namespace MoneySharp.Test.Internal
{
    [TestFixture]
    public class DefaultConnectorTest
    {
        private AutoMocker _mocker;

        private Mock<IClientInitializer> _initializer;
        private Mock<IRequestHelper> _requestHelper;
        private string _urlAppend;

        //RestSharp mocks
        private Mock<IRestClient> _restClient;
        private Mock<IRestRequest> _restRequest;

        private DefaultConnector<GetObject, PostObject> _connector;

        [SetUp]
        public void Setup()
        {
            _urlAppend = "Test";
            _mocker = new AutoMocker();

            _restRequest = _mocker.GetMock<IRestRequest>();
            _restClient = _mocker.GetMock<IRestClient>();
            _initializer = _mocker.GetMock<IClientInitializer>();
            _requestHelper = _mocker.GetMock<IRequestHelper>();

            _connector = new DefaultConnector<GetObject, PostObject>(_urlAppend, _initializer.Object,_requestHelper.Object);
        }

        [Test]
        public void GetList_Calls_RequestHelperAndClient_Returns_ExpectedResult()
        {
            var expectedResult = new List<GetObject>() {new GetObject()};

            var response = new RestResponse<List<GetObject>>()
            {
                Data = expectedResult
            };

            _requestHelper.Setup(c => c.BuildRequest(_urlAppend, Method.GET, null)).Returns(_restRequest.Object);
            _restClient.Setup(c => c.Execute<List<GetObject>>(_restRequest.Object)).Returns(response);
            _initializer.Setup(c => c.Get()).Returns(_restClient.Object);

            var result = _connector.GetList();

            _requestHelper.Verify(c => c.CheckResult(response));
            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetById_Calls_RequestHelperAndClient_Returns_ExpectedResult()
        {
            var expectedResult = new GetObject();
            long inputId = 1234;

            var response = new RestResponse<GetObject>
            {
                Data = expectedResult
            };

            _requestHelper.Setup(c => c.BuildRequest($"{_urlAppend}/{inputId}", Method.GET, null)).Returns(_restRequest.Object);
            _restClient.Setup(c => c.ExecuteAsGet<GetObject>(_restRequest.Object, "GET")).Returns(response);
            _initializer.Setup(c => c.Get()).Returns(_restClient.Object);

            var result = _connector.GetById(inputId);

            _requestHelper.Verify(c => c.CheckResult(response));
            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void Create_Calls_RequestHelperAndClient_Returns_ResultFromResponse()
        {
            var postObject = new PostObject();
            var expectedResult = new GetObject();
            var response = new RestResponse<GetObject>
            {
                Data = expectedResult
            };

            _requestHelper.Setup(c => c.BuildRequest(_urlAppend, Method.POST, postObject)).Returns(_restRequest.Object);
            _restClient.Setup(c => c.ExecuteAsPost<GetObject>(_restRequest.Object, "POST")).Returns(response);
            _initializer.Setup(c => c.Get()).Returns(_restClient.Object);

            var result = _connector.Create(postObject);

            _requestHelper.Verify(c => c.CheckResult(response));
            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void Update_Calls_RequestHelperAndClient_Returns_ResultFromResponse()
        {
            long inputId = 1234;
            var postObject = new PostObject();
            var expectedResult = new GetObject();
            var response = new RestResponse<GetObject>
            {
                Data = expectedResult
            };

            _requestHelper.Setup(c => c.BuildRequest($"{ _urlAppend}/{inputId}", Method.PATCH, postObject)).Returns(_restRequest.Object);
            _restClient.Setup(c => c.ExecuteAsPost<GetObject>(_restRequest.Object, "PATCH")).Returns(response);
            _initializer.Setup(c => c.Get()).Returns(_restClient.Object);

            var result = _connector.Update(inputId, postObject);

            _requestHelper.Verify(c => c.CheckResult(response));
            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void Delete_Calls_RequestHelpderAndClient()
        {
            long inputId = 1234;
            var response = new RestResponse();

            _requestHelper.Setup(c => c.BuildRequest($"{_urlAppend}/{inputId}", Method.DELETE, null))
                .Returns(_restRequest.Object);
            _restClient.Setup(c => c.Execute(_restRequest.Object)).Returns(response);
            _initializer.Setup(c => c.Get()).Returns(_restClient.Object);

            _connector.Delete(inputId);

            _requestHelper.Verify(c => c.CheckResult(response));
        }

        private class GetObject
        {
            public string Test { get; set; }
        }

        private class PostObject
        {
            public GetObject Object { get; set; }
        }
    }
}
