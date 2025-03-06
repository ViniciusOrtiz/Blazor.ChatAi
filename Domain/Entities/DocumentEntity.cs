namespace Domain.Entities
{
    public class DocumentEntity : BaseEntity<int>
    {
        public string Name { get; private set; }
        public string Content { get; private set; }
        public float[] Embedding { get; private set; }
        public long FileSizeInBytes { get; private set; }
        public byte[] FileContent { get; private set; }

        public DocumentEntity(string name, string content, float[] embedding, long fileSizeInBytes, byte[] fileContent)
        {
            Validate(name, content, embedding, fileSizeInBytes, fileContent);
            Name = name;
            Content = content;
            Embedding = embedding;
            FileSizeInBytes = fileSizeInBytes;
            FileContent = fileContent;
        }

        public void UpdateContent(string name, string newContent, float[] newEmbedding, long newFileSizeInBytes, byte[] newFileContent)
        {
            Validate(name, newContent, newEmbedding, newFileSizeInBytes, newFileContent);
            Name = name;
            Content = newContent;
            Embedding = newEmbedding;
            FileSizeInBytes = newFileSizeInBytes;
            FileContent = newFileContent;
        }

        private static void Validate(string name, string content, float[] embedding, long fileSizeInBytes, byte[] fileContent)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The file name must not be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("The content must contain valid values.", nameof(content));

            if (embedding == null || embedding.Length == 0)
                throw new ArgumentException("The embedding must contain valid values.", nameof(embedding));

            if (embedding.Any(e => float.IsNaN(e) || float.IsInfinity(e)))
                throw new ArgumentException("The embedding must contain valid values.", nameof(embedding));

            if (fileSizeInBytes <= 0)
                throw new ArgumentException("The file size must be greater than zero.", nameof(fileSizeInBytes));

            if (fileContent == null || fileContent.Length == 0)
                throw new ArgumentException("The file content cannot be empty.", nameof(fileContent));
        }
    }
}
