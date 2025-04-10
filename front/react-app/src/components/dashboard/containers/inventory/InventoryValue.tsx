import { useInventoryStats } from "./useInvetoryStats";
import InventoryValueChart from "./InventoryValueChart";
import DashBoardCard from "../cards/DashBoardCard";

const InventoryValue = () => {
  const { total, currentValue, prices } = useInventoryStats();
  return (
    <DashBoardCard
      maxValue={total}
      title={"Inventory value"}
      value={currentValue}
      chart={<InventoryValueChart data={prices} />}
    />
  );
};

export default InventoryValue;
