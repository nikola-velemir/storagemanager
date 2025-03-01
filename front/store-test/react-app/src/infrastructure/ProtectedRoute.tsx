import { Navigate, Outlet } from "react-router-dom";

import { useAuth } from "./AuthContext";

type ProtectedRouteProps = {
  redirectPath?: string;
};

const ProtectedRoute = ({ redirectPath = "/login" }: ProtectedRouteProps) => {
  const user = useAuth();
  if (!user.getUser()) {
    return <Navigate to={redirectPath} replace />;
  }
  return <Outlet />;
};

export default ProtectedRoute;
