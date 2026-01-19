namespace com.democratia.view.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            var list = new List<ContentView>();
            for (int i = 0; i < 10; i++)
            {

            }
            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Children =
                    {

                    }
                }
            };

        }
    }
}
