using DemoApp.Helpers;
using SQLite;

namespace DemoApp.Models;

public class DemoModel
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    public Category Category { get; set; }

    public DemoModel()
    {        
    }

    public DemoModel(Category category)
    {
        Category = category;
    }
}