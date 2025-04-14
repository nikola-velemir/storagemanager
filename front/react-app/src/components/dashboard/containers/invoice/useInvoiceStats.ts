import { useEffect, useState } from "react";
import { ImportFindCountForDayResponse } from "../../../../model/invoice/import/ImportFindCountForDayResponse";
import { animate } from "framer-motion";
import { ImportService } from "../../../../services/ImportService";

export const useInvoiceStats = () => {
  const [count, setCount] = useState(0);
  const [maxCount, setMaxCount] = useState(0);
  const [invoices, setInvoices] = useState<ImportFindCountForDayResponse[]>(
    []
  );
  useEffect(() => {
    ImportService.findCountsThisWeek().then((response) => {
      const finalValue = response.data.counts.reduce(
        (sum, item) => sum + item.count,
        0
      );
      setMaxCount(finalValue);
      setInvoices(response.data.counts);
      const controls = animate(0, finalValue, {
        duration: 2,
        ease: "easeInOut",
        onUpdate: (latest) => {
          setCount(Math.floor(latest));
        },
      });
      return () => controls.stop();
    });
  }, []);
  return { count, maxCount, invoices };
};
