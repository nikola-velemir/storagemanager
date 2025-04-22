import { BusinessPartnerAddressResponse } from "./BusinessPartnerAddressResponse";
import { BusinessPartnerRoles } from "./BusinessPartnerRoles";

export interface BusinessPartnerProfileResponse {
  address: BusinessPartnerAddressResponse;
  name: string;
  partnerType: BusinessPartnerRoles;
  phoneNumber: string;
}
