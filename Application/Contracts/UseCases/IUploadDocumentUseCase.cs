using Microsoft.AspNetCore.Components.Forms;

namespace Application.Contracts.UseCases;

public interface IUploadDocumentUseCase
{
    /// <summary>
    /// Executes the document upload process, extracting text from the provided file,
    /// generating embeddings, and saving the resulting data into the document repository.
    /// </summary>
    /// <param name="file">The file to be processed for text extraction and embedding generation.</param>
    /// <returns>A task that represents the asynchronous execution of the upload process.</returns>
    Task ExecuteAsync(IBrowserFile file);
}