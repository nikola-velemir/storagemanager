namespace StoreManager.Domain.Document.Model
{
    public class DocumentModel
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public DateOnly Date { get; set; }
        public string FileName { get; set; } = string.Empty;
        public ICollection<DocumentChunkModel> Chunks { get; set; } = new List<DocumentChunkModel>();
    }
}
