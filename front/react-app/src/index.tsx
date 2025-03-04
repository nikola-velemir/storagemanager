import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import App from "./App";
import { AuthUserProvider } from "./infrastructure/AuthContext";
import { BrowserRouter } from "react-router-dom";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <AuthUserProvider>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </AuthUserProvider>
  </React.StrictMode>
);

reportWebVitals();
