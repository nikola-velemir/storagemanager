namespace StoreManager.Domain.Document.Model
{
    public class DocumentChunk
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public int ChunkNumber { get; set; }
        public string SupaBasePath { get; set; } = string.Empty;
        public required Document Document { get; set; }
    }
}
