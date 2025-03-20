using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.SupaBase.Service;

namespace StoreManager.Infrastructure.Document.Service
{
    public sealed class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly SupabaseService _supaService;
        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
            _supaService = new SupabaseService();
        }
        public async Task<DocumentDownloadResponseDTO> DownloadFile(string fileName)
        {
            var file =  await _repository.FindByName(fileName);
            if (file == null) { throw new FileNotFoundException("Could not find the file"); }
            var response = await _supaService.DownloadFile(file);
            return response;
        }
        public async Task UploadFile(IFormFile file)
        {
            try
            {
                var savedFile = await _repository.SaveFile(file);
                await _supaService.UploadFile(file, savedFile);
            }
            catch (Exception)
            {
                throw new BadHttpRequestException("Failed to upload file!");
            }
        }
    }
}
