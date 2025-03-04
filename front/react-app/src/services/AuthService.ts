import api from "../infrastructure/Interceptor";
import { AuthUser } from "../model/User/AuthUser";
import { LoginRequest } from "../model/User/Request/LoginRequest";

export class AuthService {
  static async login(request: LoginRequest) {
    return api.post<AuthUser>("/auth/login", request);
  }
  static async logout() {
    return api.post("/auth/logout");
  }
}
