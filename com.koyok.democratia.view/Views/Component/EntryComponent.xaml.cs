namespace com.koyok.democratia.Views.Component;

public partial class EntryComponent : ContentView
{
    public static readonly BindableProperty ValeurDonneProperty = BindableProperty.Create(
        nameof(ValeurDonne),
        typeof(string),
        typeof(EntryComponent),
        default(string),
        BindingMode.TwoWay);

    public string ValeurDonne
    {
        get => (string)GetValue(ValeurDonneProperty);
        set => SetValue(ValeurDonneProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(EntryComponent),
        default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty PassWordProperty = BindableProperty.Create(
        nameof(PassWord),
        typeof(bool),
        typeof(EntryComponent),
        false);

    public bool PassWord
    {
        get => (bool)GetValue(PassWordProperty);
        set => SetValue(PassWordProperty, value);
    }
    public EntryComponent()
	{
		InitializeComponent();
	}
}