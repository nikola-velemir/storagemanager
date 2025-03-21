namespace StoreManager.Infrastructure.Document.Model
{
    public class DocumentChunkModel
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public int ChunkNumber { get; set; }
        public string SupaBasePath { get; set; } = string.Empty;
    }
}
