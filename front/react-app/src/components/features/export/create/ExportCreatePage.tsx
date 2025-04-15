import { FindExporterResponse } from "../../../../model/exporter/FindExporterResponse";
import ExportProductSearchSection from "./ExportProductSearchSection";
import ExporterSelectBox from "./ExporterSelectBox";

const ExportCreatePage = () => {
  const handleExporterChange = (item: FindExporterResponse | null) =>
    console.log(item);
  return (
    <div className="w-full flex flex-col items-center p-8">
      <ExporterSelectBox
        emitExporterChange={handleExporterChange}
      ></ExporterSelectBox>
      <div className="w-full">
        <ExportProductSearchSection />
      </div>
    </div>
  );
};

export default ExportCreatePage;
