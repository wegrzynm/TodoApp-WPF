using System.Windows.Controls;
using TodoApp.Controller;
namespace TodoApp.View;

public partial class AddTasksPage : Page
{
    public AddTasksPage(AddTasksController controller)
    {
        InitializeComponent();
        DataContext = controller;
    }
}