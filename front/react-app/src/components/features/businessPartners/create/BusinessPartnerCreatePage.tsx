import { ChangeEvent, useState } from "react";
import { toast } from "react-toastify";
import { validateField } from "../../product/create/ProductCreateFormValidators";
import BusinessPartnerSelectRole from "./BusinessPartnerSelectRole";
import { BusinessPartnerRoles } from "../../../../model/businessPartner/BusinessPartnerRoles";
import { BusinessPartnerService } from "../../../../services/businessPartner/BusinessPartnerService";

interface BusinessPartnerCreateFormData {
  partnerName: string;
  partnerAddress: string;
  partnerPhoneNumber: string;
}
interface ExportCreateFormDataErrors {
  partnerNameError: string;
  partnerAddressError: string;
  partnerPhoneNumberError: string;
}

const BusinessPartnerCreatePage = () => {
  const [selectedRole, setSelectedRole] = useState(
    BusinessPartnerRoles.PROVIDER
  );
  const [formData, setFormData] = useState<BusinessPartnerCreateFormData>({
    partnerAddress: "",
    partnerName: "",
    partnerPhoneNumber: "",
  });
  const [errors, setErrors] = useState<ExportCreateFormDataErrors>({
    partnerAddressError: "",
    partnerNameError: "",
    partnerPhoneNumberError: "",
  });
  const handleCreateClick = () => {
    if (!validateAllFields()) {
      toast.error("Some fields are invalid!");
      return;
    }
    BusinessPartnerService.create({
      address: formData.partnerAddress,
      name: formData.partnerName,
      phoneNumber: formData.partnerPhoneNumber,
      role: selectedRole,
    })
      .then(() => toast.success("Partner created!"))
      .catch(() => toast.error("Something went wrong"));
  };
  const handleRoleChange = (e: BusinessPartnerRoles) => {
    setSelectedRole(e);
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
      validateField(formData.partnerName) &&
      validateField(formData.partnerAddress) &&
      validateField(formData.partnerPhoneNumber)
    );
  };
  const validate = (name: string, value: string) => {
    let valid = true;
    if (name === "partnerName") {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          partnerNameError: "Name is required",
        });
        valid = false;
      } else
        setErrors({
          ...errors,
          partnerNameError: "",
        });
    } else if (name === "partnerPhoneNumber") {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          partnerPhoneNumberError: "Phone number is required",
        });
        valid = false;
      } else
        setErrors({
          ...errors,
          partnerPhoneNumberError: "",
        });
    } else {
      if (!validateField(value)) {
        setErrors({
          ...errors,
          partnerAddressError: "Address is required",
        });
        valid = false;
      } else
        setErrors({
          ...errors,
          partnerAddressError: "",
        });
    }
    return valid;
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 py-8 overflow-auto">
        <div className="relative z-0 w-full mb-5 group">
          <BusinessPartnerSelectRole
            role={selectedRole}
            emitRole={handleRoleChange}
          />
        </div>
        <form className="max-w-lg mx-auto">
          <div className="relative z-0 w-full mb-5 group">
            <input
              type="text"
              name="partnerName"
              id="partnerName"
              onChange={handleOnChange}
              className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
              placeholder=" "
              required
            />
            <label
              htmlFor="partnerName"
              className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Name
            </label>
            <div className="h-4 text-red-600">{errors.partnerNameError}</div>
          </div>
          <div className="relative z-0 w-full mb-5 group">
            <input
              type="text"
              name="partnerPhoneNumber"
              id="partnerPhoneNumber"
              onChange={handleOnChange}
              className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
              placeholder=" "
              required
            />
            <label
              htmlFor="partnerPhoneNumber"
              className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Phone number
            </label>
            <div className="h-4 text-red-600">
              {errors.partnerPhoneNumberError}
            </div>
          </div>
          <div className="relative z-0 w-full mb-5 group">
            <input
              type="text"
              name="partnerAddress"
              id="partnerAddress"
              onChange={handleOnChange}
              className="block py-2.5 px-0 w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-400 focus:outline-none focus:ring-0 focus:border-blue-400 peer"
              placeholder=" "
              required
            />
            <label
              htmlFor="partnerAddress"
              className="peer-focus:font-medium absolute text-sm text-gray-300 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-400 peer-focus:dark:text-blue-400 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
            >
              Address
            </label>
            <div className="h-4 text-red-600">{errors.partnerAddressError}</div>
          </div>
          <button
            type="button"
            onClick={handleCreateClick}
            className="w-full text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
          >
            Create partner
          </button>
        </form>
      </div>
    </div>
  );
};

export default BusinessPartnerCreatePage;
