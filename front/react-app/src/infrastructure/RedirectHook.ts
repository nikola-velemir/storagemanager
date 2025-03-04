import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const useAuthRedirect = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const handleLogout = () => {
      navigate("/login");
    };

    window.addEventListener("forcedLogout", handleLogout);
    return () => {
      window.removeEventListener("forcedLogout", handleLogout);
    };
  }, [navigate]);
};

export const useHailFailedRedirect = () => {
  const navigate = useNavigate();
  useEffect(() => {
    const handleHailFailed = () => {
      navigate("/hailFailed");
    };
    window.addEventListener("hailFailed", handleHailFailed);
    return () => {
      window.removeEventListener("hailFailed", handleHailFailed);
    };
  }, [navigate]);
};
