import api from "../infrastructure/Interceptor/Interceptor";
import { CreateExportRequest } from "../model/invoice/export/CreateExportRequest";
import { ExportSearchResponse } from "../model/invoice/export/ExportSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";

interface FilterExportsRequest {
  pageNumber: number;
  pageSize: number;
}

export class ExportService {
  private static BASE_URL = "/exports";
  public static async create(request: CreateExportRequest) {
    return api.post(this.BASE_URL, request);
  }
  public static async findFiltered(request: FilterExportsRequest) {
    return api.get<PaginatedResponse<ExportSearchResponse>>(
      this.BASE_URL + "/filtered",
      {
        params: {
          pageNumber: request.pageNumber,
          pageSize: request.pageSize,
        },
      }
    );
  }
}
