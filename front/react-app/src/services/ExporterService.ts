import api from "../infrastructure/Interceptor/Interceptor";
import { ExporterCreateRequest } from "../model/exporter/ExporterCreateRequest";
import { FindExporterResponse } from "../model/exporter/FindExporterResponse";
import { FindExporterResponses } from "../model/exporter/FindExporterResponses";

export class ExporterService {
  static async findAll() {
    return api.get<FindExporterResponses>(this.BASE_URL);
  }
  private static BASE_URL = "/exporters";
  public static async create(request: ExporterCreateRequest) {
    return api.post(this.BASE_URL, request);
  }
}
