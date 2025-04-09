import React, { useState } from "react";
import SuccessButton from "../common/buttons/SuccessButton/SuccessButton";
import { GameService } from "../../services/GameService";
import { useAuth } from "../../infrastructure/Auth/AuthContext";
import InvoicesThisWeek from "./containers/invoice/InvoicesThisWeek";
import MechanicalComponentCount from "./containers/component/MechanicalComponentCount";

const Dashboard = () => {
  const userContext = useAuth();
  const fetchGames = () => {
    GameService.getGames()
      .then((resolve) => {
        console.log(resolve.data);
      })
      .catch(() => {});
  };

  const [isDocumentModalOpen, setIsDocumentModalOpen] = useState(false);
  const toggleDocumentModal = () => {
    setIsDocumentModalOpen(!isDocumentModalOpen);
  };
  return (
    <div className="w-full h-screen p-8">
      <div className="w-full h-5/6 overflow-y-scroll">
        <div className="w-full flex flex-row">
          <div className="flex w-1/2 p-4">
            <InvoicesThisWeek />
          </div>
          <div className="flex w-1/2 p-4">
            <MechanicalComponentCount />
          </div>
        </div>
        <div className="w-full flex flex-row">
          <div className="flex w-2/3 p-4">
            <MechanicalComponentCount />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
