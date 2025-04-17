import { ExportSearchProductResponse } from "./ExportSearchProductResponse";

export interface ExportSearchResponse {
  id: string;
  date: string;
  exporterName: string;
  products: ExportSearchProductResponse[];
}
