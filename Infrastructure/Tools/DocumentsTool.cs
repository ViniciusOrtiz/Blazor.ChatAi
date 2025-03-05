using Application.Contracts.Repositories;
using Application.Contracts.Tools;
using Domain.Entities;

namespace Infrastructure.Tools
{
    public class DocumentsTool(
        IDocumentRepository documentRepository) : IDocumentsTool
    {
        private readonly IDocumentRepository _documentRepository = documentRepository;

        public async Task<ICollection<DocumentEntity>> QueryDatabase(float[] questionEmbedding)
        {
            return await _documentRepository.QueryEmbeddingsAsync(questionEmbedding);
        }

    }
}