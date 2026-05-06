using com.koyok.democratia.Models;

namespace com.koyok.democratia.Views.Component;

public partial class LabelBudgetaire : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(LabelBudgetaire), string.Empty);

    public static readonly BindableProperty TexteProperty = BindableProperty.Create(
        nameof(Texte), typeof(string), typeof(LabelBudgetaire), string.Empty,
        propertyChanged: OnTexteChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Texte
    {
        get => (string)GetValue(TexteProperty);
        set => SetValue(TexteProperty, value);
    }

    public LabelBudgetaire()
    {
        InitializeComponent();
    }

    private static void OnTexteChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LabelBudgetaire)bindable;
        control.UpdateModel((string)newValue);
    }

    private void OnEntryTextChanged(object? sender, TextChangedEventArgs e)
    {
        Texte = e.NewTextValue;
        UpdateModel(e.NewTextValue);
    }

    private void UpdateModel(string value)
    {
        if (BindingContext is Thematique thematique)
            thematique.budget = float.TryParse(value, out float result) ? result : 0;
    }
}