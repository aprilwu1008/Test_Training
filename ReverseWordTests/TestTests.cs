using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReverseWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseWord.Tests
{
    [TestClass()]
    public class TestTests
    {
        [TestMethod()]
        public void input_empty_should_return_zeroOne()
        {
            var test = new Test();//arange
            var actual = test.AppendZeroOne("");//action
            Assert.AreEqual("01",actual);//assert
        }
        public void input_empty_should_return_oneZeroOne()
        {
            var test = new Test();//arange
            var actual = test.AppendZeroOne("");//action
            Assert.AreEqual("101", actual);//assert
        }
    }
}