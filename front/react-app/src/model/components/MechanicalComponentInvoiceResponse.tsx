import { MechanicalComponentProviderResponse } from "./MechanicalComponentProviderResponse";

export interface MechanicalComponentInvoiceResponse {
  dateIssued: string;
  id: string;
  provider: MechanicalComponentProviderResponse;
}
