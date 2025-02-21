namespace Shared.TestableCode.Database;

    public class Expense
    {
        public int EmployeeId { get; set; }
        public int FiscalYear { get; set; }
        public required string Category { get; set; }
        public decimal Amount { get; set; }
    }