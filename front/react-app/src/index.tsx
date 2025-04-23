import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import App from "./App";
import { AuthUserProvider } from "./infrastructure/Auth/AuthContext";
import { HashRouter } from "react-router-dom";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <AuthUserProvider>
      <HashRouter>
        <App />
      </HashRouter>
    </AuthUserProvider>
  </React.StrictMode>
);

reportWebVitals();
