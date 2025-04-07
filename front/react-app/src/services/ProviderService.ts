import api from "../infrastructure/Interceptor/Interceptor";
import { PaginatedResponse } from "../model/PaginatedResponse";
import { ProviderGetResponses } from "../model/provider/ProviderGetResponses";
import { ProviderSearchResponse } from "../model/provider/ProviderSearchResponse";

interface ProviderFilterRequest {
  pageNumber: number;
  pageSize: number;
  providerName: string | null;
}

export class ProviderService {
  private static BASE_URL: string = "/providers";
  public static async findAll() {
    return api.get<ProviderGetResponses>(this.BASE_URL);
  }
  public static async findFiltered(request: ProviderFilterRequest) {
    return api.get<PaginatedResponse<ProviderSearchResponse>>(
      this.BASE_URL + "/filtered",
      {
        params: {
          pageNumber: request.pageNumber,
          pageSize: request.pageSize,
          providerName: request.providerName,
        },
      }
    );
  }
}
