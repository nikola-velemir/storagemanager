import { useEffect, useState } from "react";
import { MechanicalComponentTopFiveQuantityResponse } from "../../../../model/components/MechanicalComponentTopFiveQuantityResponse";
import { MechanicalComponentService } from "../../../../services/MechanicalComponentService";
import { animate, AnimationPlaybackControls } from "framer-motion";

export const useComponentStats = () => {
  const [maxCount, setMaxCount] = useState(0);
  const [count, setCount] = useState(0);
  const [topFive, setTopFive] = useState<
    MechanicalComponentTopFiveQuantityResponse[]
  >([]);

  useEffect(() => {
    let controls: AnimationPlaybackControls;
    MechanicalComponentService.findComponentCount().then((res) => {
      const finalValue = res.data.count;
      setMaxCount(finalValue);
      controls = animate(0, finalValue, {
        duration: 2,
        ease: "easeInOut",
        onUpdate: (latest) => {
          setCount(Math.floor(latest));
        },
      });
      return () => controls.stop();
    });

    MechanicalComponentService.findTopFiveInQuantity().then((res) => {
      setTopFive(res.data.components);
    });
  }, []);
  return { count, maxCount, topFive };
};
