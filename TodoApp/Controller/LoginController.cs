using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TodoApp.Model;
using System.Windows.Input;
using TodoApp.Core;
using TodoApp.View;

namespace TodoApp.Controller;

public class LoginController
{
    private string _username = "";

    public string Username
    {
        //temp
        get => this._username;
        set => this._username = "matzyn.yt@gmail.com";
    }
    
    private string _password = "";

    public string Password
    {
        //temp
        get => this._password;
        set => this._password = "admin123";
    }

    private ICommand _loginCommand;
    
    public ICommand LoginCommand
    {
        get
        {
            if (_loginCommand == null)
            {
                _loginCommand = new RelayCommand(
                    param => this.Login()
                );
            }
            return _loginCommand;
        }
    }
    private async void Login()
    {
        var httpClient = new HttpClient();
        var httpContent = new StringContent($"{{\"username\": \"{Username}\", \"password\": \"{Password}\"}}", Encoding.UTF8, "application/json");
        var httpResponse = await httpClient.PostAsync($"{API.requestURI}login_check", httpContent);

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
             API.token = responseContent.Substring(10, 500);
             NavigateToTODOListsView();
        }
        else
        {
            //MessageBox.Show($"Status code: {httpResponse.StatusCode}, Reason phrase: {httpResponse.ReasonPhrase}", "Error", MessageBoxButton.OK);
            MessageBox.Show($"Invalid username or password!", "Error", MessageBoxButton.OK);
        }
    }
    private void NavigateToTODOListsView()
    {
        //TODO - Find better solution
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.Content = new TODOListsPage(new TODOListsController());
    }
}