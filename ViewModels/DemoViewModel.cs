using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Helpers;
using DemoApp.Models;
using DemoApp.Services.Interfaces;
using System.Collections.ObjectModel;

namespace DemoApp.ViewModels;

public partial class DemoViewModel : ObservableObject
{

    private readonly IDBService dbService;
    public ObservableCollection<DemoModel> DemoModels { get; }
    private int limit = 10, offset = 0;

    private static readonly Random random = new();

    public DemoViewModel(IDBService dbService)
    {
        this.dbService = dbService;
        DemoModels = new ObservableCollection<DemoModel>();
    }

    public async void LoadData()
    {
        var data = await dbService.Read(limit, offset);
        if (data != null && data.Count > 0)
        {
            DemoModels.Clear();
            foreach (var item in data)
            {
                DemoModels.Add(item);
            }
        }
#if DEBUG
        else
        {
            for (int i = 0; i < 222; i++)
            {
                var model = new DemoModel(GetRandomCategory());
                await dbService.Create(model);
                DemoModels.Add(model);
            }
        }
#endif
    }

    [RelayCommand]
    private async Task Next()
    {
        offset += limit;
        var data = await dbService.Read(limit, offset);
        if (data != null && data.Count > 0)
        {
            DemoModels.Clear();
            foreach (var item in data)
            {
                DemoModels.Add(item);
            }
        }
        else
        {
            offset -= limit;
        }
    }

    [RelayCommand]
    private async Task Previous()
    {
        offset -= limit;
        if (offset < 0)
        {
            offset = 0;
        }
        var data = await dbService.Read(limit, offset);
        if (data != null && data.Count > 0)
        {
            DemoModels.Clear();
            foreach (var item in data)
            {
                DemoModels.Add(item);
            }
        }
        else
        {
            offset += limit;
        }
    }

    [RelayCommand]
    private void DeleteItem(DemoModel model)
    {
        if (DemoModels.Contains(model))
        {
            DemoModels.Remove(model);
            dbService.Delete(model);
        }
    }

    [RelayCommand]
    private async Task ResetDB()
    {
        DemoModels.Clear();
        offset = 0;
        await dbService.ResetDB();
        LoadData();
    }

    private static Category GetRandomCategory()
    {
        var values = Enum.GetValues(typeof(Category));
        return (Category)values.GetValue(random.Next(values.Length));
    }
}