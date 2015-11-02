using Demo02.LayeredArchitecture.Facade;
using System;
using System.Collections.Generic;
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

namespace Demo03.Presentation.MVVM.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindowViewModel ViewModel
        {
            set
            {
                _viewModel = value;
                this.DataContext = _viewModel;
            }
        }

        public MainWindow()
        {
            InitializeComponent();            
        }
    }
}
