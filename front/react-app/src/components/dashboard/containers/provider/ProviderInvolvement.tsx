import DashBoardCard from "../cards/DashBoardCard";
import { useProviderStats } from "./useProviderStats";
import ProviderInvolvementCarousel from "./ProviderInvolvementCarousel";

const ProviderInvolvement = () => {
  const { count, maxCount, invoiceInvolvements, componentInvolvements } =
    useProviderStats();
  return (
    <DashBoardCard
      maxValue={maxCount}
      title={"Registered providers"}
      value={count}
      chart={
        <ProviderInvolvementCarousel
          invoice={invoiceInvolvements}
          components={componentInvolvements}
        />
      }
    />
  );
};

export default ProviderInvolvement;
