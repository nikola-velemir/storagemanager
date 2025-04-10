import { useEffect, useState } from "react";
import { AreaChart, CartesianGrid, XAxis, YAxis, Area } from "recharts";
import { InventoryValueByDay } from "../../../../services/InvoiceService";

interface InventoryValueChartProps {
  data: InventoryValueByDay[];
}
const InventoryValueChart = ({ data }: InventoryValueChartProps) => {
  const [prices, setPrices] = useState<InventoryValueByDay[]>([]);
  useEffect(() => {
    const updatedData = data.map((item) => ({
      ...item,
      day: item.day.substring(0, 3),
    }));
    setPrices(updatedData);
  }, [data]);
  return (
    <AreaChart
      width={500}
      height={400}
      data={prices}
      margin={{
        top: 10,
        right: 30,
        left: 0,
        bottom: 0,
      }}
    >
      <CartesianGrid strokeDasharray="3 3" />
      <XAxis dataKey="day" stroke="#fff" />
      <YAxis stroke="#fff" />
      <Area
        type="monotone"
        dataKey="value"
        stroke="#fff"
        fill="oklch(62.3% 0.214 259.815)"
      />
    </AreaChart>
  );
};

export default InventoryValueChart;
