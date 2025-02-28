const { app, BrowserWindow, protocol } = require("electron");
const path = require("path");
const url = require("url");

function createMainWindow() {
  const mainWindow = new BrowserWindow({
    width: 1000,
    height: 600,
    resizable: false,
    autoHideMenuBar: true,
    title: "Test",
    webPreferences: {},
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
}

app.on("ready", createMainWindow);
