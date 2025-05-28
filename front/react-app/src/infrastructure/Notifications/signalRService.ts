import * as signalR from "@microsoft/signalr";
import { UserService } from "../Auth/UserService";

let connection: signalR.HubConnection | null = null;

export const getConnection =
  async (): Promise<signalR.HubConnection | null> => {
    const user = UserService.getUser();
    if (!user) {
      if (connection) {
        await connection.stop().catch(console.error);
        connection = null;
      }
      return null;
    }
    if (!connection) {
      connection = new signalR.HubConnectionBuilder()
        .withUrl("http://192.168.1.7:5205/hubs/notifications", {
          accessTokenFactory: () => user.accessToken,
          withCredentials: false,
        })
        .withAutomaticReconnect()
        .build();
    }

    if (connection.state === signalR.HubConnectionState.Disconnected) {
      await connection.start().catch(console.error);
    }

    return connection;
  };

export const stopConnection = async () => {
  if (connection) {
    await connection.stop().catch(console.error);
    connection = null;
  }
};
