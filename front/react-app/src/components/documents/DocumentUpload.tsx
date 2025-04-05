import { useEffect, useState } from "react";
import { DocumentService } from "../../services/DocumentService";
import { ProviderGetResponse } from "../../model/provider/ProviderGetResponse";

export enum UPLOADING_STATE {
  UPLOADING,
  UPLOADED,
  FAILED,
  NOT_UPLOADING,
}

interface DocumentUploadProps {
  selectedFile: File | undefined | null;
  uploadProgress: number;
  uploaded: UPLOADING_STATE;
  onFileChange: (file: File) => void;
  onUpload: () => void;
}

const DocumentUpload = ({
  selectedFile,
  uploadProgress,
  uploaded,
  onFileChange,
  onUpload,
}: DocumentUploadProps) => {
  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) onFileChange(file);
  };
  const renderUploadSection = () => {
    if (!selectedFile) {
      return (
        <button
          type="button"
          disabled
          className="py-2.5 text-lg px-5 w-full font-medium text-slate-200 focus:outline-none bg-gray-700 focus:z-10"
        >
          Upload
        </button>
      );
    }

    if (uploaded === UPLOADING_STATE.UPLOADED) {
      return (
        <button
          type="button"
          disabled
          className="py-2.5 text-lg px-5 w-full font-medium text-green-400 focus:outline-none bg-gray-700 focus:z-10"
        >
          Uploaded successfully
        </button>
      );
    }

    if (uploaded === UPLOADING_STATE.NOT_UPLOADING) {
      return (
        <button
          type="button"
          onClick={onUpload}
          className="w-full text-lg text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 font-medium px-5 py-2.5 text-center"
        >
          Upload
        </button>
      );
    }

    return (
      <button
        type="button"
        disabled
        className="py-2.5 text-lg px-5 w-full font-medium text-slate-200 focus:outline-none bg-gray-700 focus:z-10"
      >
        {`${Math.trunc(uploadProgress)}%`}
      </button>
    );
  };

  return (
    <div className="flex w-2/3 overflow-hidden flex-col items-center justify-center border-2 border-gray-300 rounded-xl">
      <label
        htmlFor="dropzone-file"
        className="flex flex-col items-center justify-center w-full h-64 cursor-pointer  hover:bg-gray-800 bg-gray-700 dark:border-gray-600 dark:hover:border-gray-500"
      >
        <div className="flex flex-col items-center justify-center pt-5 pb-6">
          {selectedFile && uploaded === UPLOADING_STATE.UPLOADED ? (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth="1.5"
              className="size-36 stroke-green-500"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M9 12.75 11.25 15 15 9.75M21 12c0 1.268-.63 2.39-1.593 3.068a3.745 3.745 0 0 1-1.043 3.296 3.745 3.745 0 0 1-3.296 1.043A3.745 3.745 0 0 1 12 21c-1.268 0-2.39-.63-3.068-1.593a3.746 3.746 0 0 1-3.296-1.043 3.745 3.745 0 0 1-1.043-3.296A3.745 3.745 0 0 1 3 12c0-1.268.63-2.39 1.593-3.068a3.745 3.745 0 0 1 1.043-3.296 3.746 3.746 0 0 1 3.296-1.043A3.746 3.746 0 0 1 12 3c1.268 0 2.39.63 3.068 1.593a3.746 3.746 0 0 1 3.296 1.043 3.746 3.746 0 0 1 1.043 3.296A3.745 3.745 0 0 1 21 12Z"
              />
            </svg>
          ) : selectedFile &&
            (uploaded === UPLOADING_STATE.NOT_UPLOADING ||
              uploaded === UPLOADING_STATE.UPLOADING) ? (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              className="w-8 h-8 mb-4 text-slate-200 dark:text-gray-400"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M10.125 2.25h-4.5c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125v-9M10.125 2.25h.375a9 9 0 0 1 9 9v.375M10.125 2.25A3.375 3.375 0 0 1 13.5 5.625v1.5c0 .621.504 1.125 1.125 1.125h1.5a3.375 3.375 0 0 1 3.375 3.375M9 15l2.25 2.25L15 12"
              />
            </svg>
          ) : (
            <svg
              className="w-8 h-8 mb-4 text-slate-200 dark:text-gray-400"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 20 16"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M13 13h3a3 3 0 0 0 0-6h-.025A5.56 5.56 0 0 0 16 6.5 5.5 5.5 0 0 0 5.207 5.021C5.137 5.017 5.071 5 5 5a4 4 0 0 0 0 8h2.167M10 15V6m0 0L8 8m2-2 2 2"
              />
            </svg>
          )}

          {selectedFile &&
          (uploaded === UPLOADING_STATE.NOT_UPLOADING ||
            uploaded === UPLOADING_STATE.UPLOADING) ? (
            <p className="mb-2 text-sm text-slate-200 dark:text-gray-400">
              <span className="font-semibold">{selectedFile.name}</span>
            </p>
          ) : !selectedFile && uploaded === UPLOADING_STATE.NOT_UPLOADING ? (
            <p className="mb-2 text-sm text-slate-200 dark:text-gray-400">
              <span className="font-semibold">Click to upload</span> or drag and
              drop
            </p>
          ) : (
            <p className="mb-2 text-sm text-slate-200 dark:text-gray-400"></p>
          )}
        </div>
        <input
          id="dropzone-file"
          type="file"
          disabled={
            uploaded === UPLOADING_STATE.UPLOADING ||
            uploaded === UPLOADING_STATE.UPLOADED
          }
          className="hidden"
          onChange={handleFileChange}
        />
      </label>
      {renderUploadSection()}
    </div>
  );
};

export default DocumentUpload;
