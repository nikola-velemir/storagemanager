import DashBoardCard from "../cards/DashBoardCard";
import { useInvoiceStats } from "./useInvoiceStats";
import WeeklyInvoiceChart from "./WeeklyInvoiceChart";

const InvoicesThisWeek = () => {
  const { count, maxCount, invoices } = useInvoiceStats();
  return (
    <DashBoardCard
      maxValue={maxCount}
      title={"Invoices this week"}
      value={count}
      chart={<WeeklyInvoiceChart data={invoices} />}
    />
  );
};

export default InvoicesThisWeek;
