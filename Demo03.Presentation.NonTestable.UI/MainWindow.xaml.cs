using Demo02.LayeredArchitecture.Application;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Facade;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shared.TestableCode.Database;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Demo03.Presentation.NonTestable.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ExpenseService _expenseService = null;

    private ObservableCollection<YearlyExpense> _yearlyExpenses = [];

    private ExpenseService ExpenseService
    {
        get
        {
            if (_expenseService == null)
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                var options = new DbContextOptionsBuilder<ExpensesDbContext>()
                    .UseSqlite(connection)
                    .Options;

                this._expenseService = ExpenseServiceFactory.Create(new ExpensesDbContext(options));
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

        if (!int.TryParse(this.FiscalYear.Text, out int fiscalYear))
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

