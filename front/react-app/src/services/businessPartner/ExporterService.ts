import api from "../../infrastructure/Interceptor/Interceptor";
import { ExporterCreateRequest } from "../../model/exporter/ExporterCreateRequest";
import { ExporterSearchResponse } from "../../model/exporter/ExporterSearchResponse";
import { FindExporterResponses } from "../../model/exporter/FindExporterResponses";
import { PaginatedResponse } from "../../model/PaginatedResponse";

interface ExporterFilterRequest {
  exporterInfo: string | null;
  pageNumber: number;
  pageSize: number;
}

export class ExporterService {
  private static BASE_URL = "/exporters";
  static async findFiltered(request: ExporterFilterRequest) {
    return api.get<PaginatedResponse<ExporterSearchResponse>>(
      this.BASE_URL + "/filtered",
      {
        params: {
          pageNumber: request.pageNumber,
          pageSize: request.pageSize,
          exporterInfo: request.exporterInfo,
        },
      }
    );
  }
  static async findAll() {
    return api.get<FindExporterResponses>(this.BASE_URL);
  }
  public static async create(request: ExporterCreateRequest) {
    return api.post(this.BASE_URL, request);
  }
}
