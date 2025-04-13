export interface ProductFilterRequest {
  pageNumber: number;
  pageSize: number;
  dateCreated: string | null;
  productInfo: string | null;
}
