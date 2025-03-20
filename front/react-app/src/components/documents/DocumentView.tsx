import React, { useState } from "react";
import FullSpinner from "../common/spinners/FullSpinner";
import ExcelViewer from "./ExcelViewer";

interface DocumentViewProps {
  fileSrc: Blob | undefined;
  fileType: string | undefined;
}

const DocumentView = ({ fileSrc, fileType }: DocumentViewProps) => {
  if (!fileSrc) return <FullSpinner />;
  if (fileType?.includes("pdf")) {
    const objectUrl = URL.createObjectURL(fileSrc);
    return <iframe className="w-full h-modal" src={objectUrl}></iframe>;
  } else if (
    fileType?.includes("msword") ||
    fileType?.includes("vnd.openxmlformats-officedocument")
  ) {
    return <ExcelViewer fileSrc={fileSrc} />;
  } else {
    return <p>Unsupport file type.</p>;
  }
};

export default DocumentView;
