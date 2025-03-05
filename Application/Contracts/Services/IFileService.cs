using Microsoft.AspNetCore.Components.Forms;

namespace Application.Contracts.Services
{
    public interface IFileService
    {
        Task<string?> ExtractTextFromFileAsync(IBrowserFile file);
    }
}