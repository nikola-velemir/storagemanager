import { ProductCreateRequestComponent } from "./ProductCreateRequestComponent";

export interface ProductCreateRequest {
  name: string;
  description: string;
  components: ProductCreateRequestComponent[];
}
