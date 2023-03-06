using System.Collections.Generic;

namespace TodoApp.Model;

public class User
{
    private string _username;

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

    private List<TODOList>? _todoLists;

    public List<TODOList>? TodoLists
    {
        get => this._todoLists;
        set => this._todoLists = value;
    }

    public User(string username, string password)
    {
        this._username = username;
        this._password = password;
    }
}