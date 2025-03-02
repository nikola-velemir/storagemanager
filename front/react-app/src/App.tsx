import logo from "./logo.svg";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import { BrowserRouter, Link, Route, Routes } from "react-router-dom";
import Layout from "./components/structure/Layout/Layout";
import LoginForm from "./components/login/LoginForm/LoginForm";
import Dashboard from "./components/dashboard/Dashboard";
import { useContext, useEffect } from "react";
import ProtectedRoute from "./infrastructure/ProtectedRoute";
import AuthUserContext from "./infrastructure/AuthContext";
import { GameService } from "./services/GameService";
import api from "./infrastructure/Interceptor";

function App() {
  const user = useContext(AuthUserContext);
  const gameService = new GameService();
  useEffect(() => {
    gameService
      .getGames()
      .then((value) => {
        console.log(value.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/login" element={<LoginForm />} />
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<Dashboard />} />
            <Route path="/dashboard" element={<Dashboard />} />
          </Route>
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

const Navigation = () => (
  <nav>
    <Link to="/">Dashboard</Link>
    <Link to="/login">Login</Link>
  </nav>
);
export default App;
