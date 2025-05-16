import TopFiveComponentsBarChart from "./TopFiveComponentsBarChart";
import DashBoardCard from "../cards/DashBoardCard";
import { useComponentStats } from "./useComponentStats";

const MechanicalComponentCount = () => {
  const { count, maxCount, topFive } = useComponentStats();
  return (
    <>
      <DashBoardCard
        maxValue={maxCount}
        title="Components"
        chart={<TopFiveComponentsBarChart data={topFive} />}
        value={count}
      />
    </>
  );
};

export default MechanicalComponentCount;
