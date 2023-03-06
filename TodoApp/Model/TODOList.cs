using System;
using System.Collections.Generic;
using System.Windows;

namespace TodoApp.Model;

public class TODOList
{
    public int id { get; set; }
    public string listTitle { get; set; }
    public List<Task>? tasks { get; set; }
    public string createdAt { get; set; }
}