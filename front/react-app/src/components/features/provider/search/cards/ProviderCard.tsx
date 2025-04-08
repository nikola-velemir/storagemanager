import ProviderCardAccordion from "./ProviderCardAccordion";
import { ProviderInvoiceResponse } from "../../../../../model/provider/ProviderInvoiceResponse";
import { useNavigate } from "react-router-dom";

interface ProviderCardProps {
  id: string;
  name: string;
  phoneNumber: string;
  address: string;
  invoices: ProviderInvoiceResponse[];
}

const ProviderCard = ({
  id,
  name,
  phoneNumber,
  address,
  invoices,
}: ProviderCardProps) => {
  const navigate = useNavigate();
  const handleMoreInfoClick = () => {
    navigate("/provider-profile/" + id);
  };
  return (
    <>
      <div className="w-11/12 my-4 bg-gray-700 text-white rounded-2xl shadow-md p-4">
        <div className="flex justify-between items-center">
          <div className="flex items-center gap-4">
            <div className="bg-slate-800 rounded-xl p-2">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke-width="1.5"
                stroke="currentColor"
                className="size-16"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                />
              </svg>
            </div>

            <div className="space-y-1">
              <div className="text-sm text-gray-400">
                <span className="font-light">ID:</span>{" "}
                <span className="font-medium text-base text-white">{id}</span>
              </div>
              <div className="text-sm text-gray-400">
                <span className="font-light">Name:</span>{" "}
                <span className="font-medium text-base text-white">{name}</span>
              </div>
              <div className="text-sm text-gray-400">
                <span className="font-light">Address:</span>{" "}
                <span className="font-medium text-base text-white">
                  {address}
                </span>
              </div>
              <div className="text-sm text-gray-400">
                <span className="font-light">Phone number:</span>{" "}
                <span className="font-medium text-base text-white">
                  {phoneNumber}
                </span>
              </div>
            </div>
          </div>

          <button
            onClick={handleMoreInfoClick}
            className="bg-green-600 text-sm font-medium hover:bg-green-700 px-4 py-2 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 transition"
          >
            More info
          </button>
        </div>
        <ProviderCardAccordion invoices={invoices} />
      </div>
    </>
  );
};

export default ProviderCard;
