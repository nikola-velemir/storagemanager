import { Navigate, Outlet } from "react-router-dom";

import { UserService } from "../Auth/UserService";

type ProtectedRouteProps = {
  redirectPath?: string;
};

const ProtectedRoute = ({ redirectPath = "/login" }: ProtectedRouteProps) => {
  const user = UserService.getUser();
  console.log(user);
  if (!user) {
    return <Navigate to={redirectPath} replace />;
  }
  return <Outlet />;
};

export default ProtectedRoute;
