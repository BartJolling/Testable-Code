using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01.TestDrivenDevelopment
{
    public class StringCalculator03
    {
        public int Add(string numbers)
        {
            return this.Add(numbers, ',');
        }

        public int Add(string numbers, char delimiter)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var nummbersArray = numbers.Split(delimiter);
            int sum = 0;

            foreach (var numberString in nummbersArray)
            {
                sum += int.Parse(numberString);
            }

            return sum;
        }
    }
}