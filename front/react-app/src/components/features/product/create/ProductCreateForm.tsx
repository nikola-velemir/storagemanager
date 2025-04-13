import { useEffect, useState } from "react";
import {
  validateField,
  validateIndentifier,
} from "./ProductCreateFormValidators";

export interface ProductCreateFormData {
  productName: string;
  productDescription: string;
  productIdentifier: string;
}

interface ProductCreateFormDataErrors {
  productNameError: string;
  productDescriptionError: string;
  productIdentifierError: string;
}

interface ProductCreateFormProps {
  emitProductFormData: (data: ProductCreateFormData) => void;
  emitCreate: () => void;
}

const ProductCreateForm = ({
  emitProductFormData,
  emitCreate,
}: ProductCreateFormProps) => {
  const [formData, setFormData] = useState<ProductCreateFormData>({
    productDescription: "",
    productName: "",
    productIdentifier: "",
  });
  const [errors, setErrors] = useState<ProductCreateFormDataErrors>({
    productDescriptionError: "",
    productNameError: "",
    productIdentifierError: "",
  });

  useEffect(() => {
    emitProductFormData(formData);
  }, [formData, emitProductFormData]);
  const handleOnChange = (
    e:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
    validateFields(name, value);
  };
  const validateFields = (name: string, value: string) => {
    if (name === "productName") {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          [name + "Error"]: "Name is required",
        });
      } else {
        setErrors({
          ...errors,
          [name + "Error"]: "",
        });
      }
    } else if (name === "productDescription") {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          [name + "Error"]: "Description is required",
        });
      } else {
        setErrors({
          ...errors,
          [name + "Error"]: "",
        });
      }
    } else {
      if (!validateIndentifier(value)) {
        setErrors({
          ...errors,
          [name + "Error"]: "Identifier must be atleast 6 characters long",
        });
      } else {
        setErrors({
          ...errors,
          [name + "Error"]: "",
        });
      }
    }
  };
  const handleCreateClick = () => {
    emitCreate();
  };
  return (
    <form className="w-full mx-auto">
      <div className="relative z-0 w-full mb-5 group">
        <input
          type="text"
          name="productName"
          id="productName"
          onChange={handleOnChange}
          className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
          placeholder=" "
          required
        />
        <label
          htmlFor="productName"
          className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
        >
          Name
        </label>
        <div className="h-4 text-red-600">{errors.productNameError}</div>
      </div>
      <div className="relative z-0 w-full mb-5 group">
        <input
          type="text"
          name="productIdentifier"
          id="productIdentifier"
          onChange={handleOnChange}
          className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
          placeholder=" "
          required
        />
        <label
          htmlFor="productIdentifier"
          className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
        >
          Identifier
        </label>
        <div className="h-4 text-red-600">{errors.productIdentifierError}</div>
      </div>
      <div className="relative z-0 w-full mb-5 group">
        <textarea
          rows={8}
          onChange={handleOnChange}
          name="productDescription"
          id="productDescription"
          className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
          placeholder=" "
          required
        />
        <label
          htmlFor="productDescription"
          className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
        >
          Description
        </label>
        <div className="h-4 text-red-600">{errors.productDescriptionError}</div>
      </div>
      <button
        type="button"
        onClick={handleCreateClick}
        className="w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
      >
        Create product
      </button>
    </form>
  );
};

export default ProductCreateForm;
