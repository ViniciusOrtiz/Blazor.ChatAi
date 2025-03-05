using Application.Contracts.UseCases;
using Application.Models.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace App.Components.Pages;

public partial class Files : ComponentBase
{
    private readonly IGetAllFilesUseCase _getAllFilesUseCase;
    private readonly IFileGetContentUseCase _fileGetContentUseCase;
    private IEnumerable<DocumentModel> _filesList = [];

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    public Files(
        IGetAllFilesUseCase getAllFilesUseCase,
        IFileGetContentUseCase fileGetContentUseCase)
    {
        _getAllFilesUseCase = getAllFilesUseCase;
        _fileGetContentUseCase = fileGetContentUseCase;
    }
    
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
        _filesList = await _getAllFilesUseCase.ExecuteAsync();
    }
    
    private async Task DownloadFile(int fileId)
    {
        var file = await _fileGetContentUseCase.ExecuteAsync(fileId);
        
        await JsRuntime.InvokeVoidAsync("downloadFile", file.Name, file.Content);
    }

    private void DeleteFile(int fileId)
    {
        _filesList = _filesList.Where(x => x.Id != fileId);
        StateHasChanged();
    }
}