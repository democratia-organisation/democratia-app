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
            uint vitesse = 2000;
            Task animation1 = arcPath.RelRotateToAsync(360, vitesse, Easing.CubicIn);
            Task animation2 = arcBisPath.RelRotateToAsync(-360, vitesse / 2, Easing.CubicOut);
            await Task.WhenAll(animation1, animation2);
            await Task.Delay(5);
        }
    }
}