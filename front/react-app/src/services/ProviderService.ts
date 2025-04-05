import api from "../infrastructure/Interceptor/Interceptor";
import { ProviderCreateRequest } from "../model/provider/ProviderCreateRequest";
import { ProviderGetResponse } from "../model/provider/ProviderGetResponse";
import { ProviderGetResponses } from "../model/provider/ProviderGetResponses";

export class ProviderService {
  public static async FindAll() {
    return api.get<ProviderGetResponses>("/providers");
  }
}
