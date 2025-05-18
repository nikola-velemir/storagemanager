import { useParams } from "react-router-dom";
import { ImportSearchComponentResponse } from "../../../../../model/invoice/import/ImportSearchComponentResponse";
import DocumentView from "../../../documents/DocumentView";
import ImportSearchComponentItem from "../search/cards/ImportSearchComponentItem";
import { useState, useEffect, useCallback } from "react";
import { DocumentService } from "../../../../../services/DocumentService";
import { MechanicalComponentService } from "../../../../../services/MechanicalComponentService";
import { InvoiceService } from "../../../../../services/invoice/InvoiceService";
import { ExportSearchProductResponse } from "../../../../../model/invoice/export/ExportSearchProductResponse";
import { ProductService } from "../../../../../services/products/ProductService";
import ExportSearchProductItem from "../../export/search/cards/ExportSearchProductItem";

const ImportInfo = () => {
  const { id } = useParams<{ id: string }>();
  const [progress, setProgress] = useState(0.0);
  const [components, setComponents] = useState<ImportSearchComponentResponse[]>(
    []
  );
  const [isProcessed, setIsProcessed] = useState<boolean>(false);
  const [products, setProducts] = useState<ExportSearchProductResponse[]>([]);
  const [documentSrc, setDocumentSrc] = useState<Blob | undefined>(undefined);
  const [fileType, setFileType] = useState("");
  const [invoiceType, setInvoiceType] = useState("");
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
    InvoiceService.findInvoiceType(id).then((r) => {
      setInvoiceType(r.data.type);
      setIsProcessed(r.data.isProcessed);
      if (r.data.isProcessed)
        if (r.data.type === "Import")
          MechanicalComponentService.findByInvoiceId(id).then((response) => {
            console.log(response.data.responses);
            setComponents(response.data.responses);
          });
        else
          ProductService.findByInvoiceId(id).then((res) =>
            setProducts(res.data.products)
          );
    });
  }, [id, downloadDoc]);

  const renderComponents = () => {
    return components.map((component: ImportSearchComponentResponse) => (
      <ImportSearchComponentItem
        key={component.identifier}
        id={component.id}
        identifier={component.identifier}
        name={component.name}
        price={component.price}
        quantity={component.quantity}
      />
    ));
  };
  const renderProducts = () => {
    return products.map((product: ExportSearchProductResponse) => (
      <ExportSearchProductItem
        key={product.id}
        id={product.id}
        identifier={product.identifier}
        name={product.name}
        price={product.price}
        quantity={product.quantity}
      />
    ));
  };
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
          {!isProcessed && (
            <div className="flex flex-row justify-center items-center content-center h-full text-lg font-medium">
              Invoice document has not yet been processed
            </div>
          )}
          {invoiceType === "Import" && renderComponents()}
          {invoiceType === "Export" && renderProducts()}
        </div>
      </div>
    </div>
  );
};

export default ImportInfo;
