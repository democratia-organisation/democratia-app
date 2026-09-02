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
    public ICommand OpenParameter 
    { 
       get => (ICommand)GetValue(OpenParameterProperty); 
       set => SetValue(OpenParameterProperty, value); 
    }

    public static readonly BindableProperty OpenParameterProperty = BindableProperty.Create(
        nameof(OpenParameter), typeof(ICommand), typeof(ButtonGroupe));

    public ButtonGroupe()
    {
        InitializeComponent();
    }

    protected async override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
    }
}