import api from "../../infrastructure/Interceptor/Interceptor";
import { BusinessPartnerCreateRequest } from "../../model/businessPartner/BusinessPartnerCreateRequest";

export class BusinessPartnerService {
  private static BASE_URL = "/business-partners";
  public static async create(request: BusinessPartnerCreateRequest) {
    return api.post(this.BASE_URL, request);
  }
  public static async findPartnerProfile(id: string) {
    return api.get(this.BASE_URL + "/info/" + id);
  }
}
