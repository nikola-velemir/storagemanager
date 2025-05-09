import { useState } from "react";
import DocumentUpload, {
  UPLOADING_STATE,
} from "../../../documents/DocumentUpload";
import SelectProvider from "./SelectProvider";
import { ProviderGetResponse } from "../../../../../model/provider/ProviderGetResponse";
import { DocumentService } from "../../../../../services/DocumentService";

const ImportUpload = () => {
  const [selectedFile, setSelectedFile] = useState<File | null | undefined>(
    null
  );
  const [uploaded, setUploaded] = useState(UPLOADING_STATE.NOT_UPLOADING);
  const [uploadProgress, setUploadProgress] = useState(0);
  const handleFileChange = (file: File) => {
    setSelectedFile(file);
    setUploaded(UPLOADING_STATE.NOT_UPLOADING);
  };
  const uploadFile = () => {
    if (!selectedFile || !providerId) {
      return;
    }
    setUploaded(UPLOADING_STATE.UPLOADING);
    DocumentService.uploadDocumentInChunks(
      providerId,
      selectedFile,
      (progress) => {
        setUploadProgress(progress);
        if (progress === 100) {
          setUploaded(UPLOADING_STATE.UPLOADED);
        }
      }
    )
      .then(() => {})
      .catch(() => {
        setUploadProgress(0.0);
      });
  };
  const [selectedProvider, setSelectedProvider] = useState<
    ProviderGetResponse | null | undefined
  >(null);
  const [providerId, setProviderId] = useState<string | null>(null);
  const [step, setStep] = useState(1);
  const handleProviderChange = (provider: ProviderGetResponse | null) => {
    setSelectedProvider(provider);
  };
  const handleTabChange = () => {};

  const changeStep = () => {
    setStep(2);
  };
  const validateProvider = () => {
    if (selectedProvider !== null && selectedProvider !== undefined) {
      setProviderId(selectedProvider.id);
      changeStep();
    }
  };
  return (
    <div className="flex items-center gap-8 justify-center flex-col p-8">
      <ol className="flex items-center w-full text-sm font-medium text-center text-gray-500 dark:text-gray-400 sm:text-base">
        <li className="flex md:w-full items-center text-blue-600 dark:text-blue-500 sm:after:content-[''] after:w-full after:h-1 after:border-b-8 after:border-gray-200 after:border-1 after:hidden sm:after:inline-block  dark:after:border-gray-700">
          <div
            className={`p-4 cursor-pointer rounded-xl border-white border bg-white`}
          >
            <span className="flex items-center pe-10 after:content-['/'] sm:after:hidden after:mx-0 after:text-gray-200 dark:after:text-gray-500 ">
              Provider
            </span>
          </div>
        </li>
        <li
          className={`flex md:w-full items-center after:content-[''] after:w-full after:h-1 after:border-gray-200 after:border-1 after:hidden sm:after:inline-block ${
            step === 2 ? "text-blue-600 after:border-b-8" : "after:border-b"
          } dark:after:border-gray-700`}
        >
          <div
            className={`p-4 cursor-pointer rounded-xl border-white border ${
              step === 2 ? "bg-white" : "bg-transparent"
            }`}
          >
            <span className="flex items-center pe-10 after:content-['/'] sm:after:hidden after:mx-0 after:text-gray-200 dark:after:text-gray-500">
              Document
            </span>
          </div>
        </li>
      </ol>
      <div className="w-full">
        <div className={`w-full ${step === 1 ? "" : "hidden"}`}>
          <div className={`h-64 flex justify-center items-center w-full`}>
            <SelectProvider emitProvider={handleProviderChange} />
          </div>
        </div>
      </div>
      <div
        className={`${
          step === 2 ? "" : "hidden"
        } h-64 w-full flex justify-center`}
      >
        <DocumentUpload
          onFileChange={handleFileChange}
          onUpload={uploadFile}
          selectedFile={selectedFile}
          uploadProgress={uploadProgress}
          uploaded={uploaded}
        ></DocumentUpload>
      </div>
      {uploaded !== UPLOADING_STATE.UPLOADED && (
        <div className={`w-full flex justify-end`}>
          {step === 1 ? (
            <button
              onClick={validateProvider}
              type="button"
              className="text-white font-medium text-base bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 rounded-lg px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
            >
              Next
            </button>
          ) : (
            <button
              onClick={uploadFile}
              type="button"
              className="text-white font-medium text-base bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 rounded-lg px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
            >
              Finish
            </button>
          )}
        </div>
      )}
    </div>
  );
};

export default ImportUpload;
