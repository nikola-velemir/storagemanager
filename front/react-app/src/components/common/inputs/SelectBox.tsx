import React, { useEffect, useState } from "react";
import { ProviderGetResponse } from "../../../model/provider/ProviderGetResponse";

interface SelectBoxProps<T> {
  items: T[];
  displayText: string;
  getItemId: (item: T) => string;
  getItemName: (item: T) => string;
  emitSelectionChange: (provider: T | null) => void;
}
const SelectBox = <T,>({
  items,
  displayText,
  getItemId,
  getItemName,
  emitSelectionChange,
}: SelectBoxProps<T>) => {
  const [selectedItem, setSelectedItem] = useState<T | null>(null);
  useEffect(() => {
    emitSelectionChange(selectedItem);
  }, [selectedItem]);
  const handleItemChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedId = e.target.value.trim();
    const foundItem = items.find((item) => getItemId(item) === selectedId);
    setSelectedItem(foundItem ? foundItem : null);
  };
  return (
    <form className="max-w-sm">
      <label
        htmlFor="providers"
        className="block mb-2 text-sm font-medium text-white dark:text-white"
      >
        {displayText}
      </label>
      <select
        onChange={handleItemChange}
        id="providers"
        className="h-10 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
      >
        <option value={"none"}>Choose</option>
        {items.map((item: T) => {
          return (
            <option key={getItemId(item)} value={getItemId(item)}>
              {getItemName(item)}
            </option>
          );
        })}
      </select>
    </form>
  );
};

export default SelectBox;
