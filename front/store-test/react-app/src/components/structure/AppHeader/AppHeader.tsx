import styles from "./AppHeader.module.css";
import "bootstrap-icons/font/bootstrap-icons.css";

const AppHeader = () => {
  return (
    <div className={`${styles.appHeader}`}>
      <h6>Storage manager</h6>
      <div className={`${styles.spacer}`}></div>
      <button
        className={`btn btn-secondary ${styles.windowControlButton}`}
        onClick={() => window.electron.minimize()}
      >
        <i className="bi bi-dash-lg"></i>
      </button>
      <button
        className={`btn btn-danger ${styles.windowControlButton}`}
        onClick={() => window.electron.close()}
      >
        <i className="bi bi-x-circle"></i>
      </button>
    </div>
  );
};

export default AppHeader;
