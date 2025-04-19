namespace StoreManager.Application.Document
{
    public static class DocumentUtils
    {
        public static string GetRawMimeType(string fileType) => fileType switch
        {
            "pdf" => "pdf",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "txt" => "text/plain",
            "vnd.ms-excel" => "xlsx",
            "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "xlsx",
            _ => "application/octet-stream"
        };
        public static string GetPresentationalMimeType(string fileType) => fileType switch
        {
            "pdf" => "application/pdf",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            "txt" => "text/plain",
            "text/plain" => "text/plain",
            "vnd.ms-excel" => "application/vnd.ms-excel",
            "vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };
    }
}
