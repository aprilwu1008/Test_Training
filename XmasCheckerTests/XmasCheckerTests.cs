using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmasChecker.Tests
{
    public class FakeXmasChecker : XmasChecker
    {
        internal DateTime _today;

        public override bool IsTodayXmas()
        {
            if (_today.Month == 12 && _today.Day == 25)
            {
                return true;
            }

            return false;
        }

        public void SetToday(DateTime today)
        {
            _today = today;
        }
    }

    [TestClass()]
    public class XmasCheckerTests
    {
        [TestMethod()]
        public void Today_is_not_xmas()
        {
            var xmasChecker = new FakeXmasChecker();
            DateTime today = new DateTime(2019, 12, 24);
            xmasChecker.SetToday(today);
            var actual = xmasChecker.IsTodayXmas();

            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void Today_is_xmas()
        {
            var xmasChecker = new FakeXmasChecker();
            DateTime today = new DateTime(2019, 12, 25);
            xmasChecker.SetToday(today);
            var actual = xmasChecker.IsTodayXmas();

            Assert.AreEqual(true, actual);
        }
    }
}