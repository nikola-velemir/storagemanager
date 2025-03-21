import api from "../infrastructure/Interceptor/Interceptor";

export class DocumentService {
  static async GetDocumentByName(name: string) {
    return api.get(`docs/download/${name}`, {
      responseType: "blob",
    });
  }
  static async UploadDocument(file: File) {
    const formData = new FormData();
    formData.append("file", file);
    return api.post("docs/upload", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  }
  private static getFileChunks(file: File, chunksize = 1 * 1024 * 1024) {
    const chunks = [];
    let currentPosition = 0;

    while (currentPosition < file.size) {
      const chunk = file.slice(currentPosition, currentPosition + chunksize);
      chunks.push(chunk);
      currentPosition += chunksize;
    }
    return chunks;
  }
  static async UploadDocumentInChunks(file: File) {
    const chunks = this.getFileChunks(file);
    const totalChunks = chunks.length;

    for (let i = 0; i < totalChunks; ++i) {
      const chunk = chunks[i];
      const formData = new FormData();
      formData.append("file", chunk);
      formData.append("fileName", file.name);
      formData.append("chunkIndex", "" + i);
      formData.append("totalChunks", "" + totalChunks);
      try {
        const response = await api.post("/docs/upload-chunks", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });

        console.log(`Chunk ${i + 1} uploaded successfully!`);
      } catch (error) {
        console.error(`Error uploading chunk ${i + 1}:`, error);
      }
    }
  }
}
