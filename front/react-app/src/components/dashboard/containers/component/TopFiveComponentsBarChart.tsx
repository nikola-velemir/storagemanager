import { useEffect, useState } from "react";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Rectangle,
  XAxis,
  YAxis,
} from "recharts";
import { MechanicalComponentTopFiveQuantityResponse } from "../../../../model/components/MechanicalComponentTopFiveQuantityResponse";
import { useNavigate } from "react-router-dom";

interface TopFiveComponentsBarChartProps {
  data: MechanicalComponentTopFiveQuantityResponse[];
}

const TopFiveComponentsBarChart = ({
  data,
}: TopFiveComponentsBarChartProps) => {
  const navigate = useNavigate();
  const [components, setComponents] = useState<
    MechanicalComponentTopFiveQuantityResponse[]
  >([]);
  const handleBarClick = (e: any) => {
    navigate("/component-info/" + e.id);
  };
  useEffect(() => {
    setComponents(data);
  }, [data]);
  const getMax = () => {
    const max = Math.max(...components.map((p) => p.quantity));
    const maxDigits = max.toString().length;
    const roundedMax = Math.pow(10, maxDigits - 1);
    return Math.ceil(max / roundedMax) * roundedMax + roundedMax;
  };
  return (
    <BarChart
      height={400}
      width={800}
      data={components}
      margin={{
        top: 5,
        right: 30,
        left: 20,
        bottom: 5,
      }}
    >
      <CartesianGrid strokeDasharray="3 3" stroke="#fff" />
      <XAxis
        dataKey="name"
        stroke="#fff"
        letterSpacing={0.5}
        fontSize={10}
        tickMargin={10}
      />
      <YAxis
        stroke="#fff"
        fontSize={16}
        letterSpacing={0.5}
        tickMargin={10}
        domain={[0, getMax()]}
      />
      <Bar
        dataKey="quantity"
        fill="oklch(62.3% 0.214 259.815)"
        onClick={handleBarClick}
        activeBar={<Rectangle fill="pink" stroke="blue" />}
      />
    </BarChart>
  );
};

export default TopFiveComponentsBarChart;
