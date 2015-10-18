using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01.TestDrivenDevelopment
{
    public class StringCalculator04
    {
        public int Add(string numbers)
        {
            return this.Add(numbers, ',', 0);
        }

        public int Add(string numbers, char delimiter)
        {
            return this.Add(numbers, delimiter, 0);
        }

        public int Add(string numbers, char delimiter, int startingIndex)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var numbersArray = numbers.Split(delimiter);
            int sum = 0;

            for (int index = startingIndex; index < numbersArray.Length; ++index)
            {
                var numberString = numbersArray[index];
                sum += int.Parse(numberString);
            }

            return sum;
        }

    }
}
