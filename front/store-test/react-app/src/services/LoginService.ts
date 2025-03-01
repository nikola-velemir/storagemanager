import { AuthUser } from "../model/user/AuthUser";

export class LoginService {
  static login(user: AuthUser): void {
    localStorage.setItem("user", JSON.stringify(user));
  }
}
