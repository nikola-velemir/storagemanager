import { ProductSelectionTuple } from "../../../components/features/invoice/export/create/ExportCreatePage";

export interface CreateExportRequest {
  providerId: string;
  products: ProductSelectionTuple[];
}
