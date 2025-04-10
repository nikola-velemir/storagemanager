import {
  PieChart,
  Pie,
  Tooltip,
  TooltipProps,
  ResponsiveContainer,
} from "recharts";
import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ProviderInvoiceInvolvementResponse } from "../../../../model/provider/ProviderInvoiceInvolvementResponse";
import { useNavigate } from "react-router-dom";
import { ProviderComponentInvolvementResponse } from "../../../../model/provider/ProviderComponentInvolvementResponse";

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
  data:
    | ProviderInvoiceInvolvementResponse[]
    | ProviderComponentInvolvementResponse[];
  dataKey: string;
}
const generateRandomColor = () => {
  const blue = Math.floor(Math.random() * 76) + 180;

  const red = Math.floor(Math.random() * 70);
  const green = Math.floor(Math.random() * 50) + 150;

  return `rgb(${red}, ${green}, ${blue})`;
};
const ProviderInvolvementPieChart = ({
  data,
  dataKey,
}: ProviderInvolvementPieChartProps) => {
  const navigate = useNavigate();
  const handlePieSliceClick = (data: any) => {
    navigate("/provider-profile/" + data.id);
  };
  return (
    <ResponsiveContainer height="100%" width="100%">
      <PieChart>
        <Pie
          dataKey={dataKey}
          data={data.map((entry) => ({
            ...entry,
            fill: generateRandomColor(),
          }))}
          cx="50%"
          cy="50%"
          innerRadius="30%"
          outerRadius="80%"
          fill="#82ca9d"
          stroke="oklch(44.6% 0.043 257.281)"
          onClick={handlePieSliceClick}
          label={({ name }) => name}
        />
        <Tooltip content={<CustomTooltip />} />
      </PieChart>
    </ResponsiveContainer>
  );
};

export default ProviderInvolvementPieChart;
