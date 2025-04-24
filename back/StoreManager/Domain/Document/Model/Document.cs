namespace StoreManager.Domain.Document.Model
{
    public class Document
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public DateOnly Date { get; set; }
        public string FileName { get; set; } = string.Empty;
        public ICollection<DocumentChunk> Chunks { get; set; } = new List<DocumentChunk>();
    }
}
