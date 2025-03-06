using Application.Contracts.Repositories;
using Data.Context;
using Data.Repositories.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Data.Repositories;
public sealed class DocumentRepository(DatabaseContext dbContext) : BaseRepository<DocumentEntity, int>(dbContext), IDocumentRepository
{
    public async Task<ICollection<DocumentEntity>> QueryEmbeddingsAsync(float[] queryEmbedding)
    {
        var documents = await _dbContext.Documents.ToListAsync();

        var rankedDocuments = documents
            .Select(d => new
            {
                Document = d,
                Similarity = CosineSimilarity(d.Embedding, queryEmbedding)
            })
            .OrderByDescending(d => d.Similarity)
            .Take(5)
            .Select(d => d.Document)
            .ToList();

        return rankedDocuments;
    }

    private static double CosineSimilarity(float[] vectorA, float[] vectorB)
    {
        var dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
        var magnitudeA = Math.Sqrt(vectorA.Sum(a => a * a));
        var magnitudeB = Math.Sqrt(vectorB.Sum(b => b * b));

        return dotProduct / (magnitudeA * magnitudeB);
    }
}
