import { easeIn } from "framer-motion";
import { PieChart, Pie, Tooltip, TooltipProps } from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ProviderInvolvementResponse } from "../../../../model/provider/ProviderInvolvementResponse";
import { useNavigate } from "react-router-dom";

const CustomTooltip = ({
  active,
  payload,
  coordinate,
}: TooltipProps<ValueType, NameType>) => {
  const getLeft = () => {
    if (!coordinate || !coordinate.x) {
      return 0;
    }
    return coordinate.x;
  };
  const getRight = () => {
    if (!coordinate || !coordinate.y) {
      return 0;
    }
    return coordinate.y;
  };
  if (active && payload && payload.length) {
    return (
      <div
        className="custom-tooltip bg-gray-300 text-slate-800 p-2 text-sm font-normal transition absolute w-fit"
        style={{
          left: getLeft() + 5,
          top: getRight() + 5,
        }}
      >
        <p className="label break-keep">{`${payload[0].name} : ${payload[0].value}`}</p>
      </div>
    );
  }

  return null;
};
interface ProviderInvolvementPieChartProps {
  data: ProviderInvolvementResponse[];
}
const generateRandomColor = () => {
  const blue = Math.floor(Math.random() * 76) + 180;

  const red = Math.floor(Math.random() * 70);
  const green = Math.floor(Math.random() * 200);

  return `rgb(${red}, ${green}, ${blue})`;
};
const ProviderInvolvementPieChart = ({
  data,
}: ProviderInvolvementPieChartProps) => {
  const navigate = useNavigate();
  const handlePieSliceClick = (data: any) => {
    navigate("/provider-profile/" + data.id);
  };
  return (
    <PieChart height={400} width={800}>
      <Pie
        dataKey="invoiceCount"
        data={data.map((entry) => ({
          ...entry,
          fill: generateRandomColor(),
        }))}
        cx="50%"
        cy="50%"
        innerRadius={80}
        outerRadius={200}
        fill="#82ca9d"
        onClick={handlePieSliceClick}
        label={({ name }) => name}
      />
      <Tooltip content={<CustomTooltip />} />
    </PieChart>
  );
};

export default ProviderInvolvementPieChart;
