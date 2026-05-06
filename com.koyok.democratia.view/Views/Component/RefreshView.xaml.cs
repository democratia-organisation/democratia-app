using System.Diagnostics;
using System.Windows.Input;

namespace com.koyok.democratia.Views.Component;

public partial class RefreshView : ContentView
{
    public static readonly BindableProperty ChargerElementsCommandProperty =
        BindableProperty.Create(nameof(ChargerElementsCommand), typeof(ICommand), typeof(RefreshView), default(ICommand), BindingMode.OneWay);

    public ICommand ChargerElementsCommand
	{
		get => (ICommand)GetValue(ChargerElementsCommandProperty);
        set => SetValue(ChargerElementsCommandProperty, value);
    }

    public static readonly BindableProperty ElementsProperty =
        BindableProperty.Create(nameof(Elements), typeof(IEnumerable<object>), typeof(RefreshView));
    public IEnumerable<object> Elements
	{
        get => (IEnumerable<object>)GetValue(ElementsProperty);
        set => SetValue(ElementsProperty, value);

    }

    public static readonly BindableProperty IsRefreshingProperty =
        BindableProperty.Create(nameof(IsRefreshing), typeof(bool), typeof(RefreshView), default(bool), BindingMode.TwoWay);
    public bool IsRefreshing
    {
        get => (bool)GetValue(IsRefreshingProperty);
        set => SetValue(IsRefreshingProperty, value);
    }

    public static readonly BindableProperty UpdateElementsCommandProperty = 
        BindableProperty.Create(nameof(UpdateElementsCommand), typeof(ICommand), typeof(RefreshView), default(ICommand), BindingMode.OneWay);

    public ICommand UpdateElementsCommand
    {
        get => (ICommand)GetValue(UpdateElementsCommandProperty);
        set => SetValue(UpdateElementsCommandProperty, value);
    }

    public static readonly BindableProperty ItemTemplateProperty =
    BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RefreshView));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public RefreshView()
	{
		InitializeComponent();
	}
}