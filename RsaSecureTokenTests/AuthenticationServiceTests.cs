using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsaSecureToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaSecureToken.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            var fakeProfileDao = new FakeProfileDao();
            var fakeRsaTokenDao = new FakeRsaTokenDao();
            var sut = new AuthenticationService(fakeProfileDao, fakeRsaTokenDao);

            // implement your own act
            var actual = sut.IsValid("April", "330959");

            Assert.IsTrue(actual);
        }
    }

    public class FakeProfileDao : IProfileDao
    {
        public int GetRegisterTimeInMinutes(string account)
        {
            return 1008;
        }
    }

    public class FakeRsaTokenDao : IRsaTokenDao
    {
        public Random GetRandom(int minutes)
        {
            return new Random(1008);
        }
    }
}