import { BehaviorSubject } from "rxjs";
import { AuthUser } from "../../model/userModels/AuthUser";

const storedUser = localStorage.getItem("user");
const initialUser: AuthUser | null = storedUser ? JSON.parse(storedUser) : null;

export class UserService {
  private static userSubject = new BehaviorSubject<AuthUser | null>(
    initialUser
  );
  static user$ = this.userSubject.asObservable();

  static getUser = (): AuthUser | null => {
    return this.userSubject.getValue();
  };
  static setUser = (user: AuthUser): void => {
    localStorage.setItem("user", JSON.stringify(user));
    this.userSubject.next(user);
  };
  static clearUser = (): void => {
    localStorage.removeItem("user");
    this.userSubject.next(null);
  };
}
