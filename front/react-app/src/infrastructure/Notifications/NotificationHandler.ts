import * as signalR from "@microsoft/signalr";
import React, { use, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { UserService } from "../Auth/UserService";
import { getConnection } from "./signalRService";

const NotificationHandler = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const connection = getConnection();

    const handleDocumentProcessed = (data: any) => {
      toast.info(
        `Document ${data.fileName} processed under invoice ID ${data.documentId}`,
        {
          onClick: () => navigate(`/invoice-info/${data.documentId}`),
          style: { cursor: "pointer" },
        }
      );
    };

    connection.on("DocumentProcessed", handleDocumentProcessed);

    connection
      .start()
      .catch((err) => console.error("SignalR Connection Error:", err));

    return () => {
      connection.off("DocumentProcessed", handleDocumentProcessed);
    };
  }, [navigate]);

  return null;
};

export default NotificationHandler;
