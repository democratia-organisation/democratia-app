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

    public static readonly BindableProperty SizeAttributeProperty = BindableProperty.Create(
        nameof(SizeAttribute),
        typeof(Size),
        typeof(LoadingComponent),
        defaultValue: new Size(50,50)
    );

    public Size SizeAttribute
    {
        get => (Size)GetValue(SizeAttributeProperty);
        set => SetValue(SizeAttributeProperty, value);
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
        while (Loading && arcPath != null)
        {
            await arcPath.RelRotateToAsync(180, 1000, Easing.CubicInOut);
            await Task.Delay(10);
        }

    }
}