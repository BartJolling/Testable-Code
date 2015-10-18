
namespace Demo02.LayeredArchitecture.Facade
{
    public class Expenses
    {
        public void PersistFile(System.IO.FileInfo file, int fiscalYear)
        {
            var service = ExpenseServiceFactory.Create();
            var stringContent = string.Empty;

            using( var reader = file.OpenText())
            {
                stringContent = reader.ReadToEnd();
            }

            service.PersistFile(string.Empty, ',', fiscalYear);
        }
    }
}
