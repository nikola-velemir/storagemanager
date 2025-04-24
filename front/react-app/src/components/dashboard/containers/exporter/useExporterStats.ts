import { useEffect, useState } from "react";
import { ExporterExportInvolvementResponse } from "../../../../model/exporter/ExporterExportInvolvementResponse";
import { ExporterProductInvolvementResponse } from "../../../../model/exporter/ExporterProductInvolvementResponse";
import { ExporterService } from "../../../../services/businessPartner/ExporterService";
import { animate } from "framer-motion";

export const useExporterStats = () => {
  const [count, setCount] = useState(0);
  const [maxCount, setMaxCount] = useState(0);
  const [exportInvolvements, setExportInvolvements] = useState<
    ExporterExportInvolvementResponse[]
  >([]);
  const [productInvolvements, setProductInvolvements] = useState<
    ExporterProductInvolvementResponse[]
  >([]);
  useEffect(() => {
    ExporterService.findExporterInvoiceInvolvement().then((res) => {
      setExportInvolvements(res.data.exporters);
      const finalValue = res.data.exporters.length;
      setMaxCount(finalValue);
      ExporterService.findExporterProductInvolvement().then((res) => {
        setProductInvolvements(res.data.products);
      });
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

  return { count, maxCount, exportInvolvements, productInvolvements };
};
