import { ProviderProfileComponentResponse } from "./ProviderProfileComponentResponse";
import { ProviderProfileInvoiceResponse } from "./ProviderProfileInvoiceResponse";

export interface ProviderProfileResponse {
  name: string;
  address: string;
  phoneNumber: string;
  invoices: ProviderProfileInvoiceResponse[];
  components: ProviderProfileComponentResponse[];
}
