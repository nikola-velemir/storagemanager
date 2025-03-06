import { Navigate, Outlet } from "react-router-dom";

import { useAuth } from "../Interceptor/Auth/AuthContext";
import { UserService } from "../Interceptor/Auth/UserService";

type ProtectedRouteProps = {
  redirectPath?: string;
};

const ProtectedRoute = ({ redirectPath = "/login" }: ProtectedRouteProps) => {
  const user = UserService.getUser();
  if (!user) {
    return <Navigate to={redirectPath} replace />;
  }
  return <Outlet />;
};

export default ProtectedRoute;
