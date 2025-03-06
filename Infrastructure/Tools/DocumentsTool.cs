using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Contracts.Tools;
using Application.Models.Dtos.Tools;
using Domain.Entities;

namespace Infrastructure.Tools
{
    public sealed class DocumentsTool : IDocumentsTool
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ISecurityService _securityService;

        public DocumentsTool(
            IDocumentRepository documentRepository,
            ISecurityService securityService)
        {
            _documentRepository = documentRepository;
            _securityService = securityService;
        }

        public async Task<ICollection<DocumentToolOutputDto>> QueryDatabase(float[] questionEmbedding)
        {
            var response = await _documentRepository.QueryEmbeddingsAsync(questionEmbedding);
            return response.Select(p => new DocumentToolOutputDto
            {
                Content = _securityService.DecryptText(p.Content)
            }).ToList();
        }

    }
}