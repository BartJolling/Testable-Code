using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo01.TestDrivenDevelopment.Test
{
    [TestClass]
    public class StringCalculator01Test
    {
        #region Requirement 1: The Add method accepts a comma-delimeted string of integers

        [TestMethod]
        public void SC01WhenIntegersArePassedThenNoExceptionIsThrown()
        {
            //Arrange
            string input = "7,9,10,15";
            var calculator = new StringCalculator01();

            //Act
            calculator.Add(input);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void SC01WhenNonNumbersArePassedThenExceptionIsThrown()
        {
            //Arrange
            string input = "7,9,F,15";
            var calculator = new StringCalculator01();

            //Act
            calculator.Add(input);
        }

        #endregion
    }
}
