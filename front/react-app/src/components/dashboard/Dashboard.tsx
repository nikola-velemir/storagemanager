import ImportsThisWeek from "./containers/invoice/ImportsThisWeek";
import MechanicalComponentCount from "./containers/component/MechanicalComponentCount";
import ProviderInvolvement from "./containers/provider/ProviderInvolvement";
import InventoryValue from "./containers/inventory/InventoryValue";

const Dashboard = () => {
  return (
    <div className="w-full h-screen p-8">
      <div className="w-full h-5/6 overflow-y-scroll">
        <div className="w-full flex flex-row">
          <div className="flex w-1/2 p-4">
            <ImportsThisWeek />
          </div>
          <div className="flex w-1/2 p-4">
            <ProviderInvolvement />
          </div>
        </div>
        <div className="w-full flex flex-row">
          <div className="flex w-2/3 p-4">
            <MechanicalComponentCount />
          </div>
          <div className="flex w-1/3 p-4">
            <InventoryValue />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
