using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.Infrastructure.Document.Model
{
    public class DocumentModel
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public DateOnly Date { get; set; }
        public string FileName { get; set; } = string.Empty;
        public long ChunkCount { get; set; }
        public ICollection<DocumentChunkModel> Chunks { get; set; } = new List<DocumentChunkModel>();
    }
}
