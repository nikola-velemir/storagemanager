import logo from "./logo.svg";
import "./App.css";
import {
  Await,
  BrowserRouter,
  Link,
  Route,
  Routes,
  useLocation,
} from "react-router-dom";
import Layout from "./components/structure/Layout/Layout";
import LoginForm from "./components/login/LoginForm/LoginForm";
import Dashboard from "./components/dashboard/Dashboard";
import { useContext, useEffect } from "react";
import ProtectedRoute from "./infrastructure/Routes/ProtectedRoute";
import AuthUserContext from "./infrastructure/Interceptor/Auth/AuthContext";
import { GameService } from "./services/GameService";
import api from "./infrastructure/Interceptor/Interceptor";
import ContentContainer from "./components/structure/ContentContainer/ContentContainer";
import {
  useAuthRedirect,
  useHailFailedRedirect,
} from "./infrastructure/Routes/RedirectHook";
import HailFailed from "./components/errors/HailFailed";
import { AnimatePresence } from "framer-motion";
import AnimatedRoutes from "./components/structure/AnimatedRoutes/AnimatedRoutes";
const hailApp = async () => {
  try {
    await api.get("/hail");
  } catch (error) {
    console.error("App is offline", error);
  }
};
function App() {
  useEffect(() => {
    const checkOnline = async () => {
      try {
        await hailApp();
      } catch (e) {}
    };
    checkOnline();
  }, []);
  useAuthRedirect(); // Attach the logout listener
  useHailFailedRedirect();
  return (
    <Layout>
      <AnimatedRoutes></AnimatedRoutes>
    </Layout>
  );
}

export default App;
