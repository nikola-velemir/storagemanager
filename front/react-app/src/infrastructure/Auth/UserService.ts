import { AuthUser } from "../../model/userModels/AuthUser";

export class UserService {
  static getUser = (): AuthUser | null => {
    const userString = localStorage.getItem("user");
    return userString ? JSON.parse(userString) : null;
  };
  static setUser = (user: AuthUser): void => {
    localStorage.setItem("user", JSON.stringify(user));
  };
  static clearUser = (): void => {
    localStorage.removeItem("user");
  };
}
