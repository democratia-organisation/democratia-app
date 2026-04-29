using com.democratia.Models;
using System.Windows.Input;

namespace com.democratia.Views.Component;

public partial class Formulaire : ContentView
{
	public static readonly BindableProperty UserProperty = BindableProperty.Create(
        nameof(User),
        typeof(Internaute),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.TwoWay);
    public Internaute User
    {
        get => (Internaute)GetValue(UserProperty);
        set => SetValue(UserProperty, value);
    }

    public static readonly BindableProperty EmailProperty = BindableProperty.Create(
        nameof(Email),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.TwoWay);
    public string Email
    {
        get => (string)GetValue(EmailProperty);
        set => SetValue(EmailProperty, value);
    }

    public static readonly BindableProperty PasswordProperty = BindableProperty.Create(
        nameof(Password),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.TwoWay);
    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public static readonly BindableProperty RetourMessageProperty = BindableProperty.Create(
        nameof(RetourMessage),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.TwoWay);

    public string RetourMessage
    {
        get => (string)GetValue(RetourMessageProperty);
        set => SetValue(RetourMessageProperty, value);
    }

    public static readonly BindableProperty ActionTappedCommandProperty = BindableProperty.Create(
        nameof(ActionTappedCommand),
        typeof(ICommand),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty TextActionProperty = BindableProperty.Create(
        nameof(TextAction),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.OneWay);

    public string TextAction
    {
        get => (string)GetValue(TextActionProperty);
        set => SetValue(TextActionProperty, value);
    }

    public ICommand ActionTappedCommand
    {
        get => (ICommand)GetValue(ActionTappedCommandProperty);
        set => SetValue(ActionTappedCommandProperty, value);
    }
    public Formulaire()
	{
		InitializeComponent();
	}
}