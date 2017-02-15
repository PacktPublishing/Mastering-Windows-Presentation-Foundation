using System;
using System.Windows.Input;

namespace CompanyName.ApplicationName.ViewModels.Commands
{
    public class ActionCommand : ICommand
    {
        readonly Action<object> action;
        readonly Predicate<object> canExecute;
        private EventHandler eventHandler;

        public ActionCommand(Action<object> action) : this(action, null) { }

        public ActionCommand(Action<object> action, Predicate<object> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                eventHandler += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                eventHandler -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            eventHandler?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}