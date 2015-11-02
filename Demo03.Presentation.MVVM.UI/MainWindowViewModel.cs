using Demo02.LayeredArchitecture.Business;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Facade;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Demo03.Presentation.MVVM.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ExpenseService _expenseService = null;
        private readonly Dispatcher _currentDispatcher = null;

        private void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private ExpenseService ExpenseService
        {
            get
            {
                return _expenseService;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<YearlyExpense> YearlyExpenses { get; set; }
        public string Filename { get; set; }
        public char? Separator { get; set; }
        public int? FiscalYear { get; set; }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (string.Compare(value, this._errorMessage) != 0)
                {
                    this._errorMessage = value;
                    FirePropertyChanged();
                }
            }
        }
        
        public ICommand PersistFileCommand { get; set; }

        public MainWindowViewModel(ExpenseService expenseService)
        {
            if (expenseService == null)
                throw new ArgumentNullException("ExpenseService");

            this.Filename = @"C:\Users\Bart\Documents\Expenses_2015.txt";
            this.Separator = ',';
            this.FiscalYear = DateTime.Now.Year;
            this.YearlyExpenses = new ObservableCollection<YearlyExpense>();

            this.PersistFileCommand = new PersistFileCommand(this);

            _currentDispatcher = Dispatcher.CurrentDispatcher;

            this._expenseService = expenseService;
            this._expenseService.ExpensePersisted += ExpenseService_ExpensePersisted;

        }
        void ExpenseService_ExpensePersisted(YearlyExpense expense)
        {
            Action dispatchAction = () => YearlyExpenses.Add(expense);
            _currentDispatcher.BeginInvoke(dispatchAction);
        }

        public void PersistFile()
        {
            if (string.IsNullOrWhiteSpace(this.Filename))
            {
                this.ErrorMessage = "Provide a filename";
                return;
            }

            if (this.Separator == 0)
            {
                this.ErrorMessage = "Provide a single separator charactor";
                return;
            }

            if (this.FiscalYear == 0)
            {
                this.ErrorMessage = "Provide a valid fiscal year";
                return;
            }

            try
            {
                this.ErrorMessage = string.Empty;

                Task.Run(() =>
                {
                    string fileContent = string.Empty;

                    using (var reader = new FileInfo(this.Filename).OpenText())
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    this.ExpenseService.PersistFile(fileContent, this.Separator.Value, this.FiscalYear.Value);
                });

                this.ErrorMessage = string.Format("File {0} Submitted", this.Filename);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }
    }
}
