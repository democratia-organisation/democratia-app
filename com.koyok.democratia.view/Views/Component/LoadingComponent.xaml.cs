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

    public static readonly BindableProperty SizeProperty = BindableProperty.Create(
        nameof(Size),
        typeof(double),
        typeof(LoadingComponent),
        defaultValue: 50.0
    );

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
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
        while (Loading)
        {
            await arcPath.RotateToAsync(180, 1000, Easing.CubicInOut);
            Thread.Sleep(500);
        }

    }
}