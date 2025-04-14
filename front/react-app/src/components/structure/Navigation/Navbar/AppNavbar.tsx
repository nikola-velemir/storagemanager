import { useNavigate } from "react-router-dom";
import { useRouteStack } from "../../../../infrastructure/Routes/RouteStackContext";

interface AppNavbarProps {
  toggleDrawer: () => void;
}

const AppNavbar = ({ toggleDrawer }: AppNavbarProps) => {
  const navigate = useNavigate();
  const routeStack = useRouteStack();
  const isPreviousRouteLogin = () => {
    return (
      routeStack.length < 2 || routeStack[routeStack.length - 2] === "/login"
    );
  };
  const handleGoBack = () => {
    if (isPreviousRouteLogin()) {
      return;
    }
    routeStack.pop();
    navigate(-1);
  };
  return (
    <nav className="bg-neutral-800 border-gray-200 dark:bg-gray-900 h-16">
      <div className="w-screen flex-row-reverse flex flex-wrap items-center justify-between mx-0 p-3">
        <button
          type="button"
          className="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg hover:text-neutral-800 hover:bg-white focus:outline-none dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
          data-drawer-target="drawer-navigation"
          data-drawer-show="drawer-navigation"
          aria-controls="drawer-navigation"
          onClick={toggleDrawer}
        >
          <span className="sr-only">Open main menu</span>
          <svg
            className="w-5 h-5"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 17 14"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M1 1h15M1 7h15M1 13h15"
            />
          </svg>
        </button>{" "}
        {!isPreviousRouteLogin() && (
          <button
            type="button"
            className="inline-flex items-center p-1 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg hover:text-neutral-800 hover:bg-white focus:outline-none dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
            data-drawer-target="drawer-navigation"
            data-drawer-show="drawer-navigation"
            aria-controls="drawer-navigation"
            onClick={handleGoBack}
          >
            <span className="sr-only">Open main menu</span>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth="1.5"
              stroke="currentColor"
              className="w-10 h-10"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="m11.25 9-3 3m0 0 3 3m-3-3h7.5M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
              />
            </svg>
          </button>
        )}
      </div>
    </nav>
  );
};
export default AppNavbar;
