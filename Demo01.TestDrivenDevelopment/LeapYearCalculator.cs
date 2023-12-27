
namespace Demo01.TestDrivenDevelopment
{
    public class LeapYearCalculator
    {
        public bool IsLeapYear(int year)
        {
            return ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0));
        }
    }
}