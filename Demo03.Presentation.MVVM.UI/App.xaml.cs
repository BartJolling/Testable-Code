using Demo02.LayeredArchitecture.Facade;
using System.Windows;

namespace Demo03.Presentation.MVVM.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainWindowViewModel(ExpenseServiceFactory.Create());
            var window = new MainWindow
            {
                ViewModel = viewModel
            };
            window.Show();
        }
    }
}
