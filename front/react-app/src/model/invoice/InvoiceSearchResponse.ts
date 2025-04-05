import { InvoiceSearchComponentResponse } from "./InvoiceSearchComponentResponse";
import { InvoiceSearchProviderResponse } from "./InvoiceSearchProviderResponse";

export interface InvoiceSearchResponse {
  id: string;
  date: string;
  provider: InvoiceSearchProviderResponse;
  components: InvoiceSearchComponentResponse[];
}
