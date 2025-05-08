import { useEffect, useState } from "react";
import {
  InventoryValueByDay,
  ImportService,
} from "../../../../../services/invoice/ImportService";
import { animate } from "framer-motion";

export const useInventoryStats = () => {
  const [total, setTotal] = useState(0);
  const [currentValue, setCurrentValue] = useState(0);
  const [prices, setPrices] = useState<InventoryValueByDay[]>([]);
  useEffect(() => {
    ImportService.findTotalInventoryValue().then((res) => {
      var totalValue = parseFloat(res.data.total.toFixed(2));
      setTotal(totalValue);
      setPrices(res.data.values);
      const controls = animate(0, totalValue, {
        duration: 2,
        ease: "easeInOut",
        onUpdate: (latest) => {
          setCurrentValue(parseFloat(latest.toFixed(2)));
        },
      });
      return () => controls.stop();
    });
  }, []);
  return { total, currentValue, prices };
};
