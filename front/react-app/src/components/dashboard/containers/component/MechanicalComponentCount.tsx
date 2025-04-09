import TopFiveComponentsBarChart from "./TopFiveComponentsBarChart";
import DashBoardCard from "../cards/DashBoardCard";
import { useComponentStats } from "./useComponentStats";

const MechanicalComponentCount = () => {
  const { count, topFive } = useComponentStats();
  return (
    <>
      <DashBoardCard
        title="Components"
        chart={<TopFiveComponentsBarChart data={topFive} />}
        value={count}
      />
    </>
  );
};

export default MechanicalComponentCount;
