import api from "../infrastructure/Interceptor/Interceptor";
import { ExporterCreateRequest } from "../model/exporter/ExporterCreateRequest";

export class ExporterService {
  private static BASE_URL = "/exporters";
  public static async create(request: ExporterCreateRequest) {
    return api.post(this.BASE_URL, request);
  }
}
