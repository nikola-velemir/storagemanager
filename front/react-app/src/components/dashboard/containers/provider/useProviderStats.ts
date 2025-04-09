import { useEffect, useState } from "react";
import { ProviderService } from "../../../../services/ProviderService";
import { animate } from "framer-motion";
import { ProviderInvolvementResponse } from "../../../../model/provider/ProviderInvolvementResponse";

export const useProviderStats = () => {
  const [count, setCount] = useState(0);
  const [maxCount, setMaxCount] = useState(0);
  const [providers, setProviders] = useState<ProviderInvolvementResponse[]>([]);

  useEffect(() => {
    ProviderService.findProviderInvolvement().then((response) => {
      console.log(response.data);
      const finalValue = response.data.providers.length;
      setMaxCount(finalValue);
      setProviders(response.data.providers);
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
  return { count, maxCount, providers };
};
