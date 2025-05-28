using com.democratia.Services;
using System;

namespace com.democratia.ViewModels 
{
	public interface INavigeablleViewModel
    {
        public virtual async Task NavigateTapped(string commande) { }
    }
}
