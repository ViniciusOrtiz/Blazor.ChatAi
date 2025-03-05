using Application.Models.Dtos.Message;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Application.Contracts.Gateways;
using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Domain.Entities;

namespace App.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] private IAiGateway AiGateway { get; set; } = null!;
    [Inject] private IFileService FileService { get; set; } = null!;
    [Inject] private IDocumentRepository DocumentRepository { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private List<MessageInputDto> Messages { get; set; } = new()
    {
        new MessageInputDto
        {
            Text = "Hello, I am the system AI. How can I assist you?",
            IsUser = false
        }
    };

    private string UserInput { get; set; } = string.Empty;
    private bool IsLoading { get; set; } = false;

    /// <summary>
    /// Sends the user input message and generates a mock AI response. Updates the message list and UI state.
    /// </summary>
    private async Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(UserInput) || IsLoading)
            return;

        IsLoading = true;
        StateHasChanged();

        Messages.Add(new MessageInputDto
        {
            Text = UserInput,
            IsUser = true
        });

        try
        {
            var response = await AiGateway.GetAnswerFromAI(UserInput);
            Messages.Add(new MessageInputDto
            {
                Text = response,
                IsUser = false
            });
        }
        catch (Exception ex)
        {
            Messages.Add(new MessageInputDto
            {
                Text = $"Error: {ex.Message}",
                IsUser = false
            });
        }
        finally
        {
            UserInput = string.Empty;
            IsLoading = false;
            StateHasChanged();
            await ScrollToBottom();
        }
    }

    /// <summary>
    /// Handles file upload events.
    /// </summary>
    private async Task OnFileUpload(InputFileChangeEventArgs e)
    {
        if (IsLoading)
            return;

        IsLoading = true;
        StateHasChanged();

        try
        {
            var file = e.File;
            var fileSize = file.Size;

            var text = await FileService.ExtractTextFromFileAsync(file);
            if (string.IsNullOrWhiteSpace(text))
                return;

            var embedding = await AiGateway.GenerateEmbedding(text);
            var entity = new DocumentEntity(text, embedding);

            await DocumentRepository.CreateAsync(entity);

            Messages.Add(new MessageInputDto
            {
                Text = $"Uploaded: {file.Name} ({fileSize / 1024} KB)",
                IsUser = true
            });
        }
        catch (Exception ex)
        {
            Messages.Add(new MessageInputDto
            {
                Text = $"Upload Error: {ex.Message}",
                IsUser = false
            });
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
            await ScrollToBottom();
        }
    }

    /// <summary>
    /// Scrolls to the bottom of the chat after a new message or file upload.
    /// </summary>
    private async Task ScrollToBottom()
    {
        await JsRuntime.InvokeVoidAsync("scrollToBottom", "chat-container");
    }
}
