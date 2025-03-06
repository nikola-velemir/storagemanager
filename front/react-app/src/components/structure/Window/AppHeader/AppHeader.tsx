import WindowControllButton from "../WindowControllButtons/WindowControllButton";
import styles from "./AppHeader.module.css";

const AppHeader = () => {
  return (
    <div className={`${styles.appHeader}`}>
      <div className={`${styles.mainHeader}`}>
        <h6 className="font-bold tracking-wider text-lg">Storage manager</h6>
        <div className={`${styles.spacer}`}></div>
      </div>
      <div className={`${styles.controlHeader}`}>
        <WindowControllButton
          icon={
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="size-6"
            >
              <path d="M3.75 9h16.5m-16.5 6.75h16.5" />
            </svg>
          }
          transitionColor="hover:bg-stone-500"
          windowHandler={() => window.electron.maximize()}
        ></WindowControllButton>
        <WindowControllButton
          icon={
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="size-6"
            >
              <path d="M3 8.25V18a2.25 2.25 0 0 0 2.25 2.25h13.5A2.25 2.25 0 0 0 21 18V8.25m-18 0V6a2.25 2.25 0 0 1 2.25-2.25h13.5A2.25 2.25 0 0 1 21 6v2.25m-18 0h18M5.25 6h.008v.008H5.25V6ZM7.5 6h.008v.008H7.5V6Zm2.25 0h.008v.008H9.75V6Z" />
            </svg>
          }
          transitionColor="hover:bg-neutral-500"
          windowHandler={() => window.electron.maximize()}
        ></WindowControllButton>

        <WindowControllButton
          icon={
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="size-6 fill-white"
            >
              <path d="M6 18 18 6M6 6l12 12" />
            </svg>
          }
          transitionColor="hover:bg-red-800"
          windowHandler={() => window.electron.close()}
        />
      </div>
    </div>
  );
};

export default AppHeader;
