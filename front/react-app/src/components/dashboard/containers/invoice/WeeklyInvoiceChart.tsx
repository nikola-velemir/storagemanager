import {
  CartesianGrid,
  Line,
  LineChart,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { ImportFindCountForDayResponse } from "../../../../model/invoice/import/ImportFindCountForDayResponse";
import { useEffect, useState } from "react";

interface WeeklyInvoiceChartProps {
  data: ImportFindCountForDayResponse[];
}

const WeeklyInvoiceChart = ({ data }: WeeklyInvoiceChartProps) => {
  const [invoices, setInvoices] = useState<ImportFindCountForDayResponse[]>(
    []
  );
  useEffect(() => {
    const updatedData = data.map((item) => ({
      ...item,
      dayOfTheWeek: item.dayOfTheWeek.substring(0, 3),
    }));
    setInvoices(updatedData);
  }, [data]);
  const getMax = () => {
    const max = Math.max(...invoices.map((p) => p.count));
    const maxDigits = max.toString().length;
    const roundedMax = Math.pow(10, maxDigits - 1);
    return Math.ceil(max / roundedMax) * roundedMax + roundedMax;
  };
  return (
    <LineChart width={800} height={400} data={invoices}>
      <CartesianGrid stroke="#fff" />
      <XAxis
        dataKey={"dayOfTheWeek"}
        stroke="#fff"
        letterSpacing={0.5}
        fontSize={16}
        tickMargin={10}
      />
      <YAxis
        domain={[0, getMax()]}
        letterSpacing={0.5}
        stroke="#fff"
        fontSize={16}
        tickMargin={10}
      />
      <Tooltip />
      <Line
        type="monotone"
        dataKey="count"
        strokeWidth={3}
        stroke="oklch(62.3% 0.214 259.815)"
      />
    </LineChart>
  );
};

export default WeeklyInvoiceChart;
