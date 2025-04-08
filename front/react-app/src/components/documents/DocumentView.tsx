import React, { useState } from "react";
import FullProgress from "../common/spinners/FullProgress";
import ExcelViewer from "./ExcelViewer";

interface DocumentViewProps {
  fileSrc: Blob | undefined;
  fileType: string | undefined;
  downloadProgress: number;
}

const DocumentView = ({
  fileSrc,
  fileType,
  downloadProgress,
}: DocumentViewProps) => {
  if (!fileSrc) return <FullProgress progress={downloadProgress} />;
  if (fileType?.includes("pdf")) {
    const objectUrl = URL.createObjectURL(fileSrc);
    return <iframe className="w-full h-full" src={objectUrl}></iframe>;
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
