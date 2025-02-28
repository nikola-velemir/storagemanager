const { app, BrowserWindow, protocol, ipcMain } = require("electron");
const path = require("path");
const url = require("url");

function createMainWindow() {
  const mainWindow = new BrowserWindow({
    width: 1000,
    height: 600,
    resizable: false,
    autoHideMenuBar: true,
    title: "Sreto",
    frame: false,
    webPreferences: {
      contextIsolation: true,
      enableRemoteModule: false,
      nodeIntegration: false,
      preload: __dirname + "/preload.js",
    },
  });
  const isDev = process.env.NODE_ENV === "development";
  const startUrl = "http://localhost:3000";
  //   const startUrl = isDev
  //     ? "http://localhost:3000"
  //     : url.format({
  //         pathname: path.join(__dirname, "./react-app/build/index.html"),
  //         protocol: "file",
  //       });
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
