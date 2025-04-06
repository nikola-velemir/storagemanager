import { useEffect } from "react";
import { MechanicalComponentService } from "../../../services/MechanicalComponentService";

const ComponentsSearch = () => {
  useEffect(() => {
    MechanicalComponentService.findFiltered({
      pageNumber: 1,
      pageSize: 5,
    }).then((response) => {
      console.log(response.data);
    });
  });
  return (
    <div className="h-screen w-full p-8">
      <div className="h-5/6 overflow-y-auto flex items-center flex-col"></div>
    </div>
  );
};

export default ComponentsSearch;
