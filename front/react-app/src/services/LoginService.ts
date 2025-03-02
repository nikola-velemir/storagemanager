import api from "../infrastructure/Interceptor";
import { AuthUser } from "../model/User/AuthUser";
import { LoginRequest } from "../model/User/Request/LoginRequest";

export class LoginService {
  static async login(request: LoginRequest) {
    return api.post<AuthUser>("/auth/login", request);
  }
}
