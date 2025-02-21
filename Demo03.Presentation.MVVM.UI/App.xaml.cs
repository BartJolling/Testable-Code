using Demo02.LayeredArchitecture.Facade;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shared.TestableCode.Database;
using System.Windows;

namespace Demo03.Presentation.MVVM.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ExpensesDbContext>()
            .UseSqlite(connection)
            .Options;

        var viewModel = new MainWindowViewModel(ExpenseServiceFactory.Create(new ExpensesDbContext(options)));
        var window = new MainWindow
        {
            ViewModel = viewModel
        };
        window.Show();
    }
}
