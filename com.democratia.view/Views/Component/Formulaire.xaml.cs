using com.democratia.Models;
using System.Windows.Input;

namespace com.democratia.Views.Component;

public partial class Formulaire : ContentView
{
	public static readonly BindableProperty InteranuteProperty = BindableProperty.Create(
        nameof(internaute),
        typeof(Internaute),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.OneWayToSource);
    public Internaute internaute
    {
        get => (Internaute)GetValue(InteranuteProperty);
        set => SetValue(InteranuteProperty, value);
    }

    public static readonly BindableProperty EmailProperty = BindableProperty.Create(
        nameof(email),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.OneWayToSource);
    public string email
    {
        get => (string)GetValue(EmailProperty);
        set => SetValue(EmailProperty, value);
    }

    public static readonly BindableProperty PasswordProperty = BindableProperty.Create(
        nameof(password),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.OneWayToSource);
    public string password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public static readonly BindableProperty RetourMessageProperty = BindableProperty.Create(
        nameof(retourMessage),
        typeof(string),
        typeof(Formulaire),
        defaultBindingMode: BindingMode.OneWayToSource);

    public string retourMessage
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