import React from "react";
import DashBoardCard from "../cards/DashBoardCard";
import { useExporterStats } from "./useExporterStats";
import ExporterInvolvementCarousel from "./ExporterInvolvementCarousel";

const ExporterInvolvement = () => {
  const { count, maxCount, exportInvolvements, productInvolvements } =
    useExporterStats();
  return (
    <DashBoardCard
      maxValue={maxCount}
      title={"Registered exporters"}
      value={count}
      chart={
        <ExporterInvolvementCarousel
          export={exportInvolvements}
          components={productInvolvements}
        />
      }
    />
  );
};

export default ExporterInvolvement;
