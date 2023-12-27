using System;

namespace Demo02.LayeredArchitecture.Domain
{
    public class YearlyExpense
    {
        private int _fiscalYear;

        public int EmployeeId { get; set; }
        
        public string Category { get; set; }

        public decimal Amount { get; set; }

        public int FiscalYear 
        { 
            get
            {
                return this._fiscalYear;
            }
            set
            {
                if( value < 1962 )
                {
                    throw new ArgumentOutOfRangeException(nameof(FiscalYear), "Fiscal Year must be from 1962 and later.");
                }

                this._fiscalYear = value;
            }

        }
    }
}
