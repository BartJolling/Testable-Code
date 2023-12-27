namespace Demo01.TestDrivenDevelopment
{
    public class StringCalculator03
    {
        public int Add(string numbers)
        {
            return Add(numbers, ',');
        }

        public int Add(string numbers, char delimiter)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var numbersArray = numbers.Split(delimiter);
            int sum = 0;

            foreach (var numberString in numbersArray)
            {
                sum += int.Parse(numberString);
            }

            return sum;
        }
    }
}