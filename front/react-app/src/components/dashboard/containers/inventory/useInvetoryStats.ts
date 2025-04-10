import { useEffect, useState } from "react";
import {
  InventoryValueByDay,
  InvoiceService,
} from "../../../../services/InvoiceService";
import { animate } from "framer-motion";

export const useInventoryStats = () => {
  const [total, setTotal] = useState(0);
  const [prices, setPrices] = useState<InventoryValueByDay[]>([]);
  useEffect(() => {
    InvoiceService.findTotalInventoryValue().then((res) => {
      var totalValue = parseFloat(res.data.total.toFixed(2));
      setTotal(totalValue);
      setPrices(res.data.values);
      const controls = animate(0, totalValue, {
        duration: 2,
        ease: "easeInOut",
        onUpdate: (latest) => {
          setTotal(parseFloat(latest.toFixed(2)));
        },
      });
      return () => controls.stop();
    });
  }, []);
  return { total, prices };
};
