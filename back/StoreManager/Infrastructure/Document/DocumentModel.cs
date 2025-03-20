

using System.ComponentModel.DataAnnotations;

namespace StoreManager.Infrastructure.Document
{
    public class DocumentModel
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public DateOnly Date { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }
}
