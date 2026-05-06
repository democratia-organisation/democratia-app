namespace com.koyok.democratia.Views.Component;

public partial class DecideurIndicator : ContentView
{
    public static readonly BindableProperty CouleurCercleProperty = BindableProperty.Create(
        nameof(CouleurCercle),
        typeof(Color),
        typeof(DecideurIndicator),
        default(Color),
        BindingMode.TwoWay);

    public static readonly BindableProperty TexteProperty = BindableProperty.Create(
        nameof(Texte),
        typeof(string),
        typeof(DecideurIndicator),
        default(string),
        BindingMode.TwoWay);

    public string? Texte
    {
        get => (string)GetValue(TexteProperty);
        set => SetValue(TexteProperty, value);
    }

    public Color? CouleurCercle
    {
        get => (Color)GetValue(CouleurCercleProperty);
        set => SetValue(CouleurCercleProperty, value);
    }

    public DecideurIndicator()
    {
        InitializeComponent();
    }
}