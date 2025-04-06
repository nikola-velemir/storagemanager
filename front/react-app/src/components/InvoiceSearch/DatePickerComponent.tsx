import { Datepicker } from "flowbite-react";
import React from "react";

interface DatePickerComponentProps {
  onDateChange: (date: Date | null) => void;
}

const DatePickerComponent = ({ onDateChange }: DatePickerComponentProps) => {
  const handleDateChange = (date: Date | null) => {
    onDateChange(date);
  };
  return (
    <div>
      <div className="text-sm font-medium mb-2">Date issued</div>
      <Datepicker
        className="h-10"
        title="Date issued"
        maxDate={new Date()}
        onChange={handleDateChange}
        autoHide={true}
      />
    </div>
  );
};

export default DatePickerComponent;
