import { app, BrowserWindow } from "electron";
import path from "path";

export function isDev() {
  return process.env.NODE_ENV === "development";
}

app.on("ready", () => {
  const mainWindow = new BrowserWindow({
    width: 800,
    height: 600,
    resizable: false,
    autoHideMenuBar: true,
  });

  mainWindow.loadURL("http://localhost:5123");
});
