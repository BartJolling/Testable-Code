using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01.TestDrivenDevelopment
{
    public class StringCalculator01
    {
        public void Add(string numbers)
        {
            var nummbersArray = numbers.Split(',');

            foreach(var numberString in nummbersArray)
            {
                int number = int.Parse(numberString);
            }
        }
    }
}
