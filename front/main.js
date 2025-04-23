const { app, BrowserWindow, protocol, ipcMain } = require("electron");
const path = require("path");
const url = require("url");

function createMainWindow() {
  const mainWindow = new BrowserWindow({
    width: 1200,
    height: 800,
    minHeight: 800,
    maxHeight: 1200,
    resizable: true,
    autoHideMenuBar: true,
    title: "Sreto",
    titleBarStyle: "hiddenInset",
    icon: path.join(__dirname, "assets", "icon.ico"),
    frame: false,
    webPreferences: {
      contextIsolation: true,
      enableRemoteModule: false,
      nodeIntegration: false,
      devTools: true,
      preload: __dirname + "/preload.js",
      devTools: true,
    },
  });
  const isDev = process.env.NODE_ENV === "development";
  const startUrl = url.format({
    pathname: path.join(__dirname, "./react-app/build/index.html"),
    protocol: "file",
  });
  mainWindow.loadURL(startUrl);

  ipcMain.on("window-minimize", () => mainWindow.minimize());
  ipcMain.on("window-maximize", () => {
    if (mainWindow.isMaximized()) {
      mainWindow.unmaximize();
    } else {
      mainWindow.maximize();
    }
  });
  ipcMain.on("window-close", () => mainWindow.close());
}

app.on("ready", createMainWindow);
