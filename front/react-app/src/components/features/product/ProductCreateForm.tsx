import { useEffect, useState } from "react";

export interface ProductCreateFormData {
  productName: string;
  productDescription: string;
}

interface ProductCreateFormProps {
  emitProductFormData: (data: ProductCreateFormData) => void;
}

const ProductCreateForm = ({ emitProductFormData }: ProductCreateFormProps) => {
  const [credentials, setCredentials] = useState<ProductCreateFormData>({
    productDescription: "",
    productName: "",
  });
  useEffect(() => {
    emitProductFormData(credentials);
  }, [credentials, emitProductFormData]);
  const handleOnChange = (
    e:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setCredentials({
      ...credentials,
      [name]: value,
    });
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
      </div>
    </form>
  );
};

export default ProductCreateForm;
