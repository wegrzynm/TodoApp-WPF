using System.Net.Http;
using System.Security;
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
        get => this._username;
        set => this._username = value;
    }
    
    private string _password;

    public string Password
    {
        get => this._password;
        set => this._password = value;
    }

    private ICommand _loginCommand;
    
    public ICommand LoginCommand
    {
        get
        {
            if (_loginCommand == null)
            {
                _loginCommand = new RelayCommand(this.Login);
            }
            return _loginCommand;
        }
    }
    private async void Login(object obj)
    {
        if (obj is PasswordBox)
        {
            PasswordBox password = (PasswordBox)obj;
            Password = password.Password;

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
        else
        {
            MessageBox.Show("Try again later!", "Error", MessageBoxButton.OK);
        }
       
    }
    private void NavigateToTODOListsView()
    {
        //TODO - Find better solution
        Frame mainFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");
        mainFrame.Content = new TODOListsPage(new TODOListsController());
    }
}