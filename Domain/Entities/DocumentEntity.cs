namespace Domain.Entities
{
    public class DocumentEntity : BaseEntity<int>
    {
        public string Content { get; private set; }

        public float[] Embedding { get; private set; }

        public DocumentEntity(string content, float[] embedding)
        {
            Validate(content, embedding);
            Content = content;
            Embedding = embedding;
        }

        public void UpdateContent(string newContent, float[] newEmbedding)
        {
            Validate(newContent, newEmbedding);
            Content = newContent;
            Embedding = newEmbedding;
        }

        private static void Validate(string content, float[] embedding)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("The content must contain valid values.", nameof(content));

            if (embedding == null || embedding.Length == 0)
                throw new ArgumentException("The embedding must contain valid values.", nameof(embedding));

            if (embedding.Any(e => float.IsNaN(e) || float.IsInfinity(e)))
                throw new ArgumentException("The embedding must contain valid values.", nameof(embedding));
        }
    }
}