﻿using System;
using System.Windows.Input;

namespace ClinicWpf.ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action methodToExecute;
        private readonly Func<bool>? canExecuteEvaluator;

        public RelayCommand(Action methodToExecute, Func<bool>? canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object? parameter)
        {
            if (canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                return canExecuteEvaluator.Invoke();
            }
        }

        public void Execute(object? parameter)
        {
            methodToExecute.Invoke();
        }
    }
}
