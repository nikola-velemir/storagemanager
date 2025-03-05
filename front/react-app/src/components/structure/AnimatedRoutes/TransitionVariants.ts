import { Variants } from "framer-motion";

export const transitions: Variants = {
  initial: { opacity: 0, transition: { duration: 0.2, ease: "easeOut" } },
  animate: { opacity: 1, transition: { duration: 0.4, ease: "easeIn" } },
  exit: { opacity: 0, transition: { duration: 0.2, ease: "easeOut" } },
};
