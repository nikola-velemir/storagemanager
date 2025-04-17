import { ImportSearchComponentResponse } from "./ImportSearchComponentResponse";
import { ImportSearchProviderResponse } from "./ImportSearchProviderResponse";

export interface ImportSearchResponse {
  id: string;
  date: string;
  provider: ImportSearchProviderResponse;
  components: ImportSearchComponentResponse[];
}
