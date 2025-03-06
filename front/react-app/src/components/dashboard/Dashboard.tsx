import React from "react";
import dummy from "../../assets/Bhpd8.jpg";
import SuccessButton from "../common/SuccessButton/SuccessButton";
import { GameService } from "../../services/GameService";
import { useAuth } from "../../infrastructure/Interceptor/Auth/AuthContext";
import api from "../../infrastructure/Interceptor/Interceptor";

const Dashboard = () => {
  const userContext = useAuth();
  const fetchGames = () => {
    GameService.getGames()
      .then((resolve) => {
        console.log(resolve.data);
      })
      .catch(() => {});
  };
  return (
    <div>
      <SuccessButton text="Send Request" onClick={fetchGames} />
    </div>
  );
};

export default Dashboard;
