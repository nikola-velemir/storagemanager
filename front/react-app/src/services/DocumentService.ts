import downloadApi from "../infrastructure/Interceptor/DownloadInterceptor";
import api from "../infrastructure/Interceptor/Interceptor";
import { AxiosResponse } from "axios";
import { RequestDownloadResponse } from "../model/document/RequestDownloadResponse";
import { ChunkDownloadResponse } from "../model/document/ChunkDownloadResponse";

export class DocumentService {
  private static getContentFile(response: AxiosResponse) {
    let contentType: string = "application/octet-stream";
    if (response.headers && response.headers["content-type"]) {
      const type = response.headers["content-type"];

      if (typeof type === "string") {
        contentType = type;
      } else if (Array.isArray(type)) {
        contentType = type[0];
      }
    }
    return contentType;
  }
  static async RequestDownload(fileName: string) {
    return api.get<RequestDownloadResponse>(
      `docs/request-download/${fileName}`
    );
  }
  static async DownloadChunk(fileName: string, chunkIndex: number) {
    return api.get(`docs/download-chunk`, {
      params: {
        fileName: fileName,
        chunkIndex: chunkIndex,
      },
      responseType: "blob",
    });
  }

  static concatenateByteArrays(byteArrays: Uint8Array[]): Uint8Array {
    let totalLength = byteArrays.reduce(
      (sum, byteArray) => sum + byteArray.length,
      0
    );
    let combined = new Uint8Array(totalLength);
    let offset = 0;

    for (let byteArray of byteArrays) {
      combined.set(byteArray, offset);
      offset += byteArray.length;
    }

    return combined;
  }
  static async DownloadFile(
    fileName: string,
    onProgress: (progress: number) => void
  ) {
    const response = await this.RequestDownload(fileName);
    const totalChunks = response.data.totalChunks;
    const allChunks: Blob[] = [];
    for (let i = 0; i < totalChunks; ++i) {
      const chunk = await this.DownloadChunk(fileName, i);
      allChunks.push(chunk.data);
      onProgress(Math.trunc(((i + 1) / totalChunks) * 100));
    }
    return new Blob(allChunks, { type: response.data.type });
  }
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
  static async UploadDocumentInChunks(
    file: File,
    onProgress: (progress: number) => void
  ) {
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
        const progress = ((i + 1) / totalChunks) * 100;
        onProgress(progress);
      } catch (error) {
        console.error(`Error uploading chunk ${i + 1}:`, error);
      }
    }
  }
}
