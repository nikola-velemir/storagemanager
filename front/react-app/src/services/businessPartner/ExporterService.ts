import api from "../../infrastructure/Interceptor/Interceptor";
import { ExporterCreateRequest } from "../../model/exporter/ExporterCreateRequest";
import { ExporterExportInvolvementResponses } from "../../model/exporter/ExporterExportInvovlementResponses";
import { ExporterProductInvolvementResponse } from "../../model/exporter/ExporterProductInvolvementResponse";
import { ExporterSearchResponse } from "../../model/exporter/ExporterSearchResponse";
import { FindExporterResponses } from "../../model/exporter/FindExporterResponses";
import { PaginatedResponse } from "../../model/PaginatedResponse";

interface ExporterFilterRequest {
  exporterInfo: string | null;
  pageNumber: number;
  pageSize: number;
}

export class ExporterService {
  public static async findExporterProductInvolvement() {
    return api.get<{ products: ExporterProductInvolvementResponse[] }>(
      this.BASE_URL + "/find-product-involvement"
    );
  }
  public static async findExporterInvoiceInvolvement() {
    return api.get<ExporterExportInvolvementResponses>(
      this.BASE_URL + "/find-invoice-involvement"
    );
  }
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
