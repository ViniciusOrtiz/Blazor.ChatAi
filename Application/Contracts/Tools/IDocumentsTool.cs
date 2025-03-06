using Application.Models.Dtos.Tools;
using Domain.Entities;

namespace Application.Contracts.Tools;

public interface IDocumentsTool
{
    Task<ICollection<DocumentToolOutputDto>> QueryDatabase(float[] questionEmbedding);
}