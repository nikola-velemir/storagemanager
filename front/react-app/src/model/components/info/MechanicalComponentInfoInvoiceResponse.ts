import { MechanicalComponentInfoProviderResponse } from "./MechanicalComponentInfoProviderResponse";

export interface MechanicalComponentInfoInvoiceResponse {
  id: string;
  dateIssued: string;
  provider: MechanicalComponentInfoProviderResponse;
}
