import { useEffect, useState } from "react";
import { InvoiceFindCountForDayResponse } from "../../../../model/invoice/InvoiceFindCountForDayResponse";
import { animate, AnimationPlaybackControls } from "framer-motion";
import { InvoiceService } from "../../../../services/InvoiceService";

export const useInvoiceStats = () => {
  const [count, setCount] = useState(0);
  const [maxCount, setMaxCount] = useState(0);
  const [invoices, setInvoices] = useState<InvoiceFindCountForDayResponse[]>(
    []
  );
  useEffect(() => {
    InvoiceService.findCountsThisWeek().then((response) => {
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
