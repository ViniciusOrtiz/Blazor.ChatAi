using Microsoft.AspNetCore.Components.Forms;

namespace Application.Contracts.UseCases;

public interface IUploadDocumentUseCase
{
    Task ExecuteAsync(IBrowserFile file);
}