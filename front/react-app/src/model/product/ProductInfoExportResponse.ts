import { ProductInfoExporterResponse } from "./ProductInfoExporterResponse";

export interface ProductInfoExportResponse {
  id: string;
  date: string;
  exporter: ProductInfoExporterResponse;
}
