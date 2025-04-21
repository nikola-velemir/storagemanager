import { BusinessPartnerRoles } from "./BusinessPartnerRoles";

export interface BusinessPartnerCreateRequest {
  name: string;
  city: string;
  street: string;
  streetNumber: string;
  phoneNumber: string;
  role: BusinessPartnerRoles;
}
