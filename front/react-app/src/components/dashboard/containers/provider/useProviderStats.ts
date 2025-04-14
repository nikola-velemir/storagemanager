import { useEffect, useState } from "react";
import { ProviderService } from "../../../../services/ProviderService";
import { animate } from "framer-motion";
import { ProviderImportInvolvementResponse } from "../../../../model/provider/ProviderImportInvolvementResponse";
import { ProviderComponentInvolvementResponse } from "../../../../model/provider/ProviderComponentInvolvementResponse";

export const useProviderStats = () => {
  const [count, setCount] = useState(0);
  const [maxCount, setMaxCount] = useState(0);
  const [importInvolvements, setInvoiceInvolvements] = useState<
    ProviderImportInvolvementResponse[]
  >([]);
  const [componentInvolvements, setComponentInvolvements] = useState<
    ProviderComponentInvolvementResponse[]
  >([]);

  useEffect(() => {
    ProviderService.findProviderInvoiceInvolvement().then((response) => {
      const finalValue = response.data.providers.length;
      setMaxCount(finalValue);
      setInvoiceInvolvements(response.data.providers);
      ProviderService.findProviderComponentInvolvement().then((response) => {
        setComponentInvolvements(response.data.components);
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
  return {
    count,
    maxCount,
    importInvolvements,
    componentInvolvements,
  };
};
