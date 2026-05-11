using System.Windows.Input;

namespace com.koyok.democratia.UI.Component.internaute;

public partial class FinGestionCompte : ContentView
{
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText), typeof(string), typeof(FinGestionCompte), default(string));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(
        nameof(ButtonText), typeof(string), typeof(FinGestionCompte), default(string));

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(FinGestionCompte), null);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter), typeof(object), typeof(FinGestionCompte), null);

    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public FinGestionCompte()
    {
        InitializeComponent();
    }
}