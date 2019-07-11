using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ServerApiDependency.Enum;
using ServerApiDependency.Enums;
using ServerApiDependency.Utility.CustomException;

namespace ServerApiDependency.Tests
{
    [TestClass()]
    public class ServerApiTests
    {
        /// <summary>
        /// LV 3, verify specific method be called
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_exception_test()
        {
            var logger = Substitute.For<ILogger>();
            var serverApi = Substitute.For<ServerApi>(logger);

            serverApi.PostToThirdParty(ApiType.CancelGame, Arg.Any<string>()).ThrowsForAnyArgs<WebException>();
            serverApi.CancelGame();

            // Assert SaveFailRequestToDb() be called once
            serverApi.Received(1).SaveFailRequestToDb(ApiType.CancelGame, Arg.Any<string>());
        }

        /// <summary>
        /// LV 2, verify specific exception be thrown
        /// </summary>
        [ExpectedException(typeof(AuthFailException))]
        [TestMethod()]
        public void post_cancelGame_third_party_fail_test()
        {
            var logger = Substitute.For<ILogger>();
            var serverApi = Substitute.For<ServerApi>(logger);

            serverApi.PostToThirdParty(ApiType.CancelGame, Arg.Any<string>()).ReturnsForAnyArgs(99);

            serverApi.CancelGame();

            // Assert PostToThirdParty() return not correct should throw AuthFailException
        }

        /// <summary>
        /// LV 1, verify api correct response
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_success_test()
        {
            var logger = Substitute.For<ILogger>();
            var serverApi = Substitute.For<ServerApi>(logger);

            serverApi.PostToThirdParty(ApiType.CancelGame, Arg.Any<string>()).ReturnsForAnyArgs(0);

            Assert.AreEqual((ServerResponse)0, serverApi.CancelGame());
            // Assert success
        }
    }
}