import api from "../infrastructure/Interceptor/Interceptor";
import { CreateExportRequest } from "../model/invoice/export/CreateExportRequest";

export class ExportService {
  private static BASE_URL = "/exports";
  public static async create(request: CreateExportRequest) {
    return api.post(this.BASE_URL, request);
  }
}
