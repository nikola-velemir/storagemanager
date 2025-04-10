import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { useLocation } from "react-router-dom";

const RouteStackContext = createContext<string[]>([]);

export const RouteStackProvider = ({ children }: { children: ReactNode }) => {
  const location = useLocation();
  const [stack, setStack] = useState<string[]>([]);

  useEffect(() => {
    setStack((prevStack) => {
      const current = location.pathname;
      if (prevStack[prevStack.length - 1] !== current) {
        return [...prevStack, current];
      }
      return prevStack;
    });
  }, [location]);

  return (
    <RouteStackContext.Provider value={stack}>
      {children}
    </RouteStackContext.Provider>
  );
};

export const useRouteStack = () => useContext(RouteStackContext);
