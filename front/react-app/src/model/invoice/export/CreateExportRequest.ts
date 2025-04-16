import { ProductSelectionTuple } from "../../../components/features/export/create/ExportCreatePage";

export interface CreateExportRequest {
  providerId: string;
  products: ProductSelectionTuple[];
}
