import { InvoiceSearchResponse } from "./InvoiceSearchResponse";

export interface InvoiceSearchResponses {
  responses: {
    items: InvoiceSearchResponse[];
    pageNumber: number;
    pageSize: number;
    totalCount: number;
  };
}
