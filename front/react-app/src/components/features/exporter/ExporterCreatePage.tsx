import React, { ChangeEvent, useEffect, useState } from "react";
import { validateField } from "./ExporterCreateValidators";
import { toast } from "react-toastify";
import { ExporterService } from "../../../services/ExporterService";

interface ExportCreateFormData {
  exporterName: string;
  exporterAddress: string;
  exporterPhoneNumber: string;
}
interface ExportCreateFormDataErrors {
  exporterNameError: string;
  exporterAddressError: string;
  exporterPhoneNumberError: string;
}

const ExporterCreatePage = () => {
  const [formData, setFormData] = useState<ExportCreateFormData>({
    exporterAddress: "",
    exporterName: "",
    exporterPhoneNumber: "",
  });
  const [errors, setErrors] = useState<ExportCreateFormDataErrors>({
    exporterAddressError: "",
    exporterNameError: "",
    exporterPhoneNumberError: "",
  });
  useEffect(() => {
    console.log(formData);
  }, [formData]);
  const handleCreateClick = () => {
    if (!validateAllFields()) {
      toast.error("Some fields are invalid!");
      return;
    }
    ExporterService.create({
      address: formData.exporterAddress,
      name: formData.exporterName,
      phoneNumber: formData.exporterPhoneNumber,
    })
      .then(() => toast.success("Exporter created!"))
      .catch(() => toast.error("Something went wrong"));
  };
  const handleOnChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
    validate(name, value);
  };
  const validateAllFields = () => {
    return (
      validateField(formData.exporterName) &&
      validateField(formData.exporterAddress) &&
      validateField(formData.exporterPhoneNumber)
    );
  };
  const validate = (name: string, value: string) => {
    let valid = true;
    if (name === "exporterName") {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          exporterNameError: "Name is required",
        });
        valid = false;
      } else
        setErrors({
          ...errors,
          exporterNameError: "",
        });
    } else if (name === "exporterPhoneNumber") {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          exporterPhoneNumberError: "Phone number is required",
        });
        valid = false;
      } else
        setErrors({
          ...errors,
          exporterPhoneNumberError: "",
        });
    } else {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          exporterAddressError: "Address is required",
        });
        valid = false;
      } else
        setErrors({
          ...errors,
          exporterAddressError: "",
        });
    }
    return valid;
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 py-8 overflow-auto">
        <form className="max-w-lg mx-auto">
          <div className="relative z-0 w-full mb-5 group">
            <input
              type="text"
              name="exporterName"
              id="exporterName"
              onChange={handleOnChange}
              className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
              placeholder=" "
              required
            />
            <label
              htmlFor="exporterName"
              className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Name
            </label>
            <div className="h-4 text-red-600">{errors.exporterNameError}</div>
          </div>
          <div className="relative z-0 w-full mb-5 group">
            <input
              type="text"
              name="exporterPhoneNumber"
              id="exporterPhoneNumber"
              onChange={handleOnChange}
              className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
              placeholder=" "
              required
            />
            <label
              htmlFor="exporterPhoneNumber"
              className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Phone number
            </label>
            <div className="h-4 text-red-600">
              {errors.exporterPhoneNumberError}
            </div>
          </div>
          <div className="relative z-0 w-full mb-5 group">
            <input
              type="text"
              name="exporterAddress"
              id="exporterAddress"
              onChange={handleOnChange}
              className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
              placeholder=" "
              required
            />
            <label
              htmlFor="exporterAddress"
              className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Address
            </label>
            <div className="h-4 text-red-600">
              {errors.exporterAddressError}
            </div>
          </div>
          <button
            type="button"
            onClick={handleCreateClick}
            className="w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
          >
            Create product
          </button>
        </form>
      </div>
    </div>
  );
};

export default ExporterCreatePage;
