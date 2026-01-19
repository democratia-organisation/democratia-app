namespace com.democratia.Views.Component;

public partial class EntryComponent : ContentView
{
    private string? _title;

    private bool? _passWord;

    private Entry entryComponent { get; }

    public static readonly BindableProperty ValeurDonneProperty = BindableProperty.Create(
    nameof(ValeurDonne),
    typeof(string),
    typeof(EntryComponent),
    default(string),
    BindingMode.TwoWay
);

    public string ValeurDonne
    {
        get => (string)GetValue(ValeurDonneProperty);
        set => SetValue(ValeurDonneProperty, value);
    }
    // Propriété publique pour passer un paramètre  
    public string? Title
    {
        get => _title;
        set
        {
            _title = value;
            // Met à jour le texte du Label si nécessaire  
            if (Content is VerticalStackLayout layout && layout.Children[0] is Label label)
            {
                label.Text = _title;
            }
        }
    }



    public bool? PassWord
    {
        get => _passWord;
        set
        {
            _passWord = value;
            entryComponent.IsPassword = (bool)_passWord;
        }
    }

    public EntryComponent()
    {
        entryComponent = new Entry()
        {
            Style = (Style?)Application.Current?.Resources["EntryStyle"],
            MaximumWidthRequest = 300,
            AutomationId = "Entry",

        };
        entryComponent.TextChanged += OnTitleChanged;
        Content = new VerticalStackLayout
        {

            Children = {
                new Label {
                  HorizontalOptions = LayoutOptions.Center,
                  Text = _title,
                  Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                  AutomationId = "Label",
                },

                new BoxView {
                  HeightRequest = 30
                },

                new Border {
                    Style = (Style?)Application.Current?.Resources["BorderStyle"],
                    Content = entryComponent,
                },

                new BoxView {
                    HeightRequest = 30
                }
            }
        };
    }

    private void OnTitleChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is Entry)
        {
            ValeurDonne = e.NewTextValue;
        }
    }
}
