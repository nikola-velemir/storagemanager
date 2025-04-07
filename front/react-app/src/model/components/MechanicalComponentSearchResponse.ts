import { MechanicalComponentInvoiceResponse } from "./MechanicalComponentInvoiceResponse";

export interface MechanicalComponentSearchResponse {
  id: string;
  name: string;
  identifier: string;
  invoices: MechanicalComponentInvoiceResponse[];
}
