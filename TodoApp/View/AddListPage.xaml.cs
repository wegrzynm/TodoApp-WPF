using System.Windows.Controls;
using TodoApp.Controller;

namespace TodoApp.View;

public partial class AddList : Page
{
    public AddList(AddListController controller)
    {
        InitializeComponent();
        DataContext = controller;
    }
}