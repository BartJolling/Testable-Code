using Demo02.LayeredArchitecture.Application;
using Demo02.LayeredArchitecture.Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Demo03.Presentation.MVVM.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ExpenseService _expenseService = null;
        private readonly Dispatcher _currentDispatcher = null;

        private void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (string.Compare(value, this._statusMessage) != 0)
                {
                    this._statusMessage = value;
                    FirePropertyChanged();
                }
            }
        }

        public ICommand PersistFileCommand { get; set; }

        public MainWindowViewModel(ExpenseService expenseService)
        {
            ArgumentNullException.ThrowIfNull(expenseService);

            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

            this.Filename =  Path.Combine(assemblyFolder, "Expenses_2015.txt");
            this.Separator = ',';
            this.FiscalYear = DateTime.Now.Year;
            this.YearlyExpenses = [];

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
                this.StatusMessage = "Provide a filename";
                return;
            }

            if (this.Separator == 0)
            {
                this.StatusMessage = "Provide a single separator character";
                return;
            }

            if (this.FiscalYear == 0)
            {
                this.StatusMessage = "Provide a valid fiscal year";
                return;
            }

            this.StatusMessage = $"File {this.Filename} Submitted";

            Task.Run(() =>
            {
                try
                {
                    using var reader = new FileInfo(this.Filename).OpenText();
                    string fileContent = reader.ReadToEnd();
                    
                    this.ExpenseService.PersistFile(fileContent, this.Separator.Value, this.FiscalYear.Value);

                    this.StatusMessage = $"File {this.Filename} Uploaded";
                }
                catch (Exception ex)
                {
                    this.StatusMessage = ex.Message;
                }
            });
        }
    }
}
