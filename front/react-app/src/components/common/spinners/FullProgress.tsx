interface FullProgressProps {
  progress: number;
}

const FullProgress = ({ progress }: FullProgressProps) => {
  return (
    <div className="w-full h-full flex-col flex justify-center items-center">
      <div className="flex justify-between mb-1">
        <span className="text-xl font-medium text-blue-400 dark:text-white">
          {`${progress}%`}
        </span>
      </div>
      <div className="w-full bg-gray-200 rounded-full h-4 dark:bg-gray-700">
        <div
          className="bg-blue-400 transition-opacity ease-in-out transition-discrete h-4 rounded-full "
          style={{ width: `${progress}%` }}
        ></div>
      </div>
    </div>
  );
};

export default FullProgress;
