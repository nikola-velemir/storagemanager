import { Navigate, Outlet } from "react-router-dom";

import React from "react";
import { AuthUser } from "../model/user/AuthUser";
import { useAuth } from "./AuthContext";

type ProtectedRouteProps = {
  redirectPath?: string;
};

const ProtectedRoute = ({ redirectPath = "/login" }: ProtectedRouteProps) => {
  const user = useAuth();
  if (!user.user) {
    console.log(user);
    return <Navigate to={redirectPath} replace />;
  }
  console.log(user);
  return <Outlet />;
};

export default ProtectedRoute;
