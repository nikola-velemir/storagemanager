import React from "react";
import dummy from "../../assets/Bhpd8.jpg";
import SuccessButton from "../common/SuccessButton/SuccessButton";
import { GameService } from "../../services/GameService";

const Dashboard = () => {
  const fetchGames = () => {
    GameService.getGames().then((resolve) => console.log(resolve.data));
  };
  return (
    <div>
      <SuccessButton text="Send Request" onClick={fetchGames} />
    </div>
  );
};

export default Dashboard;
