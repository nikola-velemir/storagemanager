import { ProductInfoComponentResponse } from "./ProductInfoComponentResponse";
import { ProductInfoExportResponse } from "./ProductInfoExportResponse";

export interface ProductInfoResponse {
  identifier: string;
  name: string;
  description: string;
  dateCreated: string;
  components: ProductInfoComponentResponse[];
  exports: ProductInfoExportResponse[];
}
