using Demo02.LayeredArchitecture.Business;
using Demo02.LayeredArchitecture.Facade;
using Demo02.LayeredArchitecture.Infrastructure;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

            MainWindowViewModel viewModel = new MainWindowViewModel(ExpenseServiceFactory.Create());

            MainWindow window = new MainWindow();
            window.ViewModel = viewModel;
            window.Show();
        }
    }
}
