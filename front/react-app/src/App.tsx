import "./App.css";
import Layout from "./components/structure/Layout/Layout";
import { useEffect } from "react";
import api from "./infrastructure/Interceptor/Interceptor";
import {
  useAuthRedirect,
  useHailFailedRedirect,
} from "./infrastructure/Routes/RedirectHook";
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
