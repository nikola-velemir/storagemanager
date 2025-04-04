import React, {
  forwardRef,
  useEffect,
  useImperativeHandle,
  useState,
} from "react";
import {
  validatePhoneField,
  validateTextField,
} from "./CreateProviderFormValidators";

interface ProviderFormData {
  name: string;
  address: string;
  phone: string;
}

interface ProviderFormDataErrors {
  nameError: string;
  addressError: string;
  phoneError: string;
}

export interface CreateProviderFormRef {
  triggerValidate: () => boolean;
  triggerLock: () => void;
}

interface CreateProviderFormProps {
  handleProviderChange: (provider: ProviderFormData) => void;
}

const CreateProviderForm = forwardRef<
  CreateProviderFormRef,
  CreateProviderFormProps
>(({ handleProviderChange }, ref) => {
  const [lock, setLock] = useState(false);
  const [formData, setFormData] = useState<ProviderFormData>({
    address: "",
    name: "",
    phone: "",
  });
  const [errors, setErrors] = useState<ProviderFormDataErrors>({
    addressError: "",
    nameError: "",
    phoneError: "",
  });

  useEffect(() => {
    handleProviderChange(formData);
  }, [formData]);
  const triggerLock = () => {
    setLock(true);
  };
  const triggerValidate = (): boolean => {
    const newErrors: ProviderFormDataErrors = {
      nameError: validateTextField(formData.name) ? "" : "Name is required",
      addressError: validateTextField(formData.address)
        ? ""
        : "Address is required",
      phoneError: validatePhoneField(formData.phone)
        ? ""
        : "Phone is missing or is in wrong format",
    };

    setErrors(newErrors);

    const isValid =
      !newErrors.nameError && !newErrors.addressError && !newErrors.phoneError;

    return isValid;
  };

  useImperativeHandle(ref, () => ({
    triggerValidate,
    triggerLock,
  }));

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));

    if (name === "address") {
      setErrors((prev) => ({
        ...prev,
        addressError: validateTextField(value) ? "" : "Address is required",
      }));
    } else if (name === "name") {
      setErrors((prev) => ({
        ...prev,
        nameError: validateTextField(value) ? "" : "Name is required",
      }));
    } else if (name === "phone") {
      setErrors((prev) => ({
        ...prev,
        phoneError: validatePhoneField(value)
          ? ""
          : "Phone is missing or is in wrong format",
      }));
    }
  };

  return (
    <form className="max-w-lg p-8 mx-auto">
      <div className="text-lg text-white mb-4 font-medium">Create provider</div>

      <div className="relative z-0 w-full mb-5 group">
        <input
          type="text"
          name="name"
          disabled={lock}
          id="providerName"
          onChange={handleChange}
          className="block py-2.5 px-0 w-full text-base text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          required
        />
        <label htmlFor="providerName" className="form-label">
          Name
        </label>
        <div className="h-6">
          {errors.nameError && (
            <p className="text-red-500">{errors.nameError}</p>
          )}
        </div>
      </div>

      <div className="relative z-0 w-full mb-5 group">
        <input
          type="text"
          name="address"
          id="providerAddress"
          onChange={handleChange}
          disabled={lock}
          className="block py-2.5 px-0 w-full text-base text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          required
        />
        <label htmlFor="providerAddress" className="form-label">
          Address
        </label>
        {errors.addressError && (
          <div className="h-6">
            {errors.addressError && (
              <p className="text-red-500">{errors.addressError}</p>
            )}
          </div>
        )}
      </div>

      <div className="relative z-0 w-full mb-5 group">
        <input
          type="tel"
          name="phone"
          id="phoneNumberProvider"
          onChange={handleChange}
          disabled={lock}
          className="block py-2.5 px-0 w-full text-base text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          required
        />
        <label htmlFor="phoneNumberProvider" className="form-label">
          Phone
        </label>
        <div className="h-6">
          {errors.phoneError && (
            <p className="text-red-500">{errors.phoneError}</p>
          )}
        </div>
      </div>
    </form>
  );
});

export default CreateProviderForm;
