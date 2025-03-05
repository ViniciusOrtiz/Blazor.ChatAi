using Microsoft.AspNetCore.Components;

namespace App.Components.Layout;

public partial class Header : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    private bool IsSidebarOpen = false;

    private void ToggleSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }

    private void NavigateToPage(string url)
    {
        Navigation.NavigateTo(url);
    }

    private string GetActiveClass(string page)
    {
        return Navigation.Uri.ToLower().Contains(page) 
            ? "bg-indigo-800 font-semibold" 
            : "hover:text-gray-200";
    }
}
