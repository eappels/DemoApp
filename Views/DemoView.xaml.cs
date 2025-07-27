using DemoApp.ViewModels;

namespace DemoApp.Views;

public partial class DemoView : ContentPage
{
    public DemoView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((DemoViewModel)BindingContext).LoadData();
    }
}