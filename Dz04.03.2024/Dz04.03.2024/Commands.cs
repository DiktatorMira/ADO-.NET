using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dz04._03._2024 {
    public class Command : ICommand {
        Action<object> execute;
        Predicate<object> canExecute;
        public Command(Action<object> execute, Predicate<object> can) {
            if (execute == null) throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecute = can;
        }
        public event EventHandler? CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter) {
            if (canExecute != null) return canExecute(parameter);
            return true;
        }
        public void Execute(object parameter) => execute(parameter);
    }
}
