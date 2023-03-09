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
using TodoApp.Core;
using TodoApp.Model;
using TodoApp.View;

namespace TodoApp.Controller;

public class TODOListsController : INotifyPropertyChanged
{
    private List<TODOList> _todoLists;

    public List<TODOList> TodoLists
    {
        get => _todoLists;
        set
        {
            if (_todoLists != value)
            {
                _todoLists = value;
                OnPropertyChanged(nameof(TodoLists));
            }
        }
    }

    private ICommand _showTasksCommand;
    public ICommand ShowTasksCommand
    {
        get
        {
            if (_showTasksCommand == null)
            {
                _showTasksCommand = new RelayCommand(this.ShowTasks);
            }
            return _showTasksCommand;
        }
    }
    
    private ICommand _addListCommand;
    
    public ICommand AddListCommand
    {
        get
        {
            if (_addListCommand == null)
            {
                _addListCommand = new RelayCommand(
                    param => this.AddList()
                );
            }
            return _addListCommand;
        }
    }
    
    private ICommand _deleteTaskCommand;
    
    public ICommand DeleteTaskCommand
    {
        get
        {
            if (_deleteTaskCommand == null)
            {
                _deleteTaskCommand = new RelayCommand(this.DeleteList);
            }
            return _deleteTaskCommand;
        }
    }

    public TODOListsController()
    {
        loadTODOLists();
    }

    private async void loadTODOLists()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API.token);
        var httpResponse = await httpClient.GetAsync($"{API.requestURI}todo-lists");
        if (httpResponse.IsSuccessStatusCode)
        {
            try
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<TODOList>));
                using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseContent)))
                {
                    TodoLists = (List<TODOList>)serializer.ReadObject(ms);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK);
                TodoLists = new List<TODOList>();
            }
        }
        else
        {
            MessageBox.Show($"Coudn't fetch items. Try again later", "Error", MessageBoxButton.OK);
        }
    }

    private void ShowTasks(object obj)
    {
        //TODO - Find better solution
        if (obj is TODOList)
        {
            TODOList todoList = (TODOList)obj;
            Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
            mainFrame.Content = new TasksPage(new TasksController(todoList));
        }
    }
    
    private async void DeleteList(object obj)
    {
        if (obj is TODOList)
        {
            TODOList todoList = (TODOList)obj;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API.token);
            var httpResponse = await httpClient.DeleteAsync($"{API.requestURI}todo-lists/{todoList.id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Deleted list!", "Info", MessageBoxButton.OK);
                loadTODOLists();
            }
            else
            {
                MessageBox.Show($"Coudn't delete this list. Try again later", "Error", MessageBoxButton.OK);
            }
        }
        else
        {
            MessageBox.Show("Coudn't find this list", "Error", MessageBoxButton.OK);
        }
    }

    private void AddList()
    {
        //TODO - Find better solution
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.Content = new AddList(new AddListController());
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}