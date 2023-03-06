using System.Windows.Controls;
using TodoApp.Controller;

namespace TodoApp.View;

public partial class TasksPage : Page
{
    public TasksPage(TasksController controller)
    {
        InitializeComponent();
        DataContext = controller;
    }
}