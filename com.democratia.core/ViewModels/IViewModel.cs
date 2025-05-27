using com.democratia.Services;
using System;

namespace com.democratia.ViewModels 
{
	public interface IViewModel 
	{
		protected Client? client { get; }
    }
}
