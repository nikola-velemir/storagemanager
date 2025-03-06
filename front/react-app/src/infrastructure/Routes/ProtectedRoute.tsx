import { Navigate, Outlet } from "react-router-dom";

import { useAuth } from "../Auth/AuthContext";
import { UserService } from "../Auth/UserService";

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
