using System;
using System.Windows.Controls;
using TodoApp.Controller;

namespace TodoApp.View;

public partial class TODOListsPage : Page
{
    public TODOListsPage(TODOListsController controller)
    {
        InitializeComponent();
        DataContext = controller;
    }
}