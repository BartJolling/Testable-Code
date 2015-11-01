using Demo02.LayeredArchitecture.Business;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Facade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo03.Presentation.NonTestable.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExpenseService _expenseService = null;

        private ObservableCollection<YearlyExpense> _yearlyExpenses = new ObservableCollection<YearlyExpense>();

        private ExpenseService ExpenseService
        {
            get
            {
                if (_expenseService == null)
                {
                    this._expenseService = ExpenseServiceFactory.Create();
                }
                return _expenseService;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _yearlyExpenses;
            this.ExpenseService.ExpensePersisted += ExpenseService_ExpensePersisted;
        }

        void ExpenseService_ExpensePersisted(Demo02.LayeredArchitecture.Domain.YearlyExpense expense)
        {
            Action dispatchAction = () => _yearlyExpenses.Add(expense);
            this.Dispatcher.BeginInvoke(dispatchAction);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filename = this.Filename.Text;
            var separators = this.Separator.Text.ToCharArray();
            int fiscalYear = 0;

            if (string.IsNullOrWhiteSpace(filename))
            {
                MessageBox.Show("Provide a filename", "Error", MessageBoxButton.OK);
                return;
            }

            if (separators == null || separators.Count() != 1)
            {
                MessageBox.Show("Provide a single separator charactor", "Error", MessageBoxButton.OK);
                return;
            }

            if (!int.TryParse(this.FiscalYear.Text, out fiscalYear))
            {
                MessageBox.Show("Provide a valid fiscal year", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                Task.Run(() =>
                {
                    string fileContent = string.Empty;

                    using (var reader = new FileInfo(filename).OpenText())
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    this.ExpenseService.PersistFile(fileContent, separators.First(), fiscalYear);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
    }
}
