import React from "react";

interface DashBoardCardProps {
  title: string;
  value: number;
  maxValue: number;
  chart: React.ReactNode;
}

const DashBoardCard = ({
  title,
  value,
  maxValue,
  chart,
}: DashBoardCardProps) => {
  return (
    <div className="w-full bg-slate-700 transition hover:-translate-y-1 border-white border-2 flex flex-col rounded-xl">
      <div className="w-full flex flex-col items-center justify-center pt-12 pb-2">
        <div className="grid place-items-center text-4xl underline underline-offset-4">
          {value}
        </div>
        <span className="mt-2 text-lg font-medium">{title}</span>
      </div>
      <div className="w-full h-full flex items-center justify-center p-4 pt-2 text-white text-lg font-medium">
        {value !== 0 && maxValue !== 0 && value === maxValue && chart}
      </div>
    </div>
  );
};

export default DashBoardCard;
