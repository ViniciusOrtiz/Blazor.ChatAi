using Application.Contracts.Gateways;
using Application.Contracts.Tools;
using OpenAI;
using OpenAI.Chat;

namespace Infrastructure.Gateways;

public sealed class OpenAiGateway : IAiGateway
{
    private readonly OpenAIClient _openAiClient;
    private readonly IDocumentsTool _documentsTool;

    public OpenAiGateway(
        OpenAIClient openAiClient,
        IDocumentsTool documentsTool)
    {
        _openAiClient = openAiClient;
        _documentsTool = documentsTool;
    }

    public async Task<string> GetAnswerFromAI(string question)
    {
        var queryDatabaseTool = ChatTool.CreateFunctionTool(
            functionName: "queryDatabase",
            functionDescription: "Searches the database for a specific question."
        );

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(@"""
                You can access the database to get information about the documents from a specific question from the user.
            """),
            new UserChatMessage($"User question: {question}")
        };

        ChatCompletionOptions options = new()
        {
            Tools = { queryDatabaseTool }
        };

        bool requiresAction;
        do
        {
            requiresAction = false;
            ChatCompletion completion = await _openAiClient.GetChatClient("gpt-4o").CompleteChatAsync(messages, options);

            switch (completion.FinishReason)
            {
                case ChatFinishReason.Stop:
                    messages.Add(new AssistantChatMessage(completion));
                    return completion.Content[0].Text;

                case ChatFinishReason.ToolCalls:
                    messages.Add(new AssistantChatMessage(completion));

                    foreach (var toolCall in completion.ToolCalls)
                    {
                        switch (toolCall.FunctionName)
                        {
                            case "queryDatabase":
                                {
                                    var questionEmbedding = await GenerateEmbedding(question);
                                    var filteredDbResults = await _documentsTool.QueryDatabase(questionEmbedding);

                                    var filteredDbResponse = filteredDbResults.Count != 0
                                        ? string.Join("\n", filteredDbResults.Select(d => d.Content))
                                        : "Data not found.";

                                    messages.Add(new ToolChatMessage(toolCall.Id, filteredDbResponse));
                                    requiresAction = true;
                                    break;
                                }

                            default:
                                throw new NotImplementedException();
                        }
                    }
                    break;

                case ChatFinishReason.Length:
                case ChatFinishReason.ContentFilter:
                case ChatFinishReason.FunctionCall:
                default:
                    throw new NotImplementedException(completion.FinishReason.ToString());
            }
        } while (requiresAction);

        return "The information could not be retrieved.";
    }

    public async Task<float[]> GenerateEmbedding(string text)
    {
        var client = _openAiClient.GetEmbeddingClient("text-embedding-3-small");

        var embeddingResponse = await client.GenerateEmbeddingAsync(text);

        return embeddingResponse.Value.ToFloats().ToArray();
    }
}