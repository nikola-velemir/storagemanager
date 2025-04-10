import React from "react";
import { useInventoryStats } from "./useInvetoryStats";
import InventoryValueChart from "./InventoryValueChart";
import DashBoardCard from "../cards/DashBoardCard";

const InventoryValue = () => {
  const { total, prices } = useInventoryStats();
  return (
    <DashBoardCard
      maxValue={total}
      title={"Inventory value"}
      value={total}
      chart={<InventoryValueChart data={prices} />}
    />
  );
};

export default InventoryValue;
