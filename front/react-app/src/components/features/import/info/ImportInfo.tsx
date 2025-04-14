import { useParams } from "react-router-dom";
import { ImportSearchComponentResponse } from "../../../../model/invoice/import/ImportSearchComponentResponse";
import DocumentView from "../../documents/DocumentView";
import ImportSearchComponentItem from "../search/cards/ImportSearchComponentItem";
import { useState, useEffect, useCallback } from "react";
import { DocumentService } from "../../../../services/DocumentService";
import { MechanicalComponentService } from "../../../../services/MechanicalComponentService";

const ImportInfo = () => {
  const { id } = useParams<{ id: string }>();
  const [progress, setProgress] = useState(0.0);
  const [components, setComponents] = useState<
    ImportSearchComponentResponse[]
  >([]);
  const [documentSrc, setDocumentSrc] = useState<Blob | undefined>(undefined);
  const [fileType, setFileType] = useState("");
  const downloadDoc = useCallback(() => {
    setProgress(0);
    if (!id) return;
    DocumentService.downloadFile(id, (percentage) => {
      setProgress(percentage);
    }).then((doc) => {
      setDocumentSrc(doc);
      setFileType(doc.type);
    });
  }, [id]);

  useEffect(() => {
    if (!id || id.trim().length === 0) return;
    downloadDoc();
    MechanicalComponentService.findByInvoiceId(id).then((response) => {
      setComponents(response.data.responses);
    });
  }, [id, downloadDoc]);

  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 overflow-auto">
        <div className="px-8 pt-8 space-y-4 h-full">
          <DocumentView
            downloadProgress={progress}
            fileSrc={documentSrc}
            fileType={fileType}
          />
        </div>
        <div className="flex mx-8 flex-col h-5/6 overflow-y-scroll border-t-2 border-b-2 border-white items-center bg-gray-500">
          {components.map((component: ImportSearchComponentResponse) => {
            return (
              <ImportSearchComponentItem
                key={component.identifier}
                id={component.id}
                identifier={component.identifier}
                name={component.name}
                price={component.price}
                quantity={component.quantity}
              />
            );
          })}
        </div>
      </div>
    </div>
  );
};

export default ImportInfo;
