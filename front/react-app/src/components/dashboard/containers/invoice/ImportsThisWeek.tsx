import DashBoardCard from "../cards/DashBoardCard";
import { useImportStats } from "./useImportsStats";
import WeeklyImportChart from "./WeeklyImportChart";

const ImportsThisWeek = () => {
  const { count, maxCount, invoices } = useImportStats();
  return (
    <DashBoardCard
      maxValue={maxCount}
      title={"Imports this week"}
      value={count}
      chart={<WeeklyImportChart data={invoices} />}
    />
  );
};

export default ImportsThisWeek;
