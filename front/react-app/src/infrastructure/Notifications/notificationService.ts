import { toast } from "react-toastify";
import { NavigateFunction } from "react-router-dom";
import { initializeSignalRUserWatcher } from "./signalRManager";

let initialized = false;

export const setupNotifications = (navigate: NavigateFunction) => {
  if (initialized) return;
  initialized = true;

  initializeSignalRUserWatcher((data: any) => {
    toast.info(
      `Document ${data.fileName} processed under invoice ID ${data.documentId}`,
      {
        onClick: () => navigate(`/invoice-info/${data.documentId}`),
        style: { cursor: "pointer" },
      }
    );
  });
};
