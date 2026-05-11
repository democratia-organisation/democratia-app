namespace com.koyok.democratia.UI.Component
{
    public partial class LoadingComponent : ContentView
    {
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
                uint vitesse = 1500;
                await Task.WhenAll(
                    arcPath.RelRotateToAsync(360, vitesse, Easing.CubicIn),
                    arcBisPath.RelRotateToAsync(360, vitesse/2, Easing.CubicInOut)
                );
            }
        }


    }
}