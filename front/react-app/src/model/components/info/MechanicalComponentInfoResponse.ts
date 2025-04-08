import { MechanicalComponentInfoInvoiceResponse } from "./MechanicalComponentInfoInvoiceResponse";

export interface MechanicalComponentInfoResponse {
  name: string;
  identifier: string;
  quantity: number;
  invoices: MechanicalComponentInfoInvoiceResponse[];
}
