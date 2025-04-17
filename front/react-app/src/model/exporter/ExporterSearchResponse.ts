import { ExporterSearchExportResponse } from "./ExporterSearchExportResponse";

export interface ExporterSearchResponse {
  id: string;
  name: string;
  address: string;
  phoneNumber: string;
  exports: ExporterSearchExportResponse[];
}
