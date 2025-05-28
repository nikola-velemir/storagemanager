import "./App.css";
import Layout from "./components/structure/Layout/Layout";
import { useEffect } from "react";
import api from "./infrastructure/Interceptor/Interceptor";
import {
  useAuthRedirect,
  useHailFailedRedirect,
} from "./infrastructure/Routes/RedirectHook";
import AnimatedRoutes from "./components/structure/AnimatedRoutes/AnimatedRoutes";
import { setupNotifications } from "./infrastructure/Notifications/notificationService";
import { useNavigate } from "react-router-dom";
const hailApp = async () => {
  try {
    await api.get("/hail");
  } catch (error) {
    console.error("App is offline", error);
  }
};
function App() {
  const navigate = useNavigate();
  useEffect(() => {
    const checkOnline = async () => {
      try {
        await hailApp();
      } catch (e) {}
    };
    checkOnline();
  }, []);
  useEffect(() => {
    setupNotifications(navigate);
  }, [navigate]);
  useAuthRedirect(); // Attach the logout listener
  useHailFailedRedirect();
  return (
    <Layout>
      <AnimatedRoutes></AnimatedRoutes>
    </Layout>
  );
}

export default App;
