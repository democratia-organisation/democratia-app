using System.Globalization;
using System.Windows.Input;

namespace com.koyok.democratia.UI.Component.groupe;

public partial class ButtonGroupe : ContentView
{
    public static readonly BindableProperty OpenGroupeCommandProperty = BindableProperty.Create(
        nameof(OpenGroupeCommand), typeof(ICommand), typeof(ButtonGroupe));

    public ICommand OpenGroupeCommand
    {
        get => (ICommand)GetValue(OpenGroupeCommandProperty);
        set => SetValue(OpenGroupeCommandProperty, value);
    }

    public ButtonGroupe()
    {
        InitializeComponent();
    }

    protected async override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
    }
}