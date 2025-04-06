import api from "../infrastructure/Interceptor/Interceptor";

export interface MechanicalComponentFilterRequest {
  pageSize: number;
  pageNumber: number;
}
export class MechanicalComponentService {
  public static async findFiltered(request: MechanicalComponentFilterRequest) {
    return api.get("/components/filtered", {
      params: {
        pageSize: request.pageSize,
        pageNumber: request.pageNumber,
      },
    });
  }
}
