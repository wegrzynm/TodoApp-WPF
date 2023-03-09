using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TodoApp.Core;
using TodoApp.Model;
using TodoApp.View;

namespace TodoApp.Controller;

public class AddTasksController
{
    public TODOList TodoList;
    public Task Task
    {
        get;
        set;
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
    
    private ICommand _addNewTaskCommand;
    public ICommand AddNewTaskCommand
    {
        get
        {
            if (_addNewTaskCommand == null)
            {
                _addNewTaskCommand = new RelayCommand(
                    param => this.AddTask()
                );
            }
            return _addNewTaskCommand;
        }
    }

    public AddTasksController(TODOList todoList)
    {
        TodoList = todoList;
        Task = new Task();
    }

    private async void AddTask()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API.token);
        var httpContent = new StringContent($"{{\"task\": \"{Task.task}\", \"dueDate\": null }}", Encoding.UTF8, "application/json");
        var httpResponse = await httpClient.PostAsync($"{API.requestURI}todo-lists/{TodoList.id}", httpContent);

        if (httpResponse.IsSuccessStatusCode)
        {
            MessageBox.Show("Added task!", "Info", MessageBoxButton.OK);
            this.GoBack();
        }
        else
        {
            MessageBox.Show($"Status code: {httpResponse.StatusCode}, Reason phrase: {httpResponse.ReasonPhrase}", "Error", MessageBoxButton.OK);
            //MessageBox.Show($"Couldn't add task. Try again later!", "Error", MessageBoxButton.OK);
        }
    }
    private void GoBack()
    {
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.Content = new TODOListsPage(new TODOListsController());
    }
}