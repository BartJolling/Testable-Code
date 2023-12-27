using System.Windows;

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
