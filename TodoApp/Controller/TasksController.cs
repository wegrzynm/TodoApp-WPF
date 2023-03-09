using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TodoApp.Core;
using TodoApp.Model;
using TodoApp.View;

namespace TodoApp.Controller;

public class TasksController: INotifyPropertyChanged
{
    public TODOList TodoList
    {
        get;
    }
    
    private List<Task>? _tasks;
    public List<Task>? Tasks
    {
        get => this._tasks;
        set
        {
            if (_tasks != value)
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
    }

    private ICommand _goBackCommand;
    public ICommand GoBackCommand
    {
        get
        {
            if (_goBackCommand == null)
            {
                _goBackCommand = new RelayCommand(
                    param => this.GoBack()
                );
            }
            return _goBackCommand;
        }
    }
    private ICommand _addNewTask;
    public ICommand AddNewTask
    {
        get
        {
            if (_addNewTask == null)
            {
                _addNewTask = new RelayCommand(
                    param => this.AddTask()
                );
            }
            return _addNewTask;
        }
    }
    
    private void AddTask()
    {
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.Content = new AddTasksPage(new AddTasksController(TodoList));
    }
    
    private ICommand _deleteTaskCommand;
    public ICommand DeleteTaskCommand
    {
        get
        {
            if (_deleteTaskCommand == null)
            {
                _deleteTaskCommand = new RelayCommand(this.DeleteTask);
            }
            return _deleteTaskCommand;
        }
    }

    private async void DeleteTask(object obj)
    {
        if (obj is Task)
        {
            Task task = (Task)obj;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API.token);
            var httpResponse = await httpClient.DeleteAsync($"{API.requestURI}todo-lists/{TodoList.id}/task/{task.id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                Tasks?.Remove(task);
                MessageBox.Show("Deleted task!", "Info", MessageBoxButton.OK);
                this.GoBack();
            }
            else
            {
                MessageBox.Show($"Coudn't delete this item. Try again later", "Error", MessageBoxButton.OK);
            }
        }
        else
        {
            MessageBox.Show("Coudn't find this task", "Error", MessageBoxButton.OK);
        }
    }

    public TasksController(TODOList todoList)
    {
        TodoList = todoList;
        Tasks = TodoList.tasks;
    }

    private void GoBack()
    {
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.GoBack();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}