import { MechanicalComponentSearchProviderResponse } from "./MechanicalComponentSearchProviderResponse";

export interface MechanicalComponentSearchInvoiceResponse {
  dateIssued: string;
  id: string;
  provider: MechanicalComponentSearchProviderResponse;
}
