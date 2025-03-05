using Application.Models.Files;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.Contracts.UseCases;

public interface IUploadDocumentUseCase
{
    /// <summary>
    /// Asynchronously executes the document upload process and returns the uploaded file details.
    /// </summary>
    /// <param name="file">The file to be uploaded, represented as an <see cref="IBrowserFile"/>.</param>
    /// <returns>A task that represents the asynchronous operation, containing the details of the uploaded file as a <see cref="DocumentModel"/>.</returns>
    Task<DocumentModel> ExecuteAsync(IBrowserFile file);
}