using Microsoft.AspNetCore.Components;

namespace App.Components.Layout;

public partial class Header : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    private bool _isSidebarOpen = false;

    private void ToggleSidebar()
    {
        _isSidebarOpen = !_isSidebarOpen;
    }

    private void NavigateToPage(string url)
    {
        Navigation.NavigateTo(url);
    }

    private string GetActiveClass(string page)
    {
        return Navigation.Uri.Contains(page, StringComparison.CurrentCultureIgnoreCase)
            ? "bg-indigo-800 font-semibold" 
            : "hover:text-gray-200";
    }
}
