using com.democratia.ViewModels.groupe;

namespace com.democratia.Views.Component;

public partial class ButtonGroupe : ContentView
{
    public static readonly BindableProperty GroupeProperty = BindableProperty.Create(
        nameof(Groupe), typeof(Models.Groupe), typeof(ButtonGroupe), null,
        propertyChanged: OnGroupeChanged);

    public Models.Groupe Groupe
    {
        get => (Models.Groupe)GetValue(GroupeProperty);
        set => SetValue(GroupeProperty, value);
    }

    public ButtonGroupe()
    {
        InitializeComponent();
    }

    private static async void OnGroupeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ButtonGroupe)bindable;
        if (newValue is Models.Groupe groupe)
        {
            var vm = ServiceHelper.GetService<GroupeViewModel>();
            if (vm != null)
            {
                vm.Groupe = groupe;
                control.BindingContext = vm;
                vm.GetImageAsync(groupe.Image!);
            }
        }
    }
}