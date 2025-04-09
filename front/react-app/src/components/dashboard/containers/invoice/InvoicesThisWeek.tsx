import DashBoardCard from "../cards/DashBoardCard";
import { useInvoiceStats } from "./useInvoiceStats";
import WeeklyInvoiceChart from "./WeeklyInvoiceChart";

const InvoicesThisWeek = () => {
  const { count, invoices } = useInvoiceStats();
  return (
    <DashBoardCard
      title={"Invoices this week"}
      value={count}
      chart={<WeeklyInvoiceChart data={invoices} />}
    />
  );
};

export default InvoicesThisWeek;
