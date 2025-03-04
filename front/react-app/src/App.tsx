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
import ProtectedRoute from "./infrastructure/ProtectedRoute";
import AuthUserContext from "./infrastructure/AuthContext";
import { GameService } from "./services/GameService";
import api from "./infrastructure/Interceptor";
import ContentContainer from "./components/structure/ContentContainer/ContentContainer";
import {
  useAuthRedirect,
  useHailFailedRedirect,
} from "./infrastructure/RedirectHook";
import HailFailed from "./components/errors/HailFailed";
import { AnimatePresence } from "framer-motion";
const hailApp = async () => {
  try {
    await api.get("/hail");
  } catch (error) {
    console.error("App is offline", error);
  }
};
function App() {
  const user = useContext(AuthUserContext);
  const gameService = new GameService();
  useEffect(() => {
    const checkOnline = async () => {
      try {
        await hailApp();
      } catch (e) {}
    };
    checkOnline();
  });
  useAuthRedirect(); // Attach the logout listener
  useHailFailedRedirect();
  const location = useLocation();
  return (
    <Layout>
      <>
        <AnimatePresence mode="wait">
          <Routes location={location} key={location.pathname}>
            <Route path="/login" element={<LoginForm />} />
            <Route element={<ProtectedRoute />}>
              <Route
                path="/"
                element={
                  <ContentContainer>
                    <Dashboard />
                  </ContentContainer>
                }
              ></Route>
              <Route
                path="/kurac"
                element={
                  <ContentContainer>
                    <HailFailed />
                  </ContentContainer>
                }
              ></Route>
            </Route>
            <Route path="/hailFailed" element={<HailFailed />} />
          </Routes>
        </AnimatePresence>
      </>
    </Layout>
  );
}

const Navigation = () => (
  <nav>
    <Link to="/">Dashboard</Link>
    <Link to="/login">Login</Link>
  </nav>
);
export default App;
