import { ProductCreateRequestComponent } from "./ProductCreateRequestComponent";

export interface ProductCreateRequest {
  name: string;
  identifier: string;
  description: string;
  components: ProductCreateRequestComponent[];
}
