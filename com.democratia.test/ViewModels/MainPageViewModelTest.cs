using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
{
    
    public class MainPageViewModelTest
    {
        public async Task TestNavigateTapped()
        {
            string command = "Home";
            await MainPageViewModel.NavigateTapped(command);
            
        }
    }
}
