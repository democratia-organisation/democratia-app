namespace com.koyok.democratia.UI.Component;

public partial class LoadingComponent : ContentView
{
    public static readonly BindableProperty LoadingProperty = BindableProperty.Create(
        nameof(Loading),
        typeof(bool),
        typeof(LoadingComponent),
        defaultValue: false
    );

    public bool Loading
    {
        get => (bool)GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    public LoadingComponent()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        _ = AnimationChargement();
    }



    private async Task AnimationChargement()
    {
        while (true)
        {
            uint vitesse = 5;
            for (double i = 0; i < 2 * Math.PI; i += ((2 * Math.PI) / 80))
            {
                await arcPath.TranslateToAsync(Math.Cos(i)*5, Math.Sin(i)*5, vitesse, Easing.CubicIn);
            }
            await Task.Delay(5);
        }
    }
}