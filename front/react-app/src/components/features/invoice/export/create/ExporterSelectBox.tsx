import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { FindExporterResponse } from "../../../../../model/exporter/FindExporterResponse";
import { ExporterService } from "../../../../../services/ExporterService";
import SelectBox from "../../../../common/inputs/SelectBox";

interface ExporterSelectBoxProps {
  emitExporterChange: (item: FindExporterResponse | null) => void;
}

const ExporterSelectBox = ({ emitExporterChange }: ExporterSelectBoxProps) => {
  const [exporters, setExporters] = useState<FindExporterResponse[]>([]);
  useEffect(() => {
    ExporterService.findAll()
      .then((res) => setExporters(res.data.responses))
      .catch(() => toast.error("Failed to fetch exporters"));
  }, []);
  const getExporterId = (item: FindExporterResponse) => item.id;
  const getExporterName = (item: FindExporterResponse) => item.name;
  const handleExporterEmit = (item: FindExporterResponse | null) => {
    emitExporterChange(item);
  };
  return (
    <SelectBox
      getItemId={getExporterId}
      getItemName={getExporterName}
      items={exporters}
      displayText="Exporter"
      emitSelectionChange={handleExporterEmit}
    ></SelectBox>
  );
};

export default ExporterSelectBox;
