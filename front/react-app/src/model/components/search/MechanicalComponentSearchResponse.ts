import { MechanicalComponentSearchInvoiceResponse } from "./MechanicalComponentSearchInvoiceResponse";

export interface MechanicalComponentSearchResponse {
  id: string;
  name: string;
  identifier: string;
  invoices: MechanicalComponentSearchInvoiceResponse[];
}
