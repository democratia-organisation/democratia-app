using com.democratia.Views;
using com.democratia.Views.groupe;
using com.democratia.Views.internaute;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.democratia.view.Views
{
    internal static class BasePageRoute
    {
        public static void RouteBase()
        {
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
