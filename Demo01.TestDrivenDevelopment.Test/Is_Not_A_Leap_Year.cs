using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo01.TestDrivenDevelopment.Test
{
    [TestClass]
    public class Is_Not_A_Leap_Year
    {
        [TestMethod]
        public void LY01_Not_Divisible_By_4()
        {
            var calculator = new LeapYearCalculator();

            Assert.IsFalse(calculator.IsLeapYear(1977));
            Assert.IsFalse(calculator.IsLeapYear(2015));
        }

        [TestMethod]
        public void LY01_Divisible_By_100_But_Not_Divisible_By_400()
        {
            var calculator = new LeapYearCalculator();

            Assert.IsFalse(calculator.IsLeapYear(1900));
            Assert.IsFalse(calculator.IsLeapYear(2100));
        }
    }
}
