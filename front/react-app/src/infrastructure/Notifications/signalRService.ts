import * as signalR from "@microsoft/signalr";
import { UserService } from "../Auth/UserService";

let connection: signalR.HubConnection | null = null;

export const getConnection = () => {
  if (!connection) {
    const user = UserService.getUser();
    connection = new signalR.HubConnectionBuilder()
      .withUrl("http://192.168.1.9:5205/hubs/notifications", {
        accessTokenFactory: () => (user ? user.accessToken : ""),
        withCredentials: false,
      })
      .withAutomaticReconnect()
      .build();
  }
  return connection;
};
