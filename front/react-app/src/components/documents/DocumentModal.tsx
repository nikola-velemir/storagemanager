import { useEffect, useState } from "react";
import { DocumentService } from "../../services/DocumentService";
import DocumentView from "./DocumentView";

interface DocumentModalProps {
  invoiceId: string;
  isOpen: boolean;
  toggleOpen: () => void;
}

const DocumentModal = ({
  invoiceId,
  isOpen,
  toggleOpen,
}: DocumentModalProps) => {
  const [progress, setProgress] = useState(0.0);
  const [isDownloading, setIsDownloading] = useState(true);
  const [documentSrc, setDocumentSrc] = useState<Blob | undefined>(undefined);
  const [fileType, setFileType] = useState("");
  const downloadDoc = () => {
    setIsDownloading(true);
    setProgress(0);
    DocumentService.downloadFile(invoiceId, (percentage) => {
      setProgress(percentage);
    })
      .then((doc) => {
        setDocumentSrc(doc);
        setFileType(doc.type);
        setIsDownloading(false);
      })
      .catch(() => {
        setIsDownloading(false);
      });
  };
  useEffect(() => {
    if (isOpen) {
      setDocumentSrc(undefined);
      setFileType("");
      downloadDoc();
    }
  }, [isOpen]);
  return (
    <div
      id="default-modal"
      tabIndex={-1}
      aria-hidden="true"
      className={`${
        isOpen ? "" : "hidden"
      } backdrop-blur-lg flex overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full`}
    >
      <div className="relative p-12 w-full h-full">
        <div className="relative bg-slate-800 overflow-hidden text-white shadow-sm dark:bg-gray-700 h-full ring-2 ring-white rounded-xl">
          <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
            <h3 className="text-xl font-semibold text-white dark:text-white">
              Faktura
            </h3>
            <button
              type="button"
              className="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
              data-modal-hide="default-modal"
              onClick={toggleOpen}
            >
              <svg
                className="w-3 h-3"
                aria-hidden="true"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 14 14"
              >
                <path
                  stroke="currentColor"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
                />
              </svg>
              <span className="sr-only">Close modal</span>
            </button>
          </div>
          <div className="p-4 md:p-5 space-y-4 h-full overflow-hidden">
            <DocumentView
              downloadProgress={progress}
              fileSrc={documentSrc}
              fileType={fileType}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default DocumentModal;
