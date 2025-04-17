import DashBoardCard from "../cards/DashBoardCard";
import { useProviderStats } from "./useProviderStats";
import ProviderInvolvementCarousel from "./ProviderInvolvementCarousel";

const ProviderInvolvement = () => {
  const { count, maxCount, importInvolvements, componentInvolvements } =
    useProviderStats();
  return (
    <DashBoardCard
      maxValue={maxCount}
      title={"Registered providers"}
      value={count}
      chart={
        <ProviderInvolvementCarousel
          import={importInvolvements}
          components={componentInvolvements}
        />
      }
    />
  );
};

export default ProviderInvolvement;
