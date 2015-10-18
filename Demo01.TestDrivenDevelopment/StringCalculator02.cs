using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01.TestDrivenDevelopment
{
    public class StringCalculator02
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var numbersArray = numbers.Split(',');
            int sum = 0;

            foreach (var numberString in numbersArray)
            {
                sum += int.Parse(numberString);
            }

            return sum;
        }
    }
}