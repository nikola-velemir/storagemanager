import api from "../infrastructure/Interceptor/Interceptor";
import { AuthUser } from "../model/User/AuthUser";
import { LoginRequest } from "../model/User/Request/LoginRequest";
import { RefreshRequest } from "../model/User/Request/RefreshRequest";

export class AuthService {
  private static baseUrl: string = "/auth";
  static async login(request: LoginRequest) {
    return api.post<AuthUser>(this.baseUrl + "/login", request);
  }
  static async logout() {
    return api.post(this.baseUrl + "/logout");
  }
  static async refreshAccess(request: RefreshRequest) {
    return api.post<AuthUser>(this.baseUrl + "/refresh", request);
  }
}
