import { BusinessPartnerRoles } from "./BusinessPartnerRoles";

export interface BusinessPartnerCreateRequest {
  name: string;
  address: string;
  phoneNumber: string;
  role: BusinessPartnerRoles;
}
