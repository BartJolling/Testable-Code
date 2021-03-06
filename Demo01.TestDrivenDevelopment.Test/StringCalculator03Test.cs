﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo01.TestDrivenDevelopment.Test
{
    [TestClass]
    public class StringCalculator03Test
    {
        #region Requirement 1: The Add method accepts a comma-delimeted string of integers

        [TestMethod]
        public void SC03_When_Integers_Are_Passed_Then_No_Exception_Is_Thrown()
        {
            //Arrange
            string input = "7,9,10,15";
            var calculator = new StringCalculator03();

            //Act
            calculator.Add(input);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void SC03_When_Non_Numbers_Are_Passed_Then_Exception_Is_Thrown()
        {
            //Arrange
            string input = "7,9,F,15";
            var calculator = new StringCalculator03();

            //Act
            calculator.Add(input);
        }

        #endregion


        #region Requirement 2: For an empty string the method will return 0

        [TestMethod]
        public void SC03_When_Empty_String_Is_Passed_Then_Return_0()
        {
            //Arrange
            string input = string.Empty;
            var calculator = new StringCalculator03();

            //Act
            int sum = calculator.Add(input);

            //Assert
            Assert.AreEqual(0, sum);
        }

        #endregion


        #region Requirement 3: Method will return their sum of numbers

        [TestMethod]
        public void SC03_When_Integers_Are_Passed_Then_Return_Their_Sum()
        {
            //Arrange
            string input = "7,9,10,15,-1";
            var calculator = new StringCalculator03();

            //Act
            int sum = calculator.Add(input);

            //Assert
            Assert.AreEqual(40, sum);
        }

        #endregion


        #region Requirement 4: Support user specified delimiters

        [TestMethod]
        public void SC03_When_Integers_Are_Passed_With_Custom_Delimiter_Then_Return_Their_Sum()
        {
            //Arrange
            string input = "7;9;10;15;-1";
            var calculator = new StringCalculator03();

            //Act
            int sum = calculator.Add(input, ';');

            //Assert
            Assert.AreEqual(40, sum);
        }

        #endregion
    }
}
