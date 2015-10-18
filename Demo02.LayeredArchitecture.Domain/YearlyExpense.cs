using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    throw new ArgumentOutOfRangeException("Fiscal Year must be from 1962 and later.");
                }

                this._fiscalYear = value;
            }

        }
    }
}
