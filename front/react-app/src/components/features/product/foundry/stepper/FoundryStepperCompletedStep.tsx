const FoundryStepperCompletedStep = () => {
  return (
    <li className="flex w-full items-center text-blue-600 dark:text-blue-500 after:content-[''] after:w-full after:h-1 after:border-b after:border-blue-500 after:border-4 after:inline-block dark:after:border-blue-800">
      <span className="flex items-center justify-center w-10 h-10 bg-blue-500 rounded-full lg:h-12 lg:w-12 dark:bg-blue-800 shrink-0">
        <svg
          className="w-3.5 h-3.5 text-blue-100 lg:w-4 lg:h-4 dark:text-blue-300"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 16 12"
        >
          <path
            stroke="currentColor"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M1 5.917 5.724 10.5 15 1.5"
          />
        </svg>
      </span>
    </li>
  );
};

export default FoundryStepperCompletedStep;
