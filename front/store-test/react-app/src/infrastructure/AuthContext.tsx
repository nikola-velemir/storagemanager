import React, {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { AuthUser } from "../model/user/AuthUser";

interface AuthContextType {
  user: AuthUser | null;
  loading: boolean;
  setUser: (user: AuthUser) => void;
  clearUser: () => void;
}

interface AuthUserProviderProps {
  children: ReactNode;
}

export const AuthUserContext = createContext<
  AuthContextType | undefined | null
>(undefined);

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthUserContext);
  if (!context) {
    throw new Error("useAuthUser must be used within an AuthUserProvider");
  }
  return context;
};

export const AuthUserProvider = ({ children }: AuthUserProviderProps) => {
  const [user, setUserState] = useState<AuthUser | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const savedUser = localStorage.getItem("user");
    if (savedUser) {
      setUserState(JSON.parse(savedUser));
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    if (user) {
      localStorage.setItem("user", JSON.stringify(user));
    } else {
      localStorage.removeItem("user");
    }
  }, [user]);

  const setUser = (user: AuthUser) => {
    localStorage.setItem("user", JSON.stringify(user));
    setUserState(user);
  };
  const clearUser = () => {
    setUserState(null);
  };
  return (
    <AuthUserContext.Provider value={{ user, loading, setUser, clearUser }}>
      {children}
    </AuthUserContext.Provider>
  );
};
export default AuthUserContext;
