namespace com.koyok.democratia.UI.Component.groupe;

public partial class ValidationComponent : ContentView
{
	public Label TextLabel { get => textLabel; }
	public CheckBox CheckBox { get => valueCheckBox; }

	public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Text),
		typeof(string),
		typeof(ValidationComponent),
		string.Empty);

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}
    public ValidationComponent()
	{
		InitializeComponent();
	}
}
