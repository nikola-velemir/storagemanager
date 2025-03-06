interface AppNavbarProps {
  toggleDrawer: () => void;
}

const AppNavbar = ({ toggleDrawer }: AppNavbarProps) => {
  return (
    <nav className="bg-neutral-800 border-gray-200 dark:bg-gray-900 h-16">
      <div className="w-screen flex flex-wrap items-center justify-between mx-0 p-3">
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
        </button>
      </div>
    </nav>
  );
};
export default AppNavbar;
