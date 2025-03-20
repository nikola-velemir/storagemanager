import api from "../infrastructure/Interceptor/Interceptor";

export class DocumentService {
  static async GetDocumentByName(name: string) {
    return api.get(`docs/download/${name}`, {
      responseType: "blob",
    });
  }
}
