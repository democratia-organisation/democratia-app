namespace com.koyok.democratia.UI.Component
{
    public partial class LoadingArc : ContentView, IDrawable
    {
        public LoadingArc()
        {
            InitializeComponent();
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Couleur;
            canvas.StrokeSize = 10;
            canvas.StrokeLineCap = LineCap.Round;

            float x = 50;
            float y = 50;
            float width = 200;
            float height = 200;

            canvas.DrawArc(x, y, width, height, 0, 180, true, false);
        }

        public static readonly BindableProperty CouleurProperty = BindableProperty.Create(
            nameof(Couleur),
            typeof(Color),
            typeof(LoadingArc),
            defaultValue: default(Color),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var control = (LoadingArc)bindable;
                control.canvasView.Invalidate();
            }
        );

        public Color Couleur
        {
            get => (Color)GetValue(CouleurProperty);
            set => SetValue(CouleurProperty, value);
        }


    }
}
