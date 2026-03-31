using com.democratia.Models;
using Microsoft.Maui.Controls.Shapes;

namespace com.democratia.Views.Component
{

    public partial class LabelBudgetaire : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(LabelBudgetaire),
            defaultValue: string.Empty,
            propertyChanged: OnTitleChanged);

        public static readonly BindableProperty TexteProperty = BindableProperty.Create(
            propertyName: nameof(Texte),
            returnType: typeof(string),
            declaringType: typeof(LabelBudgetaire),
            defaultValue: string.Empty,
            propertyChanged: OnTexteChanged);
        private HorizontalStackLayout? _layout;

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

        private readonly Label _label;
        private readonly Entry _entry;
        public LabelBudgetaire()
        {
            _label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Style = (Style?)Application.Current?.Resources["LabelStyle"],

            };
            _entry = new Entry
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Style = (Style?)Application.Current?.Resources["EntryStyle"],
                MaximumWidthRequest = 100,
                Keyboard = Keyboard.Numeric,
            };
            _entry.TextChanged += _entry_TextChanged;
            _layout = new HorizontalStackLayout
            {
                Children = {
                _label,
                new BoxView { WidthRequest = 20 },
                _entry,
                new BoxView { WidthRequest = 15 },

            },
            };
            _layout.AutomationId = "LayoutLabelBudgetaire";
            AutomationProperties.SetName(_layout, "LayoutLabelBudgetaire");
            var border = new Border
            {
                Content = _layout,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 50
                },
            };
            if (Application.Current!.Resources.TryGetValue("Light-surfaceVariant", out var lightP) && Application.Current!.Resources.TryGetValue("Dark-surfaceVariant", out var darkP))
            {
                Color lightCouleur = (Color)lightP, darkCouleur = (Color)darkP;
                _layout.SetAppThemeColor(HorizontalStackLayout.BackgroundColorProperty, lightCouleur, darkCouleur);
            }

            if (Application.Current!.Resources.TryGetValue("Light-primaryContainer", out var light) && Application.Current!.Resources.TryGetValue("Dark-primaryContainer", out var dark))
            {
                Color lightCouleur = (Color)light, darkCouleur = (Color)dark;
                border.SetAppThemeColor(Border.StrokeProperty, lightCouleur, darkCouleur);
            }
            Content = border;
        }

        private void _entry_TextChanged(object? sender, TextChangedEventArgs e) =>
            Changement(this, e.OldTextValue, e.NewTextValue);

        private static void OnTexteChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LabelBudgetaire control)
                Changement(control, oldValue, newValue);
        }

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LabelBudgetaire control)
                control._label.Text = (string)newValue;
        }

        private static void Changement(LabelBudgetaire control, object oldValue, object newValue)
        {
            control._entry.Text = (string)newValue;
            ((Thematique)control.BindingContext).budget = float.TryParse(control._entry.Text, out float result) ? result : 0;
        }
    }
}