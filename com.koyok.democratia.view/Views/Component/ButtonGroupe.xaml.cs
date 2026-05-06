using System.Windows.Input;

namespace com.koyok.democratia.Views.Component;

public partial class ButtonGroupe : ContentView
{
    public static readonly BindableProperty ImageProperty = BindableProperty.Create(
        nameof(Image), typeof(ImageSource), typeof(ButtonGroupe));

    public ImageSource Image
    {
        get => (ImageSource)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly BindableProperty OpenGroupeCommandProperty = BindableProperty.Create(
        nameof(OpenGroupeCommand), typeof(ICommand), typeof(ButtonGroupe));

    public ICommand OpenGroupeCommand
    {
        get => (ICommand)GetValue(OpenGroupeCommandProperty);
        set => SetValue(OpenGroupeCommandProperty, value);
    }

    public static readonly BindableProperty GroupeProperty =
        BindableProperty.Create(
            nameof(Groupe), typeof(Models.Groupe), typeof(ButtonGroupe));

    public Models.Groupe Groupe
    {
        get => (Models.Groupe)GetValue(GroupeProperty);
        set => SetValue(GroupeProperty, value);
    }

    public ButtonGroupe()
    {
        InitializeComponent();
    }

    protected async override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is Tuple<Models.Groupe,ImageSource,ICommand> tuple)
        {
            Image = tuple.Item2;
            OpenGroupeCommand = tuple.Item3;
            Groupe = tuple.Item1;

        }
    }
}