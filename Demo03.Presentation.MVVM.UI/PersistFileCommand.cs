using System;
using System.Windows.Input;

namespace Demo03.Presentation.MVVM.UI
{
    class PersistFileCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;
        public PersistFileCommand(MainWindowViewModel vm)
        {
            _viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return (
                !string.IsNullOrWhiteSpace(_viewModel.Filename) &&
                _viewModel.Separator.HasValue &&
                _viewModel.FiscalYear.HasValue &&
                _viewModel.FiscalYear.Value > 0
                );
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            _viewModel.PersistFile();
        }
    }
}
