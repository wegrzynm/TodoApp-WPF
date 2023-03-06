using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

    public TODOListsController()
    {
        loadTODOLists();
    }

    private async void loadTODOLists()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API.token);
        var httpResponse = await httpClient.GetAsync($"{API.requestURI}todo-lists/12");
        if (httpResponse.IsSuccessStatusCode)
        {
            try
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<TODOList>));
                using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseContent)))
                {
                    this.TodoLists = (List<TODOList>)serializer.ReadObject(ms);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK);
                this.TodoLists = new List<TODOList>();
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
        if (obj is List<Task>)
        {
            List<Task> tasks = (List<Task>)obj;
            Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
            mainFrame.Content = new TasksPage(new TasksController(tasks));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}