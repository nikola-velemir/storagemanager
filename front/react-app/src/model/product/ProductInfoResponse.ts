import { ProductInfoComponentResponse } from "./ProductInfoComponentResponse";

export interface ProductInfoResponse {
  identifier: string;
  name: string;
  description: string;
  dateCreated: string;
  components: ProductInfoComponentResponse[];
}
