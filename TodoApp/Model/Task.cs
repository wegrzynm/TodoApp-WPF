using System;
using System.Windows;

namespace TodoApp.Model;

public class Task
{
    public int id { get; set; }
    public string task { get; set; }
    public object dueDate { get; set; }
    public bool isActive { get; set; }
    public string createdAt { get; set; }
}