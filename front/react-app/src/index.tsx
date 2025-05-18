import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import App from "./App";
import { HashRouter } from "react-router-dom";
import { AuthUserProvider } from "./infrastructure/Auth/AuthContext";
import NotificationHandler from "./infrastructure/Notifications/NotificationHandler";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <AuthUserProvider>
      <HashRouter>
        <NotificationHandler />
        <App />
      </HashRouter>
    </AuthUserProvider>
  </React.StrictMode>
);

reportWebVitals();
