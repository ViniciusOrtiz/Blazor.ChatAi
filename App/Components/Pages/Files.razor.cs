using Application.Models.Files;
using Microsoft.AspNetCore.Components;

namespace App.Components.Pages;

public partial class Files : ComponentBase
{
    private List<DocumentModel> _filesList = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) // Runs only after the first render
        {
            await GetFiles();
            StateHasChanged(); // Triggers a re-render with new data
        }
    }

    private async Task GetFiles()
    {
        await Task.Delay(2000); // Simulate API delay
        _filesList =
        [
            new DocumentModel { Id = 1, Name = "Report.pdf", Size = 1234.56, CreatedAt = DateTime.Now.AddDays(-2) },
            new DocumentModel { Id = 2, Name = "Presentation.pptx", Size = 789.12, CreatedAt = DateTime.Now.AddDays(-1) },
            new DocumentModel { Id = 3, Name = "Invoice.xlsx", Size = 456.78, CreatedAt = DateTime.Now }
        ];
    }
    
    private void DownloadFile(int fileId)
    {
        // Logic for downloading the file (Implementation depends on API or backend storage)
        Console.WriteLine($"Downloading file with ID: {fileId}");
    }

    private void DeleteFile(int fileId)
    {
        _filesList.RemoveAll(f => f.Id == fileId);
    }
}