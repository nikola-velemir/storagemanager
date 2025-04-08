import { InvoiceUploadFormData } from "../components/invoice/upload/InvoiceUpload";
import api from "../infrastructure/Interceptor/Interceptor";
import { RequestDownloadResponse } from "../model/document/Response/RequestDownloadResponse";

export class DocumentService {
  static async requestDownload(fileName: string) {
    return api.get<RequestDownloadResponse>(
      `docs/request-download/${fileName}`
    );
  }
  static async downloadChunk(invoiceId: string, chunkIndex: number) {
    return api.get(`docs/download-chunk`, {
      params: {
        invoiceId: invoiceId,
        chunkIndex: chunkIndex,
      },
      responseType: "blob",
    });
  }

  static async downloadFile(
    invoiceId: string,
    onProgress: (progress: number) => void
  ) {
    const response = await this.requestDownload(invoiceId);
    const totalChunks = response.data.totalChunks;
    const allChunks: Blob[] = [];
    for (let i = 0; i < totalChunks; ++i) {
      const chunk = await this.downloadChunk(invoiceId, i);
      allChunks.push(chunk.data);
      onProgress(Math.trunc(((i + 1) / totalChunks) * 100));
    }
    return new Blob(allChunks, { type: response.data.type });
  }
  static async getDocumentByName(name: string) {
    return api.get(`docs/download/${name}`, {
      responseType: "blob",
    });
  }
  static async uploadDocument(file: File) {
    const formData = new FormData();
    formData.append("file", file);
    return api.post("docs/upload", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  }
  private static getFileChunks(file: File, chunksize = 0.5 * 1024 * 1024) {
    const chunks = [];
    let currentPosition = 0;

    while (currentPosition < file.size) {
      const chunk = file.slice(currentPosition, currentPosition + chunksize);
      chunks.push(chunk);
      currentPosition += chunksize;
    }
    return chunks;
  }
  static async uploadDocumentInChunks(
    provider: InvoiceUploadFormData,
    file: File,
    onProgress: (progress: number) => void
  ) {
    const chunks = this.getFileChunks(file);
    const totalChunks = chunks.length;

    for (let i = 0; i < totalChunks; ++i) {
      const chunk = chunks[i];
      const formData = new FormData();
      formData.append("provider", JSON.stringify(provider));
      formData.append("file", chunk);
      formData.append("fileName", file.name);
      formData.append("chunkIndex", "" + i);
      formData.append("totalChunks", "" + totalChunks);
      try {
        await api.post("/docs/upload-chunks", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });
        const progress = ((i + 1) / totalChunks) * 100;
        onProgress(progress);
      } catch (error) {
        console.error(`Error uploading chunk ${i + 1}:`, error);
      }
    }
  }
}
