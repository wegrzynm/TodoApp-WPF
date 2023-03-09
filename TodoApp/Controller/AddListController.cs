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

public class AddListController
{
    public string ListName { get; set; } = "";

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

    private ICommand _createListCommand;
    public ICommand CreateListCommand
    {
        get
        {
            if (_createListCommand == null)
            {
                _createListCommand = new RelayCommand(
                    param => this.CreateList()
                );
            }
            return _createListCommand;
        }
    }
    private void GoBack()
    {
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.Content = new TODOListsPage(new TODOListsController());
    }

    private async void CreateList()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API.token);
        var httpContent = new StringContent($"{{\"listTitle\": \"{ListName}\"}}", Encoding.UTF8, "application/json");
        var httpResponse = await httpClient.PostAsync($"{API.requestURI}todo-lists", httpContent);
        

        if (httpResponse.IsSuccessStatusCode)
        {
            MessageBox.Show("Added List", "Info", MessageBoxButton.OK);
            this.GoBack();
        }
        else
        {
            //MessageBox.Show($"Status code: {httpResponse.StatusCode}, Reason phrase: {httpResponse.ReasonPhrase}", "Error", MessageBoxButton.OK);
            MessageBox.Show($"Couldn't add list. Try again later!", "Error", MessageBoxButton.OK);
        }
    }
}