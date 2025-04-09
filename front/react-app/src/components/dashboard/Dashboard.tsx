import InvoicesThisWeek from "./containers/invoice/InvoicesThisWeek";
import MechanicalComponentCount from "./containers/component/MechanicalComponentCount";
import ProviderInvolvement from "./containers/provider/ProviderInvolvement";

const Dashboard = () => {
  return (
    <div className="w-full h-screen p-8">
      <div className="w-full h-5/6 overflow-y-scroll">
        <div className="w-full flex flex-row">
          <div className="flex w-1/2 p-4">
            <InvoicesThisWeek />
          </div>
          <div className="flex w-1/2 p-4">
            <ProviderInvolvement />
          </div>
        </div>
        <div className="w-full flex flex-row">
          <div className="flex w-2/3 p-4">
            <MechanicalComponentCount />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
