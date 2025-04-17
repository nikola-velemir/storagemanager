import api from "../infrastructure/Interceptor/Interceptor";
import { PaginatedResponse } from "../model/PaginatedResponse";
import { ProviderComponentInvolvementResponses } from "../model/provider/ProviderComponentInvolvementResponses";
import { ProviderGetResponses } from "../model/provider/ProviderGetResponses";
import { ProviderImportInvolvementResponses } from "../model/provider/ProviderImportInvolvementResponses";
import { ProviderProfileResponse } from "../model/provider/ProviderProfileResponse";
import { ProviderSearchResponse } from "../model/provider/ProviderSearchResponse";

interface ProviderFilterRequest {
  pageNumber: number;
  pageSize: number;
  providerName: string | null;
}

export class ProviderService {
  private static BASE_URL: string = "/providers";
  public static async findProviderInvoiceInvolvement() {
    return api.get<ProviderImportInvolvementResponses>(
      this.BASE_URL + "/find-invoice-involvement"
    );
  }
  public static async findProviderComponentInvolvement() {
    return api.get<ProviderComponentInvolvementResponses>(
      this.BASE_URL + "/find-component-involvement"
    );
  }
  public static async findProviderProfile(providerId: string) {
    return api.get<ProviderProfileResponse>(
      this.BASE_URL + "/profile/" + providerId
    );
  }
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
