import { Subscription } from "rxjs";
import { UserService } from "../Auth/UserService";
import { getConnection, stopConnection } from "./signalRService";

let subscription: Subscription | null = null;
let eventHandlersBound = false;

export const initializeSignalRUserWatcher = (
  onDocumentProccessed: (data: any) => void
) => {
  if (subscription) return;
  subscription = UserService.user$.subscribe(async (user) => {
    if (!user) {
      await stopConnection();
      eventHandlersBound = false;
      return;
    }
    const connection = await getConnection();
    if (!connection || eventHandlersBound) return;
    connection.on("DocumentProcessed", onDocumentProccessed);
    eventHandlersBound = true;
  });
};

export const disposeSignalRUserWatcher = () => {
  subscription?.unsubscribe();
  subscription = null;
  eventHandlersBound = false;
  stopConnection();
};
