using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo01.TestDrivenDevelopment.Test
{
    [TestClass]
    public class Is_A_Leap_Year
    {
        [TestMethod]
        public void LY01_Divisible_By_4_But_Not_Divisible_By_100()
        {
            var calculator = new LeapYearCalculator();

            Assert.IsTrue(calculator.isLeapYear(1976));
            Assert.IsTrue(calculator.isLeapYear(2012));
        }

        [TestMethod]
        public void LY01_Divisible_By_400()
        {
            var calculator = new LeapYearCalculator();

            Assert.IsTrue(calculator.isLeapYear(1600));
            Assert.IsTrue(calculator.isLeapYear(2000));
        }
    }
}
