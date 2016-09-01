using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using MoneySharp.Contract.Exceptions;
using MoneySharp.Internal.Helper;
using Moq.AutoMock;
using NUnit.Framework;
using RestSharp;

namespace MoneySharp.Test.Internal.Helper
{
    public class RequestHelperTest
    {
        private AutoMocker _mocker;

        private RequestHelper _requestHelper;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();

            _requestHelper = _mocker.CreateInstance<RequestHelper>();
        }

        [TestCase(Method.GET, "test.json")]
        [TestCase(Method.DELETE, "test.json")]
        [TestCase(Method.POST, "test.json")]
        [TestCase(Method.PATCH, "test.xml")]
        [TestCase(Method.PUT, "test.xml")]
        [TestCase(Method.HEAD, "test.json")]
        public void BuildRequest_Uri_Correctly(Method method, string expectedResult)
        {
            var result = _requestHelper.BuildRequest("test", method);
            result.Resource.ShouldBeEquivalentTo(expectedResult);
        }

        [TestCase(Method.GET)]
        [TestCase(Method.DELETE)]
        [TestCase(Method.POST)]
        [TestCase(Method.PUT)]
        [TestCase(Method.PATCH)]
        public void BuildRequest_Method_Correctly(Method method)
        {
            var result = _requestHelper.BuildRequest("test", method);
            result.Method.ShouldBeEquivalentTo(method);
        }

        [TestCase(Method.POST, "postdata")]
        [TestCase(Method.PATCH, "patchdata")]
        [TestCase(Method.PUT, "putdata")]
        public void BuildRequest_MethodWithData_SetJsonData(Method method, string data)
        {
            var result = _requestHelper.BuildRequest("test", method, data);
            result.Parameters.First().Value.ShouldBeEquivalentTo("\"" + data + "\"");
        }

        [Test]
        public void CheckResult_Forbidden_Throws_RateLimitExceededException()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.Forbidden };

            Action action = () => _requestHelper.CheckResult(response);
            action.ShouldThrow<RateLimitExceededException>();
        }

        [Test]
        public void CheckResult_Unauthorized_Throws_UnauthorizedException()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.Unauthorized };

            Action action = () => _requestHelper.CheckResult(response);
            action.ShouldThrow<UnauthorizedMoneybirdException>();
        }

        [Test]
        public void CheckResult_NotFound_Throws_KeyNotFoundException()
        {
            var response = new RestResponse { StatusCode = HttpStatusCode.NotFound };

            Action action = () => _requestHelper.CheckResult(response);
            action.ShouldThrow<KeyNotFoundException>();
        }
    }
}
