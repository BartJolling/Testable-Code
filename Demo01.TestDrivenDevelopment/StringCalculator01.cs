﻿namespace Demo01.TestDrivenDevelopment
{
    public class StringCalculator01
    {
        public void Add(string numbers)
        {
            var numbersArray = numbers.Split(',');

            foreach(var numberString in numbersArray)
            {
                int number = int.Parse(numberString);
            }
        }
    }
}
