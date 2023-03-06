using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TodoApp.Core;
using TodoApp.Model;

namespace TodoApp.Controller;

public class TasksController: INotifyPropertyChanged
{
    private List<Task>? _tasks;

    public List<Task>? Tasks
    {
        get => this._tasks;
        set => this._tasks = value;
    }

    private ICommand _goBackCommand;
    public ICommand GoBackCommand
    {
        get
        {
            if (_goBackCommand == null)
            {
                _goBackCommand = new RelayCommand(
                    param => this.goBack()
                );
            }
            return _goBackCommand;
        }
    }
    
    public TasksController(List<Task> tasks)
    {
        Tasks = tasks;
    }

    private void goBack()
    {
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.GoBack();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}