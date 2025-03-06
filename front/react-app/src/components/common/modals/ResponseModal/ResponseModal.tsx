import { ReactNode } from "react";

interface ResponseModalProps {
  children: ReactNode;
}

const ResponseModal = ({ children }: ResponseModalProps) => {
  return (
    <div
      data-modal-placement="top-right"
      tabIndex={-1}
      aria-hidden="true"
      className="backdrop-blur-sm flex overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full"
    >
      <div className="relative h-1/2 rounded-xl p-6 w-full max-w-2xl max-h-full">
        <div className="relative h-full bg-stone-800 rounded-3xl ring-2 ring-gray-400 shadow-sm dark:bg-gray-700 flex justify-center">
          <div className="p-4 md:p-5 space-y-4 flex justify-center items-center">
            {children}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ResponseModal;
