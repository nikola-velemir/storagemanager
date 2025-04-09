import { useEffect } from "react";
import DashBoardCard from "../cards/DashBoardCard";
import ProviderInvolvementPieChart from "./ProviderInvolvementPieChart";
import { useProviderStats } from "./useProviderStats";

const ProviderInvolvement = () => {
  const { count, maxCount, providers } = useProviderStats();
  useEffect(() => {
    console.log(count);
    console.log(maxCount);
  }, [count, maxCount]);
  return (
    <DashBoardCard
      maxValue={maxCount}
      title={"Registered providers"}
      value={count}
      chart={<ProviderInvolvementPieChart data={providers} />}
    />
  );
};

export default ProviderInvolvement;
